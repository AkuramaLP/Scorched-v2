using UnityEngine;
using System.Collections;

public class AudioManagerCheck : MonoBehaviour {

    public GameObject AudioManager;

	void Start () {
        if (FindObjectOfType<AudioManager>())
            return;
        else
            Instantiate(AudioManager, transform.position, transform.rotation);
	}
	
	
	void Update () {
	
	}
}
