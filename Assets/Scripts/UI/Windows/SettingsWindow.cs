using System;
using Services.AudioService;
using Services.WindowService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

namespace UI.Windows
{
    public class SettingsWindow : BaseWindow
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;
        
        [Inject] private AudioService _audioService;
        [Inject] private AudioConfig _audioConfig;
        [Inject] private WindowService _windowService;
        
        private const string MusicParam = "MusicVolume";
        private const string SfxParam   = "SfxVolume";
        
        private bool _applying;

        private void Awake()
        {
            _backButton.onClick.AddListener(OnBack);
            _musicSlider.onValueChanged.AddListener(OnMusicChanged);
            _sfxSlider.onValueChanged.AddListener(OnSfxChanged);
        }

        private void OnEnable()
        {
            _applying = true;

            _musicSlider.minValue = 0f;
            _musicSlider.maxValue = 1f;
            _sfxSlider.minValue   = 0f;
            _sfxSlider.maxValue   = 1f;

            _musicSlider.SetValueWithoutNotify(ReadLinear01FromMixer(_audioConfig.musicGroup, MusicParam, 1f));
            _sfxSlider.SetValueWithoutNotify(ReadLinear01FromMixer(_audioConfig.sfxGroup,   SfxParam,   1f));
            
            _audioService.SetMusicVolume(_musicSlider.value);
            _audioService.SetSfxVolume(_sfxSlider.value);

            _applying = false;
        }

        private void OnDisable()
        {
            _applying = false;
        }

        private void OnMusicChanged(float value)
        {
            if (_applying) return;
            _audioService.SetMusicVolume(value);
        }

        private void OnSfxChanged(float value)
        {
            if (_applying) return;
            _audioService.SetSfxVolume(value);
        }

        private void OnBack()
        {
            _windowService.Close(WindowType.Settings);
        }

        private static float ReadLinear01FromMixer(AudioMixerGroup group, string param, float fallback)
        {
            if (group == null) return fallback;

            AudioMixer mixer = group.audioMixer;
            if (mixer == null) return fallback;
            
            float db;
            bool ok = mixer.GetFloat(param, out db);
            if (!ok) return fallback;
            
            if (db <= -80f) return 0f;
            float linear = Mathf.Pow(10f, db / 20f);
            if (linear < 0f) linear = 0f;
            if (linear > 1f) linear = 1f;
            return linear;
        }
    }
}