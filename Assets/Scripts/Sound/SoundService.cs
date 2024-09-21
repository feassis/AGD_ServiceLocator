using ServiceLocator.UI;
using System;
using UnityEngine;

namespace ServiceLocator.Sound
{
    public class SoundService : MonoBehaviour
    {
        public static SoundService Instance { get; private set; }

        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource backgroundMusic;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                Debug.LogError($"Trying to crate second singleton");
                return;
            }

            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }

        private void Start()
        {
            PlaybackgroundMusic(SoundType.BackgroundMusic, true);
        }

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.loop = loopSound;
                audioEffects.clip = clip;
                audioEffects.PlayOneShot(clip);
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private void PlaybackgroundMusic(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                backgroundMusic.loop = loopSound;
                backgroundMusic.clip = clip;
                backgroundMusic.Play();
            }
            else
                Debug.LogError("No Audio Clip selected.");
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(soundScriptableObject.audioList, item => item.soundType == soundType);
            if (sound.audio != null)
                return sound.audio;
            return null;
        }
    }
}