using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource BGM;

	void Start () {
        DontDestroyOnLoad(gameObject);

        if(FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
	}
	
	void Update () {
	
	}

    public void ChangeBGM(AudioClip music)
    {
        if (BGM.clip.name == music.name)
            return;

        BGM.Stop();
        BGM.clip = music;
        BGM.Play();
    }
}
