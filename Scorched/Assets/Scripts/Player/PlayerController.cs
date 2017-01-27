using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float countdown;
    public float morphCountdown;
    public float count;

    public Camera[] cams;                                                       //TODO: set Private

    public Slider absorBar;

    public GameObject PlayerGO;
    private PlayerHealthManager PlayerHealth;

    public GameObject PlayerStore;
    private PlayerStore Store;

    public bool debugMode = true;

    //Load Playermodels
    public GameObject LV0;
    public GameObject LV1;
    public GameObject LV2;

    public Image howToPlay;
    public Text howToPlayText;

    Animator anim;

    void Start()
    {
        PlayerHealth = PlayerGO.GetComponent<PlayerHealthManager>();
        Store = PlayerStore.GetComponent<PlayerStore>();
        count = 5f;

        anim = GetComponent<Animator>();

        //countdown = Store.countdown;
        morphCountdown = Store.morphCountdown;

        //Set First Cam at Game Start on
        if (Store.playerLevel == 0)
        {
            cams[0].enabled = true;
            cams[1].enabled = false;
            cams[2].enabled = false;
            Store.levelChange = false;

            PlayerHealth.maxHealth = Store.maxHealthLevel0;

            absorBar.maxValue = Store.neededExperienceForLevelUpTo1 + 1;
            
            if(howToPlay.enabled == false && howToPlayText.enabled == false)
            {
                howToPlay.enabled = true;
                howToPlayText.enabled = true;
            }
        }
    }

    void Update()
    {
        ButtonInput();

        checkIfLvlUp();

        resetLevel();

        if(Store.playerLevel <= 1)
        {
            absorBar.value = Store.playerExperience;
        }
        if(Store.playerLevel == 2)
        {
            absorBar.value = countdown;
        }
    }

    void LevelUP()
    {
        if(Store.playerLevel == 0)
        {
            Store.storePositionPlayerLV0 = GameObject.Find("PlayerLV1").transform.position;
        }
        if(Store.playerLevel == 1)
        {
            Store.storePositionPlayerLV1 = GameObject.Find("PlayerLV2").transform.position;
        }
        //if(Store.playerLevel == 2)
        //{
        //    Store.storePositionPlayerLV2 = GameObject.Find("PlayerLV3").transform.position;
        //}
        Store.playerLevel++;
        LevelUps();
    }

    void LevelDOWN()
    {
        Store.playerLevel--;
        Store.levelChange = true;
        LevelUps();
    }

    void LevelUps()
    {
        switch (Store.playerLevel)
        {
            case 0:
                cams[0].enabled = true;
                cams[1].enabled = false;
                cams[2].enabled = false;
                LV0.SetActive(true);
                LV1.SetActive(false);
                LV2.SetActive(false);
                PlayerHealth.maxHealth = PlayerHealth.startingHealth;
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                countdown = Store.countdown;
                absorBar.minValue = Store.startingExperience;
                absorBar.maxValue = Store.neededExperienceForLevelUpTo1 + 1;
                break;

            case 1:
                cams[0].enabled = false;
                cams[1].enabled = true;
                cams[2].enabled = false;
                LV1.SetActive(true);
                GameObject.Find("PlayerLV2").transform.position = Store.storePositionPlayerLV0;
                LV0.SetActive(false);
                LV2.SetActive(false);
                PlayerHealth.maxHealth = PlayerHealth.startingHealthLvl1;
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                countdown = Store.countdown;
                absorBar.minValue = Store.neededExperienceForLevelUpTo1;
                absorBar.maxValue = Store.neededExperienceForLevelUpTo2 + 1;
                break;

            case 2:
                cams[0].enabled = false;
                cams[1].enabled = false;
                cams[2].enabled = true;
                LV2.SetActive(true);
                GameObject.Find("PlayerLV3").transform.position = Store.storePositionPlayerLV1;
                LV0.SetActive(false);
                LV1.SetActive(false);
                absorBar.minValue = 0;
                absorBar.maxValue = countdown;
                PlayerHealth.maxHealth = PlayerHealth.startingHealthLvl2;
                PlayerHealth.currentHealth = PlayerHealth.maxHealth;
                break;

            default:
                break;
        }
    }

    void ButtonInput()
    {
        //Mouse & Keyboard
        if(debugMode == true)
        {
            if(Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                if(Store.playerLevel < 2)
                {
                    LevelUP();
                    Store.playerExperience = 0;
                }
            }

            if(Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                if(Store.playerLevel > 0)
                {
                    LevelDOWN();
                    Store.playerExperience = 0;
                }
            }
        }

        if(howToPlay.enabled == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                howToPlay.enabled = false;
                howToPlayText.enabled = false;
            }
        }

        //Controller
        if(howToPlay.enabled == true)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                howToPlay.enabled = false;
                howToPlayText.enabled = false;
            }
        }
        
        if(debugMode == true)
        {
            if(Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                if(Store.playerLevel < 2)
                {
                    LevelUP();
                    Store.levelChange = true;
                    Store.check = 0.5f;
                    Store.playerExperience = 100;
                }
            }

            if(Input.GetKeyDown(KeyCode.Joystick1Button2))
            {
                if(Store.playerLevel > 0)
                {
                    LevelDOWN();
                    Store.levelChange = true;
                    Store.check = 0.5f;
                    Store.playerExperience = 100;
                }
            }
            if(Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                if(cams[3].enabled == true)
                {
                    cams[3].enabled = false;
                }
                else
                {
                    cams[3].enabled = true;
                }
            }
        }
    }

    void checkIfLvlUp()
    {
        if (Store.playerExperience > Store.neededExperienceForLevelUp)
        {
            morphCountdown -= Time.deltaTime;
            if (morphCountdown <= 0)
            {
                LevelUP();
                Store.timeBetweenKills = Store.storeTimeBetweenKills;
                Store.levelChange = true;
                Debug.Log(Store.playerLevel);
                /*Store.playerExperience = 0; */    //Lösung Finden!!
            }
        }
    }

    void resetLevel()
    {
        if(Store.playerLevel == 0)
        {
            if(Store.timeBetweenKills > -0.1)
            {
                Store.timeBetweenKills -= Time.deltaTime;
            }
            if(Store.timeBetweenKills <= 0)
            {
                if (Store.playerExperience > -0.1)
                {
                        Store.playerExperience -= Time.deltaTime * 2.5f;
                }
            }
        }

        if(Store.playerLevel == 1)
        {
            if(Store.timeBetweenKills > -0.1)
            {
                Store.timeBetweenKills -= Time.deltaTime;
            }
            if(Store.timeBetweenKills < 0)
            {
                if (Store.playerExperience > Store.neededExperienceForLevelUpTo1 - 0.1)
                {
                    Store.playerExperience -= Time.deltaTime * 2.5f;
                    count = 5f;
                }
                if(Store.playerExperience < Store.neededExperienceForLevelUpTo1)
                { 
                    count -= Time.deltaTime;
                    //Debug.Log(count);
                    if(count <= 0)
                    {
                        Store.storePositionPlayerLV1 = GameObject.Find("PlayerLV2").transform.position;
                        LevelDOWN();
                        GameObject.Find("PlayerLV1").transform.position = Store.storePositionPlayerLV1;
                        count = 5f;
                    }
                }
            }
        }
        if(Store.playerLevel == 2)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0)
            {
                Store.storePositionPlayerLV2 = GameObject.Find("PlayerLV3").transform.position;
                LevelDOWN();
                LevelDOWN();
                GameObject.Find("PlayerLV1").transform.position = Store.storePositionPlayerLV2;
            }
        }
    }
}