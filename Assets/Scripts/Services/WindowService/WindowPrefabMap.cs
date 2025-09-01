using System.Collections.Generic;
using UnityEngine;

namespace Services.WindowService
{
    [CreateAssetMenu(menuName = "Configs/WindowPrefabMap")]
    public class WindowPrefabMap : ScriptableObject
    {
        public List<WindowPrefabEntry> entries;

        public Dictionary<WindowType, BaseWindow> ToDictionary()
        {
            var dictionary = new Dictionary<WindowType, BaseWindow>();
            foreach (var entry in entries)
            {
                if (!dictionary.ContainsKey(entry.WindowType))
                {
                    dictionary.Add(entry.WindowType, entry.prefab);
                }
            }
            return dictionary;
        }
    }
}