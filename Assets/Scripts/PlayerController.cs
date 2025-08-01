using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private Transform armsPivot; // пивот для вращения рук с камерой
    [SerializeField] private Animator playerAnimator;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private CharacterController controller;
    private PlayerInputs input;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool lastPunchRight = false;

    public bool isHitting = false;

    private void Awake()
    {
        input = new PlayerInputs();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Лок курсора в центр экрана
        Cursor.visible = false;
    }
    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        input.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        input.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        input.Player.Attack.performed += _ => OnPunch();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    private void Update()
    {
        // Движение
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        move = transform.TransformDirection(move);
        controller.SimpleMove(move * moveSpeed);

        // Поворот мышью
        rotationY += lookInput.x * mouseSensitivity;
        rotationX -= lookInput.y * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f); // ограничение вертикального угла

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f); // поворот тела
        if (armsPivot != null)
            armsPivot.localRotation = Quaternion.Euler(rotationX, 0f, 0f); // поворот вверх/вниз
    }

    private void OnPunch()
    {
        isHitting = true;
        if (lastPunchRight)
        {
            playerAnimator.SetTrigger("PunchLeft");
            playerAnimator.ResetTrigger("PunchRight");
        }
        else
        {
            playerAnimator.SetTrigger("PunchRight");
            playerAnimator.ResetTrigger("PunchLeft");
        }
        lastPunchRight = !lastPunchRight;
    }
}
