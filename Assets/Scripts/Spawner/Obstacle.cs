using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime= 1.5f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El jugador pierde puntos");
            
            Destroy(gameObject);
        }
    }
}
