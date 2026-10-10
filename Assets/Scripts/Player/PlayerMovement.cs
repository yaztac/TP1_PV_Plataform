using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private GroundSensor groundSensor;
    

    private Rigidbody rb;
    private float verticalImput;
    private float horizontalInput;

    private float baseSpeed;
    private Coroutine speedBoostCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        verticalImput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && groundSensor != null && groundSensor.IsGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void MovePlayer()
    {
        Vector3 direction = transform.forward * verticalImput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + direction);
    }
    
    private void RotatePlayer()
    {
        float rotation = horizontalInput * rotationSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotation, 0f));
    }

    private void Jump()
    {
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
