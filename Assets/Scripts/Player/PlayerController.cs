using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class PlayerController : MonoBehaviour
{
    [field: SerializeField, Header("References to scripts and Input")]
    public CharacterController CharacterController
    {  get; private set; }
    public PlayerControls PlayerControl
    { get; private set; }


    [field: SerializeField, Header("Movement related values")]
    public float PlayerMoveSpeed
    { get; private set; } = 10.0f;
    [field: SerializeField]
    public float PlayerRunMultiplyer
    { get; private set; } = 2.0f;
    public Vector2 CurrentMovementInput
    { get; private set; }
    public Vector3 CurrentVelocity
    { get; private set; }
    public bool IsMovementPressed
    { get; private set; }
    public bool IsRunPressed
    { get; private set; }


    [field: SerializeField, Header("Mouse Movement releated values")]
    public float MouseSensitivityX
    { get; private set; } = 8.0f;
    [field: SerializeField]
    public float MouseSensitivityY
    { get; private set; } = 0.5f;
    public float MouseX
    { get; private set; }
    public float MouseY
    { get; private set; }
    public bool IsMouseMoving
    { get; private set; }
    public float XClamp
    { get; private set; } = 85.0f;
    public float CameraPitch
    { get; private set; } = 0.0f;
    [field:SerializeField]
    public Camera PlayerCamera
    { get; private set; }


    [field: SerializeField, Header("Jump Groundedness and Gravity related values")]
    public float JumpPower
    { get; private set; } = 100.0f;
    public float Gravity
    { get; private set; } = -9.81f;
    public bool IsJumpPressed
    { get; private set; }
    public bool IsGrounded
    { get; private set; }
    [field: SerializeField]
    public float GroundCheckRadius
    { private set; get; } = 0.3f;
    [field: SerializeField]
    public float GroundCheckDistance 
    { private set; get; } = 0.1f;
    public Vector3 GroundCheckRayCastSphereOrigin
    { get; private set; }
    [field: SerializeField]
    public LayerMask GroundMask
    { get; private set; }

    public bool IsInteractPressed
    { get; private set; }

    [field: SerializeField]
    public float InteractDistance
    { get; private set; }

    private void Awake()
    {
        SetInputListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementAndRotation();

        // Debug.Log($"IsJumpPressed this frame: {IsJumpPressed}");

        IsGrounded = CheckIfGrounded();

        ApplyGravity();

        // Debug.Log($"IsCharatcerGrounded this frame: {IsGrounded}");

        if (IsJumpPressed)
        {
            if (IsGrounded)
            {
                CurrentVelocity += new Vector3(0, JumpPower , 0);
            }

            IsJumpPressed = false;
        }

        InteractLoop();
    }

    private void LateUpdate()
    {
        if (IsMouseMoving)
        {
            // Rotate character left/right (yaw)
            transform.Rotate(Vector3.up, MouseX * Time.deltaTime);

            // Adjust camera pitch (up/down rotation)
            CameraPitch -= MouseY * Time.deltaTime;
            CameraPitch = Mathf.Clamp(CameraPitch, -XClamp, XClamp); // Prevent flipping

            // Apply clamped rotation
            PlayerCamera.transform.localRotation = Quaternion.Euler(CameraPitch, 0, 0);
        }

        CharacterController.Move(CurrentVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        PlayerControl.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControl.CharacterControls.Disable();
    }

    public void InteractLoop()
    {
        if (IsInteractPressed)
        {
            Ray viewRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool RayCastSuccess = Physics.Raycast(viewRay.origin, Camera.main.transform.forward, out RaycastHit interactHit, InteractDistance);

            if (RayCastSuccess)
            {
                GameObject hitObject = interactHit.transform.gameObject;
                Debug.Log($"{hitObject.name.ToString()}");
            }

            IsInteractPressed = false;
        }
    }

    void HandleMovementAndRotation()
    {
        Vector3 forward = transform.forward * CurrentMovementInput.y;
        Vector3 right = transform.right * CurrentMovementInput.x;
        Vector3 inputMovement = forward + right;
        float speed = IsRunPressed ? PlayerMoveSpeed * PlayerRunMultiplyer : PlayerMoveSpeed;

        CurrentVelocity = new Vector3(speed * inputMovement.x, CurrentVelocity.y, speed * inputMovement.z);
    }

    private void ApplyGravity()
    {
        // If not grounded, apply gravity
        if (CharacterController.isGrounded)
        {   
            // Setting the gravity value when grounded as it should remain a constant to allow natural falls and to stop playing from bumping down shallow slopes
            CurrentVelocity = new Vector3(CurrentVelocity.x, Gravity, CurrentVelocity.z);
        }
        else
        {
            CurrentVelocity += new Vector3(0, Gravity * Time.deltaTime, 0);
        }
    }

    private bool CheckIfGrounded()
    {
        GroundCheckRayCastSphereOrigin = transform.position + Vector3.down * (CharacterController.height / 2 - GroundCheckRadius);

        return Physics.SphereCast(GroundCheckRayCastSphereOrigin, GroundCheckRadius, Vector3.down, out _, GroundCheckDistance, GroundMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GroundCheckRayCastSphereOrigin, GroundCheckRadius);
    }

    public void SetInputListeners()
    {
        PlayerControl = new PlayerControls();

        PlayerControl.CharacterControls.Move.started += context =>
        {
            OnMovementInput(context);
        };

        PlayerControl.CharacterControls.Move.canceled += context =>
        {
            OnMovementInput(context);
        };

        PlayerControl.CharacterControls.Move.performed += context =>
        {
            OnMovementInput(context);
        };

        PlayerControl.CharacterControls.Run.performed += context =>
        {
            OnRun(context);
        };

        PlayerControl.CharacterControls.Run.canceled += context =>
        {
            OnRun(context);
        };



        PlayerControl.CharacterControls.MouseX.performed += context =>
        {
            OnMouseAxisInput(context, "x");
        };

        PlayerControl.CharacterControls.MouseY.performed += context =>
        {
            OnMouseAxisInput(context, "y");
        };

        PlayerControl.CharacterControls.MouseX.canceled += context =>
        {
            OnMouseAxisInput(context, "x");
        };

        PlayerControl.CharacterControls.MouseY.canceled += context =>
        {
            OnMouseAxisInput(context, "y");
        };



        PlayerControl.CharacterControls.Jump.started += context =>
        {
            OnJumpInput(context);
            // Debug.Log("Jump Input Detected");
        };

        PlayerControl.CharacterControls.Interact.started += context =>
        {
            OnInteractInput(context);
        };
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        IsInteractPressed = context.ReadValueAsButton();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        CurrentMovementInput = context.ReadValue<Vector2>();

        IsMovementPressed = CurrentMovementInput.x != 0 || CurrentMovementInput.y != 0;
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        IsRunPressed = context.ReadValueAsButton();
    }

    public void OnMouseAxisInput(InputAction.CallbackContext context, string xOrY)
    {
        float value = context.ReadValue<float>();

        xOrY = xOrY.ToLower();

        if (xOrY == "x")
        {
            MouseX = value * MouseSensitivityX;

        }
        else if (xOrY == "y")
        {
            MouseY = value * MouseSensitivityY;
        }
        else
        {
            Debug.LogError("Error");
        }


        IsMouseMoving = MouseX != 0 || MouseY != 0;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsJumpPressed = context.ReadValueAsButton();
            // Debug.Log($"IsJumpPressed set to {IsJumpPressed}");
        }
    }
}
