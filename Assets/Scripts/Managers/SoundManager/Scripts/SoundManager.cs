using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class SoundManager : MonoBehaviour
    {
        public bool isMuted = false;
        public Sound[] sounds;
        public static SoundManager Instance;
        private Dictionary<string, Sound> soundLookup;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            //isActive = ESP.DataManager.DataManager.Instance.settings.isSoundActive;
            soundLookup = new Dictionary<string, Sound>();
            foreach (var s in sounds)
            {
                if (soundLookup.ContainsKey(s.Name))
                {
                    Debug.LogWarning($"Duplicate sound name detected: {s.Name}. Only the first entry will be used.");
                    continue;
                }

                InitializeSoundSource(s);
                soundLookup.Add(s.Name, s);
            }
        }
        private void Start()
        {
            PlayLoop("BGMusic");
        }
        public void Play(string name)
        {
            Sound s = GetSound(name);
            if (s == null)
            {
                return;
            }
            if (s.source.isPlaying == false)
            {
                s.source.PlayOneShot(s.Clip);
            }

        }
        public void PlayLoop(string name)
        {
            Sound s = GetSound(name);
            if (s == null)
            {
                return;
            }
            if (s.source.isPlaying == false)
            {
                s.source.Play();
            }

        }
        public void PlayOneShot(string name)
        {
            Sound s = GetSound(name);
            if (s == null)
            {
                return;
            }
            s.source.PlayOneShot(s.Clip);
        }
        public void PlayOneShot(string name,float pitch)
        {
            Sound s = GetSound(name);
            if (s == null)
            {
                return;
            }

            s.source.pitch = pitch;
            s.source.PlayOneShot(s.Clip);
        }
        public void Stop(string name)
        {
            Sound s = GetSound(name);
            if (s == null)
            {
                return;
            }
            s.source.Stop();


        }
        public void SwitchAllSounds()
        {
            isMuted = !isMuted;
            //ESP.DataManager.DataManager.Instance.settings.SwitchSounds();
            foreach (var item in soundLookup.Values)
            {
                ApplyMuteState(item);
            }

        }

        private void InitializeSoundSource(Sound sound)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.Clip;
            sound.source.volume = sound.Volume;
            sound.source.pitch = sound.Pitch;
            sound.source.loop = sound.Loop;
            ApplyMuteState(sound);
        }

        private void ApplyMuteState(Sound sound)
        {
            if (sound == null || sound.source == null)
            {
                return;
            }

            sound.source.mute = isMuted || sound.Mute;
        }

        private Sound GetSound(string name)
        {
            if (string.IsNullOrEmpty(name) || soundLookup.TryGetValue(name, out Sound sound) == false)
            {
                Debug.LogWarning($"Sound not found: {name}");
                return null;
            }

            return sound;
        }

    }
