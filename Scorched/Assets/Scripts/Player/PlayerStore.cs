using UnityEngine;
using System.Collections;

public class PlayerStore : MonoBehaviour {

    [Header("Player")]
    public int playerLevel;
    public float playerExperience;

    public float startingExperience;
    public float neededExperienceForLevelUp;
    public float neededExperienceForLevelUpTo1;
    public float neededExperienceForLevelUpTo2;

    public float maxHealthLevel0;
    public float maxHealthLevel1;
    public float maxHealthLevel2;

    public Vector3 storePositionPlayerLV0;
    public Vector3 storePositionPlayerLV1;
    public Vector3 storePositionPlayerLV2;

    public float countdown;
    public float morphCountdown;

    public float timeBetweenKills = 3f;
    public float storeTimeBetweenKills;

    public float check = 0.5f;

    public bool levelChange;

    public GameObject blood;

    [Header("Camera")]
    public float shakeIntensity;
    public float shakeTimer;

    // Use this for initialization
    void Start ()
    {
        storeTimeBetweenKills = timeBetweenKills;
        startingExperience = 0;

        blood = GameObject.Find("BloodImage");
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}   