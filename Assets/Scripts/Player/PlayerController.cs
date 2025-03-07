using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.PostProcessing;

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


    private void Awake()
    {
        SetInputListeners();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(CheckDepthOfField(0.1f));
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

    /// <summary>
    /// Interaction logic for assessing if a gameobject or its parent contains a script that implements the interactable interface and call that object interaction method.
    /// </summary>
    public void InteractLoop()
    {
        // Check for if the input for inteaction of pressed by the player.
        if (IsInteractPressed)
        {
            // On button press, send out a raycast for any information on any object it hits.
            Ray viewRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool rayCastSuccess = Physics.Raycast(viewRay.origin, Camera.main.transform.forward, out RaycastHit interactHit);
            IInteractable interactiveInterface = null;
            bool isInDistanceAndEnabled = false;

            // If an object was hit, check the gameobject that was hit.
            if (rayCastSuccess)
            {
                GameObject hitObject = interactHit.transform.gameObject;

                // If the gameobject has a script component that implements IInteractable interface start interaction distance check.
                if (CheckIfObjectIsInteractive(hitObject, out interactiveInterface))
                {
                    if (interactiveInterface.CheckIfInteractionAllowed(IInteractable.GetInteractionDistance(viewRay.origin, interactHit.point)))
                    {
                        interactiveInterface.OnInteraction();
                        isInDistanceAndEnabled = true;
                    }
                    else
                    {
                        isInDistanceAndEnabled = false;
                    }
                }
                // Else if the gameobjects parent has a script component that implements IInteractable interface, start interaction distance check.
                else if (hitObject.transform.parent != null && CheckIfObjectIsInteractive(hitObject.transform.parent.gameObject, out interactiveInterface))
                {
                    if (interactiveInterface.CheckIfInteractionAllowed(IInteractable.GetInteractionDistance(viewRay.origin, interactHit.point)))
                    {
                        interactiveInterface.OnInteraction();
                        isInDistanceAndEnabled = true;
                    }
                    else
                    {
                        isInDistanceAndEnabled = false;
                    }
                }
            }

            // Draws ray colour depending on hit, interactivness and within distance; if successful interaction: blue. If interactive but too far: yellow. If non-interactive object hit, white. if nothing hit, red.
            Debug.DrawRay(viewRay.origin, Camera.main.transform.forward * (rayCastSuccess ? interactHit.distance : 1000), (rayCastSuccess ? (interactiveInterface != null) ? (isInDistanceAndEnabled) ? Color.blue : Color.yellow : Color.white : Color.red), 0.5f, false);

            IsInteractPressed = false;
        }
    }

    /// <summary>
    /// Checks a given game object for containing a script with an interactable interface supplied and supplies the interactable interface.
    /// </summary>
    /// <param name="objectToCheck"></param>
    /// <param name="interactiveInterface"></param>
    /// <returns></returns>
    public static bool CheckIfObjectIsInteractive(GameObject objectToCheck, out IInteractable interactiveInterface)
    {
        if (objectToCheck.TryGetComponent<IInteractable>(out interactiveInterface))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Coroutine intended to check the distance to objects being looked at from the centre of the screen outwards and then changing DOF to reflect the distance.
    /// </summary>
    /// <param name="timePerCheck"></param>
    /// <returns></returns>
    public IEnumerator CheckDepthOfField(float timePerCheck)
    {
        // Ensure that volume post process component can be found in scene first and finding the DOF setting element.
        PostProcessVolume postProcessingVolume = GameObject.FindObjectOfType<PostProcessVolume>();
        DepthOfField dof = null;
        bool dofFound = false;

        if (postProcessingVolume.profile.TryGetSettings<DepthOfField>(out dof))
        {
            dofFound = true;
        }

        // Keep looping every given time check and re-check and recast the DOF value.
        while (true)
        {
            Ray viewRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (dofFound && dof != null && Physics.Raycast(viewRay.origin, Camera.main.transform.forward, out RaycastHit interactHit))
            {
                float targetValue = Vector3.Distance(viewRay.origin, interactHit.point);

                dof.focusDistance.Override(Mathf.Clamp(targetValue, 0.7f, targetValue));

                // The volume must be disabled and then enabled to allow changes to take place.
                postProcessingVolume.enabled = false;
                postProcessingVolume.enabled = true;
            }
            else
            {
                Debug.LogError("Error with DoF element of post-processing not found");
            }

            yield return new WaitForSeconds(timePerCheck);
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
