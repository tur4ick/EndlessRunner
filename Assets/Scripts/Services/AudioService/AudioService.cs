using UnityEngine;
using UnityEngine.Audio;

namespace Services.AudioService
{
    public class AudioService 
    {
        private readonly AudioSource _musicSource;
        private readonly AudioSource _sfxSource;
        private readonly AudioConfig _config;
        private readonly AudioMixer _mixer;
        
        private const string MusicParameters = "MusicVolume";
        private const string SfxParameters   = "SfxVolume";

        public AudioService(AudioSource musicSource, AudioSource sfxSource, AudioConfig config)
        {
            _musicSource = musicSource;
            _sfxSource = sfxSource;
            _config = config;
            
            if (_config.musicGroup != null)
            {
                _musicSource.outputAudioMixerGroup = _config.musicGroup;
            }
            if (_config.sfxGroup != null)
            {
                _sfxSource.outputAudioMixerGroup = _config.sfxGroup;
            }
            
            
            if (_config.musicGroup != null)
            {
                _mixer = _config.musicGroup.audioMixer;
            }
            else if (_config.sfxGroup != null)
            {
                _mixer = _config.sfxGroup.audioMixer;
            }
            else
            {
                _mixer = null;
            }
            
            _musicSource.loop = true;
            _musicSource.playOnAwake = false;
            _sfxSource.playOnAwake = false;
        }
        
        public void PlayMenuMusic() => PlayMusic(_config.menuMusic, true);
        public void PlayGameMusic() => PlayMusic(_config.gameMusic, true);
        
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (!clip) return;
            if (_musicSource.clip == clip && _musicSource.isPlaying) return;

            _musicSource.Stop();
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }
        
        public void StopMusic()    => _musicSource.Stop();
        public void PauseMusic()   => _musicSource.Pause();
        public void UnPauseMusic() => _musicSource.UnPause();
        
        public void PlayCoin()  => PlaySFX(_config.coinSound);
        public void PlayBuff()  => PlaySFX(_config.buffSound);
        public void PlayDeath() => PlaySFX(_config.deathSound);
        
        public void PlaySFX(AudioClip clip)
        {
            if (!clip) return;
            _sfxSource.PlayOneShot(clip);
        }
        
        public void SetMusicVolume(float linear01)
        {
            if (_mixer == null) return;
            _mixer.SetFloat(MusicParameters, Linear01ToDb(linear01));
        }

        public void SetSfxVolume(float linear01)
        {
            if (_mixer == null) return;
            _mixer.SetFloat(SfxParameters, Linear01ToDb(linear01));
        }
        
        private static float Linear01ToDb(float linear)
        {
            linear = Mathf.Clamp01(linear);
            return linear > 0.0001f ? 20f * Mathf.Log10(linear) : -80f;
        }
    }
}