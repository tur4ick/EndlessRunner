using System;
using UnityEngine;
using Zenject;

namespace Services.CoinService
{
    public class DistanceService : ITickable
    {
        [Inject] private readonly GameDataService.GameDataService _saveDataService;
        [Inject] private readonly Services.ChunkService.ChunkService _chunkService;
        
        public float SessionDistance { get; private set; }
        
        public float BestDistance
        {
            get
            {
                return _saveDataService.Data.BestDistance;
            }
        }
        public event Action<float> OnDistanceChanged;
        private bool _wasRunningLastTick;
        public void Tick()
        {
            bool isRunning = _chunkService.IsRunning;
            
            if (isRunning && !_wasRunningLastTick)
            {
                SessionDistance = 0f;
                OnDistanceChanged?.Invoke(SessionDistance);
            }
            
            if (isRunning)
            {
                SessionDistance += _chunkService.Speed * Time.deltaTime;
                OnDistanceChanged?.Invoke(SessionDistance);
            }
            
            if (SessionDistance > _saveDataService.Data.BestDistance)
            {
                _saveDataService.Data.BestDistance = SessionDistance;
                _saveDataService.Save();
            }
            
            
            _wasRunningLastTick = isRunning;
        }
        
        public void ResetDistance()
        {
            SessionDistance = 0f;
            OnDistanceChanged?.Invoke(SessionDistance);
        }
    }
}