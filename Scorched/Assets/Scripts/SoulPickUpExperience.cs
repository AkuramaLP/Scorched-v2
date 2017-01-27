using UnityEngine;
using System.Collections;

public class SoulPickUpExperience : MonoBehaviour {

    public GameObject PlayerStore;
    private PlayerStore Store;

    public float exp;

    void Start () {
        PlayerStore = GameObject.Find("Store");
        Store = PlayerStore.GetComponent<PlayerStore>();
    }
	
	
	void Update () {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerBig"))
        {
            giveExperience();
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void giveExperience()
    {
        if (Store.playerLevel < 2)
        {
            if (Store.playerLevel == 0)
            {
                if (Store.playerExperience < Store.neededExperienceForLevelUpTo1)
                {
                    Store.playerExperience = Store.playerExperience + exp;
                }
                if (Store.playerExperience > Store.neededExperienceForLevelUpTo1)
                {
                    Store.playerExperience = Store.playerExperience + 0;
                }
            }
            if (Store.playerLevel == 1)
            {
                if (Store.playerExperience < Store.neededExperienceForLevelUpTo2)
                {
                    Store.playerExperience = Store.playerExperience + exp;
                }
                if (Store.playerExperience > Store.neededExperienceForLevelUpTo2)
                {
                    Store.playerExperience = Store.playerExperience + 0;
                }
            }
        }
        if (Store.playerLevel >= 2)
        {
            exp = 0;
        }
    }
}
