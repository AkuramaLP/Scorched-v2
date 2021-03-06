﻿using UnityEngine;
using System.Collections;

public class DetectControllMethod : MonoBehaviour
{
    public PlayerMovementController theMovement;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    //Detect Mouse Input
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            theMovement.useController = false;
        }

        if(Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f)
        {
            theMovement.useController = false;
        }

        //Detect Controller Input
        if(Input.GetAxisRaw("RHorizontal") != 0.0f || Input.GetAxisRaw("RVertical") != 0.0f)
        {
            theMovement.useController = true;
        }

        if(Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKey(KeyCode.Joystick1Button2) || Input.GetKey(KeyCode.Joystick1Button3) || Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Joystick1Button6) || Input.GetKey(KeyCode.Joystick1Button7) || Input.GetKey(KeyCode.Joystick1Button8) || Input.GetKey(KeyCode.Joystick1Button9) || Input.GetKey(KeyCode.Joystick1Button10))
        {
            theMovement.useController = true;
        }
	}
}
