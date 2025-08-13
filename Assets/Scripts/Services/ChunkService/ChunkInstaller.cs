using System.Collections;
using System.Collections.Generic;
using Services.ChunkService;
using UnityEngine;
using Zenject;

public class ChunkInstaller : MonoInstaller
{
    [SerializeField] private ChunkPrefabMap _chunkPrefabMap;
    [SerializeField] private Transform chunksRoot;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ChunkService>().AsSingle().WithArguments(_chunkPrefabMap, chunksRoot);
    }
}
