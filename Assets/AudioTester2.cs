using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester2 : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetKeyDown(KeyCode.Keypad0)){
			//FindObjectOfType<AudioManager>().Play("pressure1", Random.Range(0.8f, 1.0f), Random.Range(0.8f, 1.1f));
			FindObjectOfType<AudioManager2>().Play("pressure", 0);
		}

		if (Input.GetKeyDown(KeyCode.Keypad1)){
			FindObjectOfType<AudioManager2>().Play("pressure", 1);
		}

		if (Input.GetKeyDown(KeyCode.Keypad2)){
			FindObjectOfType<AudioManager2>().Play("pressure", 2);
		}

		if (Input.GetKeyDown(KeyCode.Keypad3)){
			FindObjectOfType<AudioManager2>().Play("pressure", 3);
		}


		if (Input.GetKeyDown(KeyCode.Keypad4)){
			FindObjectOfType<AudioManager2>().Play("train", 0);
		}

		if (Input.GetKeyDown(KeyCode.Keypad5)){
			FindObjectOfType<AudioManager2>().Play("train", 1);
		}

		if (Input.GetKeyDown(KeyCode.Keypad6)){
			FindObjectOfType<AudioManager2>().Play("train", 2);
		}


		if (Input.GetKeyDown(KeyCode.Keypad7)){
			FindObjectOfType<AudioManager2>().Play("ambient", 0);
		}

		if (Input.GetKeyDown(KeyCode.Keypad8)){
			FindObjectOfType<AudioManager2>().Play("ambient", 1);
		}

		if (Input.GetKeyDown(KeyCode.Keypad9)){
			FindObjectOfType<AudioManager2>().Play("ambient", 2);
		}

		/*
		if (Input.GetKeyDown(KeyCode.Alpha1)){
			FindObjectOfType<AudioManager2>().Play("ambient", 3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)){
			FindObjectOfType<AudioManager2>().Play("ambient", 4);
		}
		*/


		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			FindObjectOfType<AudioManager2>().Play("player", 0);
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			FindObjectOfType<AudioManager2>().Play("player", 1);
		}


		if (Input.GetKeyDown(KeyCode.Alpha1)){
			FindObjectOfType<AudioManager2>().Play("ambient",3);
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)){
			//FindObjectOfType<AudioManager2>().fade()
		}
        
    }
}
