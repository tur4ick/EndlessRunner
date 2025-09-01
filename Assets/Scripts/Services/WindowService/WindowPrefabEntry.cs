using System;

namespace Services.WindowService
{
    [Serializable]
    public class WindowPrefabEntry
    {
        public WindowType WindowType;
        public BaseWindow prefab;
    }
}