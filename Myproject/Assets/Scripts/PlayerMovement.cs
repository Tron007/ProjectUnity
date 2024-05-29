using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float runSpeedMultiplier = 3f;
    [SerializeField] private float crouchSpeedMultiplier = 0.5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float crouchHeight = 0.5f;
    private float originalHeight;
    private Rigidbody _rb;
    private Animator _animator;
    private bool isRunning = false;
    public bool isCrouching = false;
    private bool isGrounded = true;
    private bool canJump = true;


    [SerializeField] private Vector3 initialSpawnPosition = new Vector3(180f, 0.1f,-180f);
    [SerializeField] private Vector3 clientSpawnPosition = new Vector3(190, 0.1f, -190);

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            if (IsServer)
            {
                transform.position = initialSpawnPosition;
            }
            else
            {
                transform.position = clientSpawnPosition;
            }
        }
        Transform cameraTransform = transform.Find("Main Camera");
        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(IsOwner);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        originalHeight = _rb.transform.localScale.y;
    }

    private void Update()
    {
        if (!IsOwner) return;
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            ToggleCrouch();
            Debug.Log("Crouching");
        }

        if (Input.GetButtonDown("Jump"))
        {
            _animator.SetTrigger("isJump");
            TryJump();
            Debug.Log("Jump");
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        float horizontalSpeed = Input.GetAxis("Horizontal") * GetEffectiveSpeed() * Time.fixedDeltaTime;
        float verticalSpeed = Input.GetAxis("Vertical") * GetEffectiveSpeed() * Time.fixedDeltaTime;

        Vector3 moveDirection = new Vector3(horizontalSpeed, 0, verticalSpeed);

        // Преобразуем его в мировые координаты
        moveDirection = transform.TransformDirection(moveDirection);

        // Устанавливаем velocity игрока, сохраняя текущую вертикальную скорость (падение/прыжок)
        _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y, moveDirection.z);

        // Поворачиваем игрока в направлении движения, если есть значимое движение
        if (new Vector3(horizontalSpeed, 0, verticalSpeed).sqrMagnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }


    private float GetEffectiveSpeed()
    {
        if (isCrouching)
        {
            return speed * crouchSpeedMultiplier;
        }
        else if (isRunning)
        {
            _animator.SetBool("isRun", (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0));
            return speed * runSpeedMultiplier;
        }
        else
        {
            _animator.SetBool("isWalk", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
            return speed;
        }
    }

    public void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            _rb.transform.localScale = new Vector3(_rb.transform.localScale.x, crouchHeight, _rb.transform.localScale.z);
        }
        else
        {
            _rb.transform.localScale = new Vector3(_rb.transform.localScale.x, originalHeight, _rb.transform.localScale.z);
        }
    }

    public void TryJump()
    {
        if (isGrounded && canJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        canJump = false;
        isGrounded = false;
        Invoke("ResetJumpFlag", 0.2f);
    }

    private void ResetJumpFlag()
    {
        canJump = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true;
        }
    }
}
