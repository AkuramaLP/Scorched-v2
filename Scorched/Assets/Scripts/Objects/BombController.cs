using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

    AudioSource Audio;

	// Use this for initialization
	void Start () {
        Audio = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
        Audio.Play();
    }
}
