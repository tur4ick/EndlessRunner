using UnityEngine;
using Zenject;

public class Coin : MonoBehaviour
{
    [Inject] private CoinService _coinService;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _coinService.AddCoin();
            Destroy(gameObject);
        }
    }
}
