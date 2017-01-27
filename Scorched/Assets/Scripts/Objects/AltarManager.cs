using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AltarManager : MonoBehaviour
{
    public int activePillars;

    public GameObject[] Pillars;
    public GameObject boss;

    public float activationTime;

    public GameObject firstPillar;
    public Image absorbarLocked;

    public GameObject store;
    public PlayerStore pS;

    [Header("LevelUPS")]
    public int unlockAbsorbarLevel1;
    public int unlockAbsorbarLevel2;

    // Use this for initialization
    void Start ()
    {
        pS = store.GetComponent<PlayerStore>();

        pS.neededExperienceForLevelUp = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        if (pS.playerLevel == 1 && activePillars < unlockAbsorbarLevel2)
        {
            pS.neededExperienceForLevelUp = Mathf.Infinity;
            //Debug.Log("bla");
            absorbarLocked.enabled = true;
        }
        if(pS.playerLevel == 0 && activePillars < unlockAbsorbarLevel2 && activePillars >= unlockAbsorbarLevel1)
        {
            pS.neededExperienceForLevelUp = pS.neededExperienceForLevelUpTo1;
            absorbarLocked.enabled = false;
            //Debug.Log("blub");
        }
        if(pS.playerLevel == 1 && activePillars == unlockAbsorbarLevel2)
        {
            pS.neededExperienceForLevelUp = pS.neededExperienceForLevelUpTo2;
            absorbarLocked.enabled = false;

            //Debug.Log("da");
        }
    }
}
