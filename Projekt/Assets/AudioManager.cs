using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] Sounds;

    // Start is called before the first frame update
    void Awake() {
        foreach (Sound s in Sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string name) {
       Sound s = Array.Find(Sounds, Sound => Sound.name == name);
        s.source.Play();
    }
}
