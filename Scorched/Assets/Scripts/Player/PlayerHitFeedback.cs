using UnityEngine;
using System.Collections;

public class PlayerHitFeedback : MonoBehaviour
{

    public float flashLength;
    private float flashCounter;
    private Renderer rend;
    private Color storedColor;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeColor()
    {
        if(flashCounter > 0)
        {
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)
            {
                rend.material.SetColor("_Color", storedColor);
            }
        }
    }

    public void visualFeedback()
    {
        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.red);
    }

}
