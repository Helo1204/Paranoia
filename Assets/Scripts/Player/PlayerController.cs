using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform CameraTransform;
    private CharacterController characterController;

    private float BaseMoveSpeed;
    public float MoveSpeed;
    public float RotateSpeed = 5f;

    private float xRotation;
    private bool cursorLocked;

    private InputAction moveAction;
    private InputAction lookAction;

    public ParanoiaBar ParanoiaBar;
    public int DrugsAmount;
    private bool IsEating = false;
    private float latestEatingTime;
    private float EATING_INTERVAL = 1.5f;
    public bool ControlsEnabled = true;

    public AudioSource Src;
    public AudioClip SfxBouffe;

    public bool IsDead;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        SetControlsEnabled(true);

        BaseMoveSpeed = MoveSpeed;
        IsEating = false;
    }

    private void Update()
    {
        if (!ControlsEnabled)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            LockCursor(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            LockCursor(true);
        }

        if (cursorLocked)
        {
            Move(moveAction.ReadValue<Vector2>());
            Rotate(lookAction.ReadValue<Vector2>());
            if (!IsEating)
            {

                if (Input.GetKeyUp(KeyCode.E) && CheckDrug())
                {
                    ConsumeDrug();
                    IsEating = true;
                    MoveSpeed /= 2;

                    latestEatingTime = Time.time;
                }
            }
        }

        if (Time.time > latestEatingTime + EATING_INTERVAL)
        {
            IsEating = false;
            MoveSpeed = BaseMoveSpeed;
        }
    }

    public void Move(Vector2 moveDir)
    {
        Vector3 move = (transform.forward * moveDir.y + transform.right * moveDir.x) * MoveSpeed * Time.deltaTime;
        characterController.Move(move);
    }

    public void Rotate(Vector2 rotationDir)
    {
        rotationDir *= RotateSpeed;
        xRotation -= rotationDir.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * rotationDir.x);
    }

    public void SlowlyTurnTo(Vector3 newAngles, float deltaT)
    {
        float newY = Mathf.Lerp(transform.rotation.eulerAngles.y, newAngles.y, deltaT);
        transform.rotation = Quaternion.Euler(0, newY, 0);
    }

    public void LockCursor(bool state)
    {
        if (state)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        cursorLocked = state;
    }

    public void SetControlsEnabled(bool enabled)
    {
        ControlsEnabled = enabled;
        LockCursor(enabled);
    }

    void ConsumeDrug()
    {
        Bouffing();
        ParanoiaBar.AddParanoia(-20);
        DrugsAmount--;
    }

    public void Bouffing()
    {
        Src.clip = SfxBouffe;
        Src.Play();
    }

    private bool CheckDrug()
    { return DrugsAmount >= 1; }

    public void AddDrug(int n)
    {
        DrugsAmount += n;
    }

}
