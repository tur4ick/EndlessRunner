using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerSkinsConfig : ScriptableObject
    {
        public GameObject MovementPrefab;
        
        public List<GameObject> VisualsPrefabs = new List<GameObject>();
        
        public int SelectedIndex = 0;
        
        public GameObject GetSelectedVisuals()
        {
            if (VisualsPrefabs == null || VisualsPrefabs.Count == 0) return null;
            if (SelectedIndex < 0 || SelectedIndex >= VisualsPrefabs.Count) return null;
            return VisualsPrefabs[SelectedIndex];
        }
    }
}