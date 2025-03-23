using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform CameraTransform;
    private CharacterController characterController;

    private float BaseMoveSpeed;
    public float MoveSpeed;
    public float RotateSpeed = 5f;
    public float Sensivity = 1f;

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
    public AudioSource WalkAudioSource;
    public AudioClip SfxBouffe;
    public AudioClip SfxWalk;

    public bool IsDead;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");

        BaseMoveSpeed = MoveSpeed;
        IsEating = false;
        SetControlsEnabled(true);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!MenuSystem.Main.Visible)
            {
                MenuSystem.Main.Show(() => {
                    SetControlsEnabled(true);
                    Time.timeScale = 1f;
                });
                SetControlsEnabled(false);
                Time.timeScale = 0f;
            }
            else
            {
                MenuSystem.Main.Hide();
                Time.timeScale = 1f;
                SetControlsEnabled(true);
            }
        }

        if (!ControlsEnabled)
        {
            return;
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
        if (move == Vector3.zero)
        { WalkAudioSource.Play();   }
        characterController.Move(move);
    }

    public void Rotate(Vector2 rotationDir)
    {
        rotationDir *= RotateSpeed * Sensivity;
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

    public void SlowlyFaceUpwards(float deltaT)
    {
        float newX = Mathf.Lerp(CameraTransform.localRotation.eulerAngles.x, 5f, deltaT);
        CameraTransform.localRotation = Quaternion.Euler(newX, 0, 0);
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
