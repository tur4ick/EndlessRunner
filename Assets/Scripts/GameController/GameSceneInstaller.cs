using Zenject;

namespace GameController
{
    public class GameSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        }
    }
}