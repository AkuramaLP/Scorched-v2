using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
    public Transform canvas;

    void Start()
    {

    }

	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Return))
        {
            Pause();
        }

        if(Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            canvas.gameObject.SetActive(true);
        }
        if (canvas.gameObject.activeInHierarchy == true)
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
