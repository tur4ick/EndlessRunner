using Buffs;
using UnityEngine;
using Zenject;

namespace Services.ChunkService
{
    public class ChunkInstaller : MonoInstaller
    {
        [SerializeField] private ChunkPrefabMap _chunkPrefabMap;
        [SerializeField] private Transform chunksRoot;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ChunkService>().AsSingle().WithArguments(_chunkPrefabMap, chunksRoot);
            Container.BindInterfacesAndSelfTo<BuffController>().AsSingle().NonLazy();
        }
    }
}
