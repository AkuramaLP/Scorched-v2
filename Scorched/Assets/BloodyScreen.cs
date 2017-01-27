using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodyScreen : MonoBehaviour {

    public Color blood = Color.red;
    public Color test;

    public Image im;

	// Use this for initialization
	void Start ()
    {
        im = GetComponent<Image>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
