using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public AudioClip SoundToPlay1;
    public AudioClip SoundToPlay2;
    public AudioSource source1;
    public AudioSource source2;
    public Animator anim;

    void Awake()
    {
        //source = GetComponent<AudioSource>();
        AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        anim = gameObject.GetComponent<Animator>();
        source1 = audioSources[0];
        source2 = audioSources[1];
    }

    /*
    void playSound()
    {
        Debug.Log("Playing sound: " + SoundToPlay);
        source.PlayOneShot(SoundToPlay1);
    }
     * */
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //playSound();
            anim.Play("sound");
            //anim.Play("sound 1");
        }
    }

    void playSound1()
    {
        //Debug.Log("Playing sound: " + SoundToPlay1);
        //later connect AudioManager2 to use the play sound function that can adjust the pitch
       // if (!source1.isPlaying) {
		//	source1.Play();		
		//}
		source1.PlayOneShot(SoundToPlay1);
    }

    void playSound2()
    {
       //Debug.Log("Playing sound: " + SoundToPlay2);
        //later connect AudioManager2 to use the play sound function that can adjust the pitch
		//if (!source2.isPlaying) {
		//	source2.Play();
		//}
        source2.PlayOneShot(SoundToPlay2);
    }

    void foo()
    {
		//Debug.Log("foo");
    }
}
