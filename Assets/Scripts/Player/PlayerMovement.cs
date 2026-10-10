using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector3 boxSize = new Vector3(0.8f, 0.2f, 0.8f); // Tamaño de la caja de detección
    [SerializeField] private LayerMask groundLayer;


    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    private float baseSpeed;
    private Coroutine speedBoostCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.sleepThreshold = 0f;
        
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
    }

    
    void Update()
    {
        float horizontal = Input.GetAxis("Vertical");
        float vertical = Input.GetAxis("Horizontal");

        moveDirection = new Vector3(vertical, 0f, horizontal).normalized;

        if (groundCheck != null)
        {
            isGrounded = Physics.OverlapBox(groundCheck.position, boxSize / 2f, Quaternion.identity, groundLayer).Length > 0;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    private void MovePlayer()
    {
        if (moveDirection.magnitude >= 0.1f)
        {
            // Mueve la posición del Rigidbody
            Vector3 targetPosition = rb.position + moveDirection * speed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            // Rota suavemente la cápsula hacia la dirección en la que avanza
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
    
  
    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    public void SpeedBoost(float boostAmount, float duration)
    {
        if (speedBoostCoroutine != null)
        {
            StopCoroutine(speedBoostCoroutine);
        }
        speedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(boostAmount, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostAmount, float duration)
    {
        speed += boostAmount;
        yield return new WaitForSeconds(duration);
        speed = baseSpeed;
        speedBoostCoroutine = null;
    }
}
