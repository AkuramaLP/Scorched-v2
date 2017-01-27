using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

    private bool doubleSpeed;
    private bool Shield;

    private bool PowerUpActive;

    private float PowerUpTimeCounter;

    private PlayerMovementController theSpeedManager;
    private PlayerHealthManager theShieldManager;

    private float normalSpeed;
    private float normalHealth;

    private float normalSpeed1;
    private float normalSpeed2;
    private float normalSpeed3;
    private float normalHealth1;
    private float normalHealth2;
    private float normalHealth3;

    public GameObject[] SpeedPowerups;
    public GameObject[] ShieldPowerups;
    public Text PowerUpText;
    public Text PowerUpText2;
    public Text PowerUpText3;

    public GameObject Playerlvl_1;
    public GameObject Playerlvl_2;
    public GameObject Playerlvl_3;

    private float LVL1Speed;
    private float LVL2Speed;
    private float LVL3Speed;

    private float LVL1Health;
    private float LVL2Health;
    private float LVL3Health;

    public float Speedmultiplikator1;
    public float Speedmultiplikator2;
    public float Speedmultiplikator3;

    AudioSource PickUpSound;

    void Start()
    {
        //theSpeedManager = FindObjectOfType<PlayerMovementController>();
        //theShieldManager = FindObjectOfType<PlayerHealthManager>();
        PowerUpText.text = "";
        PowerUpText2.text = "";
        //PowerUpText3.text = "";
        PickUpSound = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (PowerUpActive)
        {
            PowerUpTimeCounter -= Time.deltaTime;

            if (doubleSpeed)
            {
                if (Playerlvl_1.activeInHierarchy == true)
                {
                    Playerlvl_1.GetComponent<PlayerMovementController>().moveSpeed = LVL1Speed * Speedmultiplikator1;
                    PowerUpText.text = "Increased Speed: " + PowerUpTimeCounter + "s";
                }

                if (Playerlvl_2.activeInHierarchy == true)
                {
                    Playerlvl_2.GetComponent<PlayerMovementControllerv2>().moveSpeed = LVL2Speed * Speedmultiplikator2;
                    PowerUpText.text = "Increased Speed: " + PowerUpTimeCounter + "s";
                }

                if (Playerlvl_3.activeInHierarchy == true)
                {
                    Playerlvl_3.GetComponent<PlayerMovementController>().moveSpeed = LVL3Speed * Speedmultiplikator3;
                    PowerUpText.text = "Increased Speed: " + PowerUpTimeCounter + "s";
                }

                //theSpeedManager.moveSpeed = normalSpeed * Speedmultiplikator1;
                //PowerUpText.text = "Double Speed: " + PowerUpTimeCounter + "s";
            }

            if (Shield)
            {
                if (Playerlvl_1.activeInHierarchy == true)
                {
                    Playerlvl_1.GetComponent<PlayerHealthManager>().currentHealth = Mathf.Infinity;
                    Playerlvl_1.GetComponent<PlayerHealthManager>().healthBar.value = Playerlvl_1.GetComponent<PlayerHealthManager>().currentHealth;
                    PowerUpText2.text = "Shield: " + PowerUpTimeCounter + "s";
                }

                if (Playerlvl_2.activeInHierarchy == true)
                {
                    Playerlvl_2.GetComponent<PlayerHealthManager>().currentHealth = Mathf.Infinity;
                    Playerlvl_2.GetComponent<PlayerHealthManager>().healthBar.value = Playerlvl_2.GetComponent<PlayerHealthManager>().currentHealth;
                    PowerUpText2.text = "Shield: " + PowerUpTimeCounter + "s";
                }

                if (Playerlvl_3.activeInHierarchy == true)
                {
                    Playerlvl_3.GetComponent<PlayerHealthManager>().currentHealth = Mathf.Infinity;
                    Playerlvl_3.GetComponent<PlayerHealthManager>().healthBar.value = Playerlvl_3.GetComponent<PlayerHealthManager>().currentHealth;
                    PowerUpText2.text = "Shield: " + PowerUpTimeCounter + "s";
                }

                //theShieldManager.currentHealth = Mathf.Infinity;
                //theShieldManager.healthBar.value = theShieldManager.currentHealth;
                //PowerUpText2.text = "Shield: " + PowerUpTimeCounter + "s";
            }

            if (PowerUpTimeCounter <= 0)
            {
                //theSpeedManager.moveSpeed = normalSpeed;
                //theShieldManager.currentHealth = normalHealth;
                //theShieldManager.healthBar.value = normalHealth;

                if (Playerlvl_1.activeInHierarchy == true)
                {
                    if (doubleSpeed)
                    {
                        Playerlvl_1.GetComponent<PlayerMovementController>().moveSpeed = LVL1Speed;
                    }

                    if (Shield)
                    {
                        Playerlvl_1.GetComponent<PlayerHealthManager>().currentHealth = LVL1Health;
                        Playerlvl_1.GetComponent<PlayerHealthManager>().healthBar.value = LVL1Health;
                    }
                }

                if (Playerlvl_2.activeInHierarchy == true)
                {
                    if (doubleSpeed)
                    {
                        Playerlvl_2.GetComponent<PlayerMovementControllerv2>().moveSpeed = LVL2Speed;
                    }

                    if (Shield)
                    {
                        Playerlvl_2.GetComponent<PlayerHealthManager>().currentHealth = LVL2Health;
                        Playerlvl_2.GetComponent<PlayerHealthManager>().healthBar.value = LVL2Health;
                    }
                }

                if (Playerlvl_3.activeInHierarchy == true)
                {
                    if (doubleSpeed)
                    {
                        Playerlvl_3.GetComponent<PlayerMovementController>().moveSpeed = LVL3Speed;
                    }

                    if (Shield)
                    {
                        Playerlvl_3.GetComponent<PlayerHealthManager>().currentHealth = LVL3Health;
                        Playerlvl_3.GetComponent<PlayerHealthManager>().healthBar.value = LVL3Health;
                    }
                }

                PowerUpText.text = "";
                PowerUpText2.text = "";
                //PowerUpText3.text = "";

                PowerUpActive = false;
                foreach (GameObject obj in SpeedPowerups)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in ShieldPowerups)
                {
                    obj.SetActive(true);
                }
            }
        }
    }


    public void ActivatePowerUp(bool Speed, bool shield, float time)
    {
        doubleSpeed = Speed;
        Shield = shield;
        PowerUpTimeCounter = time;
        PickUpSound.Play();

        if (Playerlvl_1.activeInHierarchy == true)
        {
            LVL1Speed = Playerlvl_1.GetComponent<PlayerMovementController>().moveSpeed;
            LVL1Health = Playerlvl_1.GetComponent<PlayerHealthManager>().currentHealth;
            //normalSpeed1 = LVL1Speed;
            //normalHealth1 = LVL1Health;
        }

        if (Playerlvl_2.activeInHierarchy == true)
        {
            LVL2Speed = Playerlvl_2.GetComponent<PlayerMovementControllerv2>().moveSpeed;
            LVL2Health = Playerlvl_2.GetComponent<PlayerHealthManager>().currentHealth;
            //normalSpeed2 = LVL2Speed;
            //normalHealth2 = LVL2Health;
        }

        if (Playerlvl_3.activeInHierarchy == true)
        {
            LVL3Speed = Playerlvl_3.GetComponent<PlayerMovementController>().moveSpeed;
            LVL3Health = Playerlvl_3.GetComponent<PlayerHealthManager>().currentHealth;
            //normalSpeed3 = LVL3Speed;
            //normalHealth3 = LVL3Health;
        }

        //normalSpeed = theSpeedManager.moveSpeed;
        //normalHealth = theShieldManager.currentHealth;

        PowerUpActive = true;
        foreach (GameObject obj in SpeedPowerups)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in ShieldPowerups)
        {
            obj.SetActive(false);
        }
    }

}
