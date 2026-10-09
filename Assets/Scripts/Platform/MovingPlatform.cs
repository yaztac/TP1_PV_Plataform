using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float switchInterval = 4f;

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 targetPosition;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("¡Faltan asignar Point A o Point B en el Inspector de la plataforma!", gameObject);
            return;
        }
        posA = pointA.position;
        posB = pointB.position;

        targetPosition = posB;

        InvokeRepeating(nameof(SwitchDirection), switchInterval, switchInterval);
    }

   
    private void FixedUpdate()
    {
        if (pointA == null || pointB == null) return;
        
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPosition, speed * Time.fixedDeltaTime);
         rb.MovePosition(newPosition);
        
    }

    private void SwitchDirection()
    {
        if (targetPosition == pointB.position)
        {
            targetPosition = pointA.position;
        }
        else
        {
            targetPosition = pointB.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < 0.5f)
            {
                collision.transform.SetParent(transform);

            }
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desvincula al jugador de la plataforma cuando se baja o salta
            collision.transform.SetParent(null);
        }
    }

  


}
