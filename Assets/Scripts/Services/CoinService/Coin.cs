using UnityEngine;
using Zenject;
using Services.AudioService;

namespace Services.CoinService
{
    public class Coin : MonoBehaviour
    {
        [Inject] private CoinService _coinService;
        [Inject] private AudioService.AudioService _audio;
    

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _coinService.AddCoin();
                _audio.PlayCoin();
                Destroy(gameObject);
            }
        }
    }
}
