using UnityEngine;
using Zenject;

namespace Services.AudioService
{
    public class AudioServiceInstaller : MonoInstaller
    {
        [SerializeField] private AudioConfig _config;

        public override void InstallBindings()
        {
            var root = new GameObject("AudioRoot");
            Object.DontDestroyOnLoad(root);

            var musicSource = root.AddComponent<AudioSource>();
            var sfxSource   = root.AddComponent<AudioSource>();
            
            if (_config.musicGroup) musicSource.outputAudioMixerGroup = _config.musicGroup;
            if (_config.sfxGroup)   sfxSource.outputAudioMixerGroup   = _config.sfxGroup;
            
            Container.Bind<AudioConfig>().FromInstance(_config).AsSingle();
            Container.Bind<AudioService>().AsSingle().WithArguments(musicSource, sfxSource, _config).NonLazy();

        }
    }
}