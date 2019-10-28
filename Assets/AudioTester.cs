using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
   
	//using this to test audio without disrupting any other assets
    
    void Update()
    {
		//if (Input.GetKeyDown("space")){
			//FindObjectOfType<AudioManager>().Play("farWhistle");
		//}

		if (Input.GetKeyDown("space")){
		//play - name/volume/pitch
			FindObjectOfType<AudioManager>().Play("farWhistle", Random.Range(0.4f, 0.6f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad0)){
			AudioManager audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
			AudioSource[] allSources = audioManager.GetComponents<AudioSource>();
			
			foreach (AudioSource audioSource in allSources) {
				if (audioSource.clip.name == "ambient1") {	
					if (audioSource.isPlaying) {
						Debug.Log("it's playing already, stopping now");
						//audioManager.fadeAway("ambient1");
						//audioManager.fadeAway(audioSource);
						audioSource.volume -= 0.05f * Time.deltaTime;
					} else {
						Debug.Log("it's not playing, playing now");
						FindObjectOfType<AudioManager>().Play("ambient1");
					}
					//correct audio clip was found
					//bool isPlaying = audioManager.checkIfPlaying("ambient1");
					//if (!isPlaying) {
						//FindObjectOfType<AudioManager>().Play("ambient1");
					//}
				}
			}

			

			/*

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
			
			if (notPlaying) {
				FindObjectOfType<AudioManager>().Play("ambient1");
			} else {
				fadeaway;
			}
			*/
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)){
			FindObjectOfType<AudioManager>().Play("mediumWhistle", Random.Range(0.4f, 0.6f), Random.Range(0.8f, 1.1f));
		}

        if (Input.GetKeyDown(KeyCode.Keypad2)){
			FindObjectOfType<AudioManager>().Play("pressure1", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad3)){
			FindObjectOfType<AudioManager>().Play("pressure1a", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad4)){
			FindObjectOfType<AudioManager>().Play("pressure2", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad5)){
			FindObjectOfType<AudioManager>().Play("pressure3", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad6)){
			FindObjectOfType<AudioManager>().Play("pressure4", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad7)){
			FindObjectOfType<AudioManager>().Play("pressure5", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad8)){
			FindObjectOfType<AudioManager>().Play("pressure6", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
		}

		if (Input.GetKeyDown(KeyCode.Keypad9)){
			//trying to make this a toggle, but needs to check if its already playing. cant get it to work though
			/*bool bellIsPlaying;
			GameObject AudioManager = GameObject.FindGameObjectWithTag("AudioManager");
			bellIsPlaying = AudioManager.playingCheck("bells");	
			if (bellIsPlaying) {
				FindObjectOfType<AudioManager>.Stop("bells");
			} else {
				FindObjectOfType<AudioManager>.Play("bells");
			}*/
			FindObjectOfType<AudioManager>().Play("bells");
			
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)){
			FindObjectOfType<AudioManager>().Play("shovel1");
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)){
			FindObjectOfType<AudioManager>().Play("shovel2");
		}	

    }
}
