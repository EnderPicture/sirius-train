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
			FindObjectOfType<AudioManager>().Play("ambient1");
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
