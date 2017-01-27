using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActivationZone : MonoBehaviour
{
    public GameObject Pillar;
    private PillarManager PM;
    
    public GameObject AltarManager;
    public AltarManager altar;

    public GameObject store;
    public PlayerStore pS;

    public AudioSource activationSound;

    public GameObject Minmap;
    private Renderer rend;

    public float activationTime;
    public float activationTimeDown;
    public float activationTimeMiddle;
    public float storeNeededExperienceForLevelUp2;

    public bool soundIsActive;

    void Start()
    {
        PM = Pillar.GetComponent<PillarManager>();
        altar = AltarManager.GetComponent<AltarManager>();

        pS = store.GetComponent<PlayerStore>();

        activationSound = GetComponent<AudioSource>();

        altar.firstPillar = GameObject.Find("Obilisk");

        storeNeededExperienceForLevelUp2 = pS.neededExperienceForLevelUpTo2;
        //pS.neededExperienceForLevelUpTo2 = Mathf.Infinity;

        activationTime = altar.activationTime;
        activationTimeDown = activationTime / 3;
        activationTimeMiddle = activationTime / 3 * 2;

        rend = Minmap.GetComponent<Renderer>();
        rend.material.SetColor("_Color", Color.white);

        altar.Pillars = GameObject.FindGameObjectsWithTag("Pillar");
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (PM.isactive != true)
            {
                //Debug.Log("Drinne!!!");
                activationTime -= Time.deltaTime;
                if (soundIsActive == false)
                {
                    activationSound.Play();
                }
                soundIsActive = true;
                if(activationTime <= activationTimeDown)
                {
                    PM.eyeLight[1].SetActive(true);
                }
                if(activationTime <= activationTimeMiddle)
                {
                    PM.eyeLight[0].SetActive(true);
                }
                if (activationTime <= 0)
                {
                    Debug.Log("Aktiviert");
                    activationSound.Stop();
                    actionWhenActive();
                    rend.material.SetColor("_Color", Color.green);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        activationSound.Stop();
        soundIsActive = false;
    }

    public void actionWhenActive()
    {
        altar.activePillars++;

        if(altar.activePillars <= 1)
        {
        }

        if(altar.activePillars >= 2)
        {
        }

        if(altar.activePillars >= altar.unlockAbsorbarLevel1)
        {
            altar.absorbarLocked.enabled = false;
            //pS.playerExperience = 0;
            pS.neededExperienceForLevelUp = pS.neededExperienceForLevelUpTo1;
        }

        if(altar.activePillars == altar.Pillars.Length)
        {
            altar.boss.SetActive(true);
            altar.firstPillar.SetActive(false);
        }
        Debug.Log("Spawn " + PM.EnemyWave.name);
        PM.EnemyWave.SetActive(true);
        PM.Partikel.SetActive(true);
        PM.eyeLight[2].SetActive(true);
        PM.isactive = true;
    }

    public void delayToActivation()
    {

    }
}
