using UnityEngine;
using UnityEngine.Audio;

namespace Services.AudioService
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "Configs/Audio Config")]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip menuMusic;
        public AudioClip gameMusic;
        
        public AudioMixerGroup musicGroup;
        public AudioMixerGroup sfxGroup;
        
        public AudioClip coinSound;
        public AudioClip buffSound;
        public AudioClip deathSound;
    }
}