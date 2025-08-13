using UnityEngine;
using Zenject;

public class BoostBuff : MonoBehaviour
{
    [Inject] private ChunkService _chunkService;

    public float boostAmount = 3f;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _chunkService?.Boost(boostAmount, duration);
            Destroy(gameObject);
        }
    }
}
