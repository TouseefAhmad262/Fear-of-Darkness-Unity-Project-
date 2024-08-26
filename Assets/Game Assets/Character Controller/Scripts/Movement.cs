using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float jumpHeight = 1.5f;
    public float gravity = 9.81f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector2 motion;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        motion = InputsManager.Instance.Controls.Player.Movement.ReadValue<Vector2>();
        Vector3 move = transform.right * motion.x + transform.forward * motion.y;
        characterController.Move(move * walkSpeed * Time.deltaTime);

        if (InputsManager.Instance.Controls.Player.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * (-gravity));
        }

        velocity.y += (-gravity) * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}