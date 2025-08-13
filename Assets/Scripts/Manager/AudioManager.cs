using System;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string Name;
        public AudioClip Clip;
        public float Volume;
        public bool Loop;

        [HideInInspector]
        public AudioSource Source;
    }

    [SerializeField]
    private Sound[] sounds;

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.loop = sound.Loop;
        }
    }

    private void Start()
    {
        Play("theme");
    }

    public void Play(string name, bool smoothStart = false)
    {
        Sound s = Array.Find(sounds, s => s.Name == name);
        if (s != null)
        {
            s.Source.Play();
            if (smoothStart)
            {
                float temp = s.Source.volume;
                s.Source.volume = 0f;
                s.Source.DOFade(temp, 2f);
            }
        }
    }

    public void Stop(string name, bool smoothStop = false)
    {
        Sound s = Array.Find(sounds, s => s.Name == name);
        if (s != null)
        {
            if (smoothStop)
            {
                float temp = s.Source.volume;
                s.Source.DOFade(0f, 2f).OnComplete(() =>
                {
                    s.Source.Stop();
                    s.Source.volume = temp;
                });
            }
            else
                s.Source.Stop();
        }
    }
}
