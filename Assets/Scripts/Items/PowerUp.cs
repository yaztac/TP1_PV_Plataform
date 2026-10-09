using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float speedMultiplier = 1.8f;
    [SerializeField] private float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.SpeedBoost(speedMultiplier, duration);
            }

            
            Destroy(gameObject);
        }
    }
}
