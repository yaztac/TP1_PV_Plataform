using UnityEngine;

public class CoinCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CoinManager.Instance != null)
            {
                Debug.Log("Moneda recogida");
                CoinManager.Instance.CollectCoin();
            }

            Destroy(gameObject);
        }
    }
}
