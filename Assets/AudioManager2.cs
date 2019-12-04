using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{

	public Animator anim;
	public Animation wheelLoop;

	private float trainSpeed;
	//AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
	public AudioSource source1;
  public AudioSource source2;
	public AudioSource source3;
	public AudioSource source4;

	public int level = 1;

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
				// Debug.Log("pressureSources = null!");
			}
		}

		if (name == "train") {
			AudioSource[] trainSources = this.transform.Find("aTrainManager").GetComponents<AudioSource>();
			if (trainSources != null) {
				// Debug.Log(number);
				Debug.Log(trainSources.Length);
				// Debug.Log(trainSources[number].isPlaying);
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
				// Debug.Log("trainSources = null!");
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
				// Debug.Log("playerSources = null!");
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
				// Debug.Log("ambientSources = null!");
			}
		}

	}

	public void PlayWheelLoop(float speed) {

		trainSpeed = speed;
		anim.speed = speed;
		// Debug.Log(speed);

		//if level 3 (steam train)
		if(level == 3) {
			//steam
			//set animation volume and pitch
			// Debug.Log("source1.pitch" + source1.pitch);
			// Debug.Log("source2.pitch" + source2.pitch);
			// Debug.Log("source1.volume " + source1.volume);
			// Debug.Log("source2.volume " + source2.volume);
			//play animation
			anim.Play("dieselSound");
			anim.Play("sound");
		}

		//else if level 2 (diesel train)
		if (level == 2) {
			//set animation volume and pitch
			// Debug.Log("source3.pitch " + source3.pitch);
			// Debug.Log("source3.volume " + source3.volume);
			//play animation
			anim.Play("dieselSound");

				// Debug.Log("source1.pitch" + source1.pitch);
			// Debug.Log("source2.pitch" + source2.pitch);
			// Debug.Log("source1.volume " + source1.volume);
			// Debug.Log("source2.volume " + source2.volume);
			//play animation
			anim.Play("sound");
		}

		//else if level 1 (electric train)

		if (level == 1) {
			//set animation volume and pitch
			// Debug.Log("source4.pitch " + source4.pitch);
			// Debug.Log("source4.volume " + source4.volume);
			//play animation
			anim.Play("electricSound");
			anim.Play("sound");
		}

	}

	public void playWheel1() {
		if (!source1.isPlaying) {
			source1.pitch = trainSpeed*0.55f;
			source1.volume = trainSpeed*0.75f;
			//set maximum values on the pitch and volume
			if (source1.pitch >= 1.4f) {
				source1.pitch = 1.4f;
			}
			if (source1.pitch <= 0.6f) {
				source1.pitch = 0.6f;
			}

			// if (source1.volume <= 0.5f) {
			// 	source1.volume = 0.5f;
			// }

			source1.Play();
		}
	}

	public void playWheel2() {
		if (!source2.isPlaying) {
			source2.pitch = trainSpeed*0.55f;
			source2.volume = trainSpeed*0.75f;
			//set maximum values on the pitch and volume
			if (source2.pitch >= 1.4f) {
				source2.pitch = 1.4f;
			}
			if (source2.pitch <= 0.6f) {
				source2.pitch = 0.6f;
			}

			// if (source2.volume <= 0.5f) {
			// 	source2.volume = 0.5f;
			// }

			source2.Play();
		}
	}

	public void playWheel1(float inputVolume) {
		if (!source1.isPlaying) {
			source1.pitch = trainSpeed*0.55f;
			source1.volume = inputVolume;
			//set maximum values on the pitch and volume
			if (source1.pitch >= 1.4f) {
				source1.pitch = 1.4f;
			}
			if (source1.pitch <= 0.6f) {
				source1.pitch = 0.6f;
			}

			// if (source1.volume <= 0.5f) {
			// 	source1.volume = 0.5f;
			// }

			source1.Play();
		}
	}

	public void playWheel2(float inputVolume) {
		if (!source2.isPlaying) {
			source2.pitch = trainSpeed*0.55f;
			source2.volume = inputVolume;
			//set maximum values on the pitch and volume
			if (source2.pitch >= 1.4f) {
				source2.pitch = 1.4f;
			}
			if (source2.pitch <= 0.6f) {
				source2.pitch = 0.6f;
			}

			// if (source2.volume <= 0.5f) {
			// 	source2.volume = 0.5f;
			// }

			source2.Play();
		}
	}

	public void playDieselLoop() {
		source3.pitch = trainSpeed*0.55f;
		source3.volume = trainSpeed*0.75f;

		if (trainSpeed <= 0.0) {
			source3.volume = 0.0f;
		} else if (trainSpeed > 0.0) {
			source3.volume = trainSpeed*0.75f;
		}

		if (source3.pitch >= 1.4f) {
			source3.pitch = 1.4f;
		}
		if (source3.pitch <= 0.6f) {
			source3.pitch = 0.6f;
		}

		if (source3.volume <= 0.5f) {
			source3.volume = 0.5f;
		}

		if(!source3.isPlaying) {
			source3.Play();
		}

		playWheel1();
		playWheel2();
	}

	public void playElectricLoop() {
		source1.volume = trainSpeed*0.35f;
		source2.volume = trainSpeed*0.35f;

		source4.pitch = trainSpeed*0.55f;
		source4.volume = trainSpeed*0.75f;

		if (source4.pitch >= 1.4f) {
			source4.pitch = 1.4f;
		}
		if (source4.pitch <= 0.6f) {
			source4.pitch = 0.6f;
		}

		if (source4.volume <= 0.5f) {
			source4.volume = 0.5f;
		}

		if (trainSpeed <= 0.0) {
			source4.volume = 0.0f;
		} else if (trainSpeed > 0.0) {
			source4.volume = trainSpeed*0.75f;
		}

		if(!source4.isPlaying) {
			source4.Play();
		}

		playWheel1(source1.volume);
		playWheel2(source2.volume);
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
			// Debug.Log("Something happened in fade() of AudioManager2");
		}

	}

	public void setLevel(int levelToSet) {
		// Debug.Log("Level before: " + level);
		level = levelToSet;
		// Debug.Log("Level after: " + level);

		if (level == 1 || level == 2) {
			FindObjectOfType<AudioManager2>().Play("train", 2);
		}

		if (level == 3) {
			FindObjectOfType<AudioManager2>().Play("train", 5);
		}

	}

	public void setSpeed(float speedToSet) {
		trainSpeed = speedToSet;
	}

	void Awake() {
		anim = gameObject.GetComponent<Animator>();
		//source1 = audioSources[0];
        //source2 = audioSources[1];
		source1.volume = 0.5f;
		source2.volume = 0.5f;

	}

	// Start is called before the first frame update
    void Start()
    {
        trainSpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayWheelLoop(trainSpeed);


		//Debug.Log(trainSpeed);
		if(Input.GetKeyDown("i")) {
			// Debug.Log("speed up");
			trainSpeed+=0.2f;
		} else if (Input.GetKeyDown("o")){
			// Debug.Log("speed down");
			trainSpeed-=0.2f;
		}

    }
}
