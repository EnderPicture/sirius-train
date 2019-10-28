using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    
	public void Play(string name, int number) {

		if (name == "pressure") {
			AudioSource[] pressureSources = this.transform.Find("aPressureManager").GetComponents<AudioSource>();
			//check to make sure it's not empty
			if (pressureSources != null) {
				if(number > pressureSources.Length) {
					Debug.Log("Error: number to play > number of sources");
				}
				//check to see if it's already playing
				if (!pressureSources[number].isPlaying) {
					pressureSources[number].volume = Random.Range(0.8f, 1.0f);
					pressureSources[number].pitch = Random.Range(0.8f, 1.1f);
					pressureSources[number].Play();
				} else {
					pressureSources[number].Stop();
				}
				
			} else {
				Debug.Log("pressureSources = null!");
			}
		}

		if (name == "train") {
			AudioSource[] trainSources = this.transform.Find("aTrainManager").GetComponents<AudioSource>();
			if (trainSources != null) {
				if(number > trainSources.Length) {
					Debug.Log("Error: number to play > number of sources");
				}
				if (!trainSources[number].isPlaying) {
					trainSources[number].Play();
				} else {
					trainSources[number].Stop(); 
				}
				
			} else {
				Debug.Log("trainSources = null!");
			}
		} 

		if (name == "player") {
			AudioSource[] playerSources = this.transform.Find("aPlayerManager").GetComponents<AudioSource>();
			if (playerSources != null) {
				if(number > playerSources.Length) {
					Debug.Log("Error: number to play > number of sources");
				}
				if (!playerSources[number].isPlaying) {
					playerSources[number].Play();
				} else {
					playerSources[number].Stop();
				}
		
			} else {
				Debug.Log("playerSources = null!");
			}
		}

		if (name == "ambient") {
			AudioSource[] ambientSources = this.transform.Find("aAmbientManager").GetComponents<AudioSource>();
			if (ambientSources != null) {
				if (number > ambientSources.Length) {
					Debug.Log("Error: number to play > number of sources");
				}
				if (!ambientSources[number].isPlaying) {
					ambientSources[number].Play();
					ambientSources[number].loop = true;
				} else {
					ambientSources[number].loop = false;
					ambientSources[number].Stop();
				}
			} else {
				Debug.Log("ambientSources = null!");
			}
		}

	}

	/*
	public void fade (string name) {
		if (name == "pressure") {
			AudioSource[] pressureSources = this.transform.Find("aPressureManager").GetComponents<AudioSource>();
			AudioSource pressureSource = pressureSources[0];
			AudioMixer mixer = pressureSource.outputAudioMixer;
			//Audio.AudioMixerGroup mixerGroup = GetComponent<pressureSources[0]>().outputAudioMixerGroup;
			float decayingVolume = 1.0f;
			decayingVolume -= (Time.deltaTime*0.2f);
			mixer.SetFloat("volume", decayingVolume);

		}
	}
	*/
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
