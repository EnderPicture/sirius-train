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
					//IF it's already playing, fade away the sound
					fade("pressure", number, 0.35f);
					//StartCoroutine(FadeAudioSource.StartFade(pressureSources[number], 0.35f, 0));
					//pressureSources[number].Stop();
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
					trainSources[number].volume = 1f;
					trainSources[number].Play();
				} else {
					//IF it's already playing, fade away the sound
					fade("train", number, 1.0f);
					//StartCoroutine(FadeAudioSource.StartFade(trainSources[number], 0.35f, 0));
					//trainSources[number].Stop(); 
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
					playerSources[number].volume = 1f;
					playerSources[number].Play();
				} else {
					//IF it's already playing, fade away the sound
					fade("player", number, 0.35f);
					//StartCoroutine(FadeAudioSource.StartFade(playerSources[number], 0.35f, 0));
					//playerSources[number].Stop();
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
					ambientSources[number].volume = 1f;
					ambientSources[number].Play();
					//ambientSources[number].loop = true;
				} else {
					//IF it's already playing, fade away the sound
					fade("ambient", number, 1.0f);
					//StartCoroutine(FadeAudioSource.StartFade(ambientSources[number], 0.35f, 0));
					//ambientSources[number].loop = false;
					//ambientSources[number].Stop();
				}
			} else {
				Debug.Log("ambientSources = null!");
			}
		}

	}

	public void fade(string name, int number, float durationToFade){
		
		if (name == "pressure") {
			AudioSource[] pressureSources = this.transform.Find("aPressureManager").GetComponents<AudioSource>();
			StartCoroutine(FadeAudioSource.StartFade(pressureSources[number], durationToFade, 0));
		} else if (name == "train") {
			AudioSource[] trainSources = this.transform.Find("aTrainManager").GetComponents<AudioSource>();
			StartCoroutine(FadeAudioSource.StartFade(trainSources[number], durationToFade, 0));		
		} else if (name == "ambient") {
			AudioSource[] ambientSources = this.transform.Find("aAmbientManager").GetComponents<AudioSource>();
			StartCoroutine(FadeAudioSource.StartFade(ambientSources[number], durationToFade, 0));		
		} else if (name == "player") {
			AudioSource[] playerSources = this.transform.Find("aPlayerManager").GetComponents<AudioSource>();
			StartCoroutine(FadeAudioSource.StartFade(playerSources[number], durationToFade, 0));				
		} else {
			Debug.Log("Something happened in fade() of AudioManager2");
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
