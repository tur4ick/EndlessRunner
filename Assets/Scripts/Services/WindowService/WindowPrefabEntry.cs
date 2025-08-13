using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class WindowPrefabEntry
    {
        public WindowType WindowType;
        public BaseWindow prefab;
    }
}