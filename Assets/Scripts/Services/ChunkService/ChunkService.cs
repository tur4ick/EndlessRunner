using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.ChunkService
{
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
        private float _speedMultiplier = 1f;

        public float Speed => _speedMultiplier * _baseSpeed;
        
        public bool IsRunning => _isRunning;

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
                Debug.Log("Start chunk spawned");
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
            Debug.Log("ChunkSpawned");

            _activeChunks.Add(instance);
        }

        public void Tick()
        {
            if(!_isRunning) return;

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
                Debug.Log("ChunkSpawned");
            }
            toRemove.Clear();
        }
        
        public void SetSpeedMultiplier(float multiplier)
        {
            _speedMultiplier = Mathf.Max(1f, multiplier);
        }
        
        //Должен быть контролелр бафов. В енаме хранятся типы бафов, где находится инфа о времени бафа. На каждый типа бафа есть свой класс. В котором старт стоп и апдейт.
        //Три класса: баф коллектбл, бафф контроллер и просто баф (бафХандлер)
        //Баф коллектбл при сборе вызывает бафф контроллер -> бафф контроллер обновляет таймеры, запускает и останавливает баффы -> бафф контроллер вызывает методы старт/стоп и апдейт
        //у баффхендлера -> в бафф хендлере логика самого баффа. Спид в чанксервисе = бейзспид * мультиплаер. Бафнутая скорость -> иультиплаер = чему-то. Небаффнутая -> мультиплаер = 1
    }
}
