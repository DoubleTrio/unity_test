using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField] private PlayerInputSystem playerInputSystem;
    [SerializeField] private Transform groundReferencePoint;

    private CharacterController characterController;

	public const float GRAVITY_MULTIPLIER = 5f;
    public const float MAX_FALL_SPEED = -50f;
    public const float MAX_RISE_SPEED = 100f;
    public const float GRAVITY_COMEBACK_MULTIPLIER = .03f;
    public const float GRAVITY_DIVIDER = .6f;
    public const float AIR_RESISTANCE = 5f;

	public PlayerSettingsSO playerSettings;
    private float inputX = 0;
    bool jumpPressed = false;
    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnEnable()
	{
        playerInputSystem.Enable();
    }

	private void OnDisable()
	{
        playerInputSystem.Disable();
    }

	private void Awake()
	{

        rb = GetComponent<Rigidbody2D>();
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Player.Jump.performed += ctx => Jump(ctx);
        playerInputSystem.Player.Movement.performed += ctx => Movement(ctx);
        playerInputSystem.Player.Movement.canceled += ctx => MovementCanceled(ctx);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position, Vector3.down));
    }
    private void FixedUpdate()
	{

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        print(isGrounded);
        //isGrounded = true;
        //print(inputX);
		movementDirection = new Vector2(inputX, 0) * playerSettings.speed;
        /*		movementDirection = transform.TransformDirection(movementDirection);
                movementDirection *= playerSettings.speed * Time.deltaTime;

                if (characterController.isGrounded)
                {
                    if (jumpPressed)
                    {
                        movementDirection.y = playerSettings.jumpHeight;
                    }
                }
                movementDirection.y -= gravity * Time.deltaTime;
        *//*		transform.Rotate(0, moveVector.x, 0);*//*
                characterController.Move(movementDirection);*/
        rb.velocity = new Vector2(movementDirection.x, rb.velocity.y);
    }

	public void Jump(InputAction.CallbackContext context)
	{
		jumpPressed = context.ReadValueAsButton();
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerSettings.jumpHeight);
        }
	}

	public void Movement(InputAction.CallbackContext context)
	{
		inputX = context.ReadValue<Vector2>().x;
	}

    public void MovementCanceled(InputAction.CallbackContext context)
    {
        inputX = 0f;
    }
}
