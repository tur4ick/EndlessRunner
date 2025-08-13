using System.Collections.Generic;
using UnityEngine;

namespace Services.ChunkService
{
    [CreateAssetMenu(menuName = "Configs/ChunkPrefabMap")]
    public class ChunkPrefabMap : ScriptableObject
    {
        public List<GameObject> ChunkPrefabs;
    }
}