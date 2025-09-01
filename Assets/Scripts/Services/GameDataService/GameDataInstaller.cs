using Services.CoinService;
using Zenject;

namespace Services.GameDataService
{
    public class GameDataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameDataService>().AsSingle();
            Container.Bind<CoinService.CoinService>().AsSingle();
            Container.BindInterfacesAndSelfTo<DistanceService>().AsSingle();
        }
    }
}
