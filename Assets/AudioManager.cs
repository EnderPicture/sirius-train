using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

	private List <String> nowPlaying = new List<String>();

	public static AudioManager instance;

	private float fadeSpeed = 0.0005f;

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
      }
    }

    public void Play(string name) {
		  Sound s = Array.Find(sounds, sound => sound.name == name);
		  if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		  }
		  if (!s.source.isPlaying) {
			Debug.Log("adding " + s.name + " to nowPlaying list");
			nowPlaying.Add(s.name);
			s.source.Play();
						/*
			foreach (String str in nowPlaying) {
				Debug.Log("now in nowPlaying: " + str);
			}
			*/
		  } else {
				//if it's already playing, fade away
				if (nowPlaying.Contains(s.name)){
					//Debug.Log("this track is already playing!");
					Debug.Log("stopping track");
					//s.source.Stop();
					fadeAway(s.name);
				}
		  }
    }

	public void Play(string name, float inputVolume, float inputPitch) {
		 Sound s = Array.Find(sounds, sound => sound.name == name);
		  if (s == null) {
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		  }
		 
		  if (!s.source.isPlaying) {
			  s.source.volume = inputVolume;
			  s.source.pitch = inputPitch;
			  s.source.Play();
		  } 

	}
	
	public void Stop(string name) {
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Sound: " + name + "not found!");
			return;
		}

		s.source.Stop();

	}

	public void fadeAway(string name) {
		//HAVE TO FIND THE ADDED AUDIO SOURCE WITH THE MATCHING SOUND AND ADJUST THAT INSTEAD
		Debug.Log("fading away " + name);

	
		AudioSource[] allSources = this.GetComponents<AudioSource>();
		foreach (AudioSource audioSource in allSources) {
			if (audioSource.clip.name == name) {
				Debug.Log("success, fading away audio");
				while (audioSource.volume > 0) {
					Debug.Log("audioSource.volume: " + audioSource.volume);
					audioSource.volume -= fadeSpeed * Time.deltaTime;
				}
				audioSource.Stop();
				Sound s = Array.Find(sounds, sound => sound.name == name);
				audioSource.volume = s.source.volume;
			} else {
				Debug.Log ("Couldn't find audioSource with that name");
			}
			//Debug.Log(audioSource.clip);
		}
	

		/*
		Sound s = Array.Find(sounds, sound => sound.name == name);
		while (s.volume > 0) {
			//Debug.Log(name + " s.volume: " + s.volume);
			//s.volume -= 0.0000002f;
			//s.volume -= 0.015f;
			s.volume -= fadeSpeed * Time.deltaTime;
			//Debug.Log(name + " s.volume: after subtraction " + s.volume);
		}
		
		//Debug.Log("s.source.volume below 0, stopping");
		s.source.Stop();
		s.volume = s.source.volume;
		//Debug.Log("s.volume reset: " + s.volume);
		*/
	
	}

	public void fadeAway(AudioSource audioSource) {
		Debug.Log("fading away by audioSource " + name);
		audioSource.volume -= fadeSpeed * Time.deltaTime;
	}

	public bool checkIfPlaying(string name) {
		AudioSource[] allSources = this.GetComponents<AudioSource>();
		foreach (AudioSource audioSource in allSources) {
			if (audioSource.clip.name == name) {
				Debug.Log("successfully found audio source");
				if (audioSource.isPlaying) 
					return true;
			} else {
				Debug.Log ("Couldn't find audioSource with that name");
				return false;
			}
		//Debug.Log(audioSource.clip);
		} 
		Debug.Log("something fucked up");
		return false;
	}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

		//check the list of currently playing sounds
		if (nowPlaying != null) {
			//Debug.Log("Entering nowPlaying");

			//check to make sure what is actually playing or not
			for (int i = 0; i < nowPlaying.Count; i++) {
				//Debug.Log("Entering for loop");
				String trackName = nowPlaying[i];
				Sound s = Array.Find(sounds, sound => sound.name == trackName) ;

				Debug.Log("s.volume: " + s.volume);
				//s.volume = s.source.volume;

				//stop sounds that are not playing, remove them from the list of playing sounds
				if (!s.source.isPlaying) {
					Debug.Log("source is not playing: " + trackName + ", removing from nowPlaying");
					s.source.Stop();
					nowPlaying.RemoveAt(i);
				}				
				
			}


			/*
			foreach (String trackName in nowPlaying) {
				Debug.Log("Entering foreach");
				Sound s = Array.Find(sounds, sound => sound.name == trackName);

				if (!s.source.isPlaying) {
					Debug.Log("source is not playing");
					s.source.Stop();
				}
			}
			*/
			
		}
		foreach (string str in nowPlaying) {
			Debug.Log("Now Playing: " + str);
		}
		
		Debug.Log("Number of sounds playing: " + nowPlaying.Count);
	

    }
}
