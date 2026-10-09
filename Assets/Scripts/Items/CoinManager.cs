using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    private int coinCount;
    private int totalCoins;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        coinCount = 0;
        Debug.Log("Monedas en escena: " + totalCoins);
    }

    public void CollectCoin()
    {
        coinCount++;

        if (coinCount >= totalCoins)
        {
            Debug.Log("¡Has recogido todas las monedas!");
        }
    }
}
