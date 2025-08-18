using UnityEngine;
using Zenject;

namespace Services.CoinService
{
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
}
