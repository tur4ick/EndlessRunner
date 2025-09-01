using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services.WindowService
{
    public class WindowService
    {
        private readonly DiContainer _container;
        private readonly Dictionary<WindowType, BaseWindow> _prefabs;
        private readonly Transform _parent;
        private readonly Dictionary<WindowType, BaseWindow> _instances;

        public WindowService(DiContainer container, WindowPrefabMap prefabMap, Transform parent)
        {
            _container = container;
            _prefabs = prefabMap.ToDictionary();
            _parent = parent;
            _instances = new Dictionary<WindowType, BaseWindow>();
        }

        public T Create<T>(WindowType type) where T : BaseWindow
        {
            if (_instances.TryGetValue(type, out var existing))
            {
                return existing as T;
            }

            if (!_prefabs.TryGetValue(type, out var prefab))
            {
                return null;
            }

            var instance = _container.InstantiatePrefabForComponent<T>(prefab, _parent);
            _instances[type] = instance;
            return instance;
        }

        public void Create(WindowType type)
        {
            Create<BaseWindow>(type);
        }

        public void Close(WindowType type)
        {
            if (_instances.TryGetValue(type, out BaseWindow instance))
            {
                instance.Close();
                _instances.Remove(type);
            }
        }
    }
}
