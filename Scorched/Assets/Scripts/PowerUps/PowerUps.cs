using UnityEngine;
using System.Collections;

public class PowerUps : MonoBehaviour {

    public bool doubleSpeed;
    public bool Shield;

    public float PowerUpTime;

    private PowerUpManager thePowerUpManager;
    
	void Start ()
    {
        thePowerUpManager = FindObjectOfType<PowerUpManager>();
	}
	
	void Update ()
    {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerBig"))         //if(other.name == "Player") ZUKUNFS JONA DAMIT ARBEITEN ZUM TAUSCHEN!!!
        {
            thePowerUpManager.ActivatePowerUp(doubleSpeed, Shield, PowerUpTime);
            gameObject.SetActive(false);
        }
    }
}


