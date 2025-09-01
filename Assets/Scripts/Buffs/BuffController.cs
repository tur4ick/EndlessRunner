using System.Collections.Generic;
using Services.ChunkService;
using UnityEngine;
using Zenject;

namespace Buffs
{
    public class BuffController : ITickable
    {
        private readonly Dictionary<BuffType, Buff> _buffs = new();
        [Inject] private readonly ChunkService _chunkService;
        
        public void Apply(BuffType type, float amount, float duration)
        {
            float endTime = Time.time + duration;

            if (_buffs.TryGetValue(type, out var buff))
            {
                buff.EndTime = Mathf.Max(buff.EndTime, endTime);
                buff.Amount = amount;
            }
            else
            {
                _buffs[type] = new Buff{ EndTime = endTime, Amount = amount };
                StartBuff(type, amount);
            }
        }
        
        
        public void Tick()
        {
            float now = Time.time;
            
            foreach (var kvp in _buffs)
            {
                if (now >= kvp.Value.EndTime)
                {
                    StopBuff(kvp.Key);
                    _buffs.Remove(kvp.Key);
                    break;
                }
            }
        }
        
        private void StartBuff(BuffType type, float amount)
        {
            if (type == BuffType.Speed)
                _chunkService.SetSpeedMultiplier(amount);
        }

        private void StopBuff(BuffType type)
        {
            if (type == BuffType.Speed)
                _chunkService.SetSpeedMultiplier(1f);
        }

        public void StopAll()
        {
            _buffs.Clear();
            _chunkService.SetSpeedMultiplier(1f);
        }
    }
}