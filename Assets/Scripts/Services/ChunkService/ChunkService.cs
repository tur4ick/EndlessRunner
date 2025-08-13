using System.Collections.Generic;
using Services.ChunkService;
using UnityEngine;
using Zenject;

public class ChunkService : ITickable
{
    [Inject] private readonly DiContainer _container;
    private readonly ChunkPrefabMap _chunkPrefabMap;
    private readonly Transform _chunksRoot;
    private readonly List<GameObject> _activeChunks = new();
    private readonly int _maxChunks = 5;
    private readonly float _chunkLength = 15f;
    private readonly float _removeThresholdZ = -15f;
    
    private List<GameObject> toRemove = new();
    private bool _isRunning = false;
    
    
    private float _baseSpeed = 5f;
    private float _boostedSpeed = 5f;
    private float _boostEndTime;

    public float Speed => _boostedSpeed;

    public ChunkService(ChunkPrefabMap chunkPrefabMap, Transform root)
    {
        _chunksRoot = root;
        _chunkPrefabMap = chunkPrefabMap;
    }

    public void StartRun()
    {
        if(_isRunning) return;
        ResetRun();
        for (int i = 0; i < _maxChunks; i++)
        {
            SpawnNextChunk();
        }

        _isRunning = true;
    }
    
    public void StopRun()
    {
        _isRunning = false;
    }

    public void ResetRun()
    {
        foreach (var chunk in _activeChunks)
        {
            Object.Destroy(chunk);
        }
        _activeChunks.Clear();
        
        _boostedSpeed = _baseSpeed;
        _boostEndTime = 0f;
    }
    public void Boost(float amount, float duration)
    {
        _boostedSpeed = _baseSpeed + amount;
        _boostEndTime = Time.time + duration;
    }

    private void SpawnNextChunk()
    {
        int index = Random.Range(0, _chunkPrefabMap.ChunkPrefabs.Count);
        var prefab = _chunkPrefabMap.ChunkPrefabs[index];
        
        float lastZ;

        if (_activeChunks.Count == 0)
        {
            lastZ = _chunksRoot.position.z;
        }
        else
        {
            lastZ = _activeChunks[_activeChunks.Count - 1].transform.position.z;
        }

        Vector3 spawnPosition = new Vector3(_chunksRoot.position.x, _chunksRoot.position.y, lastZ + _chunkLength);
        GameObject instance = _container.InstantiatePrefab(prefab, spawnPosition, Quaternion.identity, _chunksRoot);

        _activeChunks.Add(instance);
    }

    public void Tick()
    {
        if(!_isRunning) return;
        
        if (_boostedSpeed > _baseSpeed && Time.time >= _boostEndTime)
        {
            _boostedSpeed = _baseSpeed;
        }

        foreach (var chunk in _activeChunks)
        {
            chunk.transform.Translate(Vector3.back * (Speed * Time.deltaTime));

            if (chunk.transform.position.z < _removeThresholdZ)
                toRemove.Add(chunk);
        }

        foreach (var chunk in toRemove)
        {
            _activeChunks.Remove(chunk);
            Object.Destroy(chunk);
            SpawnNextChunk();
        }
        toRemove.Clear();
    }
}
