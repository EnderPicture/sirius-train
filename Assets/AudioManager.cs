using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

	public static AudioManager instance;

    void Awake() {

		if (instance == null) 
			instance = this;
		else {
			Destroy(gameObject);
			return;
		}
	
		DontDestroyOnLoad(gameObject);		
	
		foreach (Sound s in sounds) {
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;
			s.source.loop = s.loop;
			s.source.outputAudioMixerGroup  = s.group;
			
			//JUST to test that the audio is working since I have no triggers right now
			//s.source.Play();
      }
    }

    public void Play(string name) {
		  Sound s = Array.Find(sounds, sound => sound.name == name);
		  if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		  }
		  s.source.Play();
    }

	public void Play(string name, float inputVolume, float inputPitch) {
		 Sound s = Array.Find(sounds, sound => sound.name == name);
		  if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		  }
		  s.source.volume = inputVolume;
		  s.source.pitch = inputPitch;
		  s.source.Play();
	}
	
	public void Stop(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + "not found!");
			return;
		}

		s.source.Stop();

	}

	public bool playingCheck(string name){
		Sound s = Array.Find(sounds, sound => sound.name == name);
		//if (s == null) {
			//Debug.LogWarning("Sound: " + name + "not found!");
			//return null;
	//	}

		if(s.source.isPlaying) {
			Debug.Log("Playing");
			return true;
		} else if(!s.source.isPlaying) {
			Debug.Log("Not Playing");
			return false;
		} else {
			Debug.LogWarning("Playing check messed up");
			return false;
		}

	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
