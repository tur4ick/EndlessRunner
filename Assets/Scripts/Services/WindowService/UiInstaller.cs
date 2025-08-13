using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Services
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField] private WindowPrefabMap _prefabMap;
        [SerializeField] private Transform _windowsRoot;
        
        public override void InstallBindings()
        {
            Container.Bind<WindowService>().AsSingle().WithArguments(Container, _prefabMap, _windowsRoot);
        }
    }
}