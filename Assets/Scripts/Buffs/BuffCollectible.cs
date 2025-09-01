using UnityEngine;
using Zenject;
using Services.AudioService;

namespace Buffs
{
    public class BuffCollectible : MonoBehaviour
    {
        [SerializeField] public BuffType type;
        
        public float amount = 1.5f;
        public float duration = 5f;
        
        [Inject] private BuffController _buffController;
        [Inject] private AudioService _audio;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            _buffController.Apply(type, amount, duration);
            _audio.PlayBuff();
            Destroy(gameObject);
        }
        
    }
}