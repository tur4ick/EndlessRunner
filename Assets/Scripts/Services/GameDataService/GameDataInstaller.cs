using System.Collections.Generic;
using Services.GameDataService;
using UnityEngine;
using Zenject;
    public class GameDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle();
            Container.Bind<CoinService>().AsSingle();
        }
    }
