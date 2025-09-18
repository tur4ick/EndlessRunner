using Character.Skins;
using Services.CoinService;
using UnityEngine;
using Zenject;

namespace Services.GameDataService
{
    public class GameDataInstaller : MonoInstaller
    {
        [SerializeField] private ModelPreviewService.ModelPreviewService _modelPreviewService;
        [SerializeField] private SkinsConfig _skinsConfig;
        [SerializeField] private InputSource _inputSource;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle();
            Container.Bind<CoinService.CoinService>().AsSingle();
            Container.BindInterfacesAndSelfTo<DistanceService>().AsSingle();
            Container.Bind<ModelPreviewService.ModelPreviewService>().FromComponentOn(_modelPreviewService.gameObject).AsSingle();
            Container.Bind<SkinsConfig>().FromInstance(_skinsConfig).AsSingle();
            Container.Bind<InputSource>().FromInstance(_inputSource).AsSingle();
        }
    }
}
