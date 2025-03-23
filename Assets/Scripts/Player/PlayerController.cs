using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform CameraTransform;
    private CharacterController characterController;

    private const float BaseMoveSpeed = 10f;
    public float MoveSpeed ;
    public float RotateSpeed = 5f;

    private float xRotation;
    private bool cursorLocked;

    private InputAction moveAction;
    private InputAction lookAction;

    public ParanoiaBar ParanoiaBar;
    public int DrugsAmmount;
    private bool Eating = false;
    private float latestEatingTime;
    private float EATING_INTERVAL = 1.5f;
    public bool ControlsEnabled = true;

    public AudioSource Src;
    public AudioClip SfxBouffe;

    


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        SetControlsEnabled(true);

        MoveSpeed = BaseMoveSpeed;
        Eating = false;
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
            if (!Eating)
            {

                if (Input.GetKeyUp(KeyCode.E) && CheckDrug())
                {
                    ConsumeDrug();
                    Eating = true;
                    MoveSpeed = 3f;

                    latestEatingTime = Time.time;
                }
            }
        }

        if (Time.time > latestEatingTime + EATING_INTERVAL)
        {
            Eating = false;
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
        DrugsAmmount--;
    }

    public void Bouffing()
    {
        Src.clip = SfxBouffe;
        Src.Play();
    }

    bool CheckDrug()
    { return DrugsAmmount >= 1; }

}
