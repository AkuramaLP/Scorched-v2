using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public int enemyLevel;

    public int enemyExperience;
    public int store;

    public float timeTillDeath = 2f;

    public GameObject PlayerStore;
    private PlayerStore Store;

    public GameObject AltManager;
    private AltarManager alt;

    private HurtPlayer hP;

    AudioSource[] Sounds;
    public Animator anim;

    public GameObject EnemySmall;
    public EnemyController_Small_New_AI ECS;

    public GameObject EnemyMedium;
    public EnemyController_Medium ECM;

    public GameObject EnemyBig;
    public EnemyController_Big ECB;

    private Vector3 SoulSpawnPosition;

    private Rigidbody rb;
    private BoxCollider mc;

    private MeshCollider mcB;

    //Particleeffect
    public GameObject hitParticle;

    private Vector3 Particleposition;

    public float Particle_y_postition;

    //damagetext
    public GameObject DamagePopupText;

    public bool Soundisactive;

    void Start()
    {
        currentHealth = health;

        PlayerStore = GameObject.Find("Store");
        AltManager = GameObject.Find("Altarmanager");

        Store = PlayerStore.GetComponent<PlayerStore>();
        alt = AltManager.GetComponent<AltarManager>();
        Sounds = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        hP = FindObjectOfType<HurtPlayer>();
        mc = GetComponent<BoxCollider>();

        if(gameObject.tag == "EnemySmall" || gameObject.tag == "EnemySmallWave")
        {
            ECS = EnemySmall.GetComponent<EnemyController_Small_New_AI>();
            ECS.enabled = true;
            mc.isTrigger = false;
            enemyLevel = 0;
        }

        if(gameObject.tag == "EnemyMedium")
        {
            ECM = EnemyMedium.GetComponent<EnemyController_Medium>();
            ECM.enabled = true;
            mc.isTrigger = false;
            enemyLevel = 1;
        }

        if(gameObject.tag == "EnemyBig")
        {
            ECB = EnemyBig.GetComponent<EnemyController_Big>();
            ECB.enabled = true;
            mcB = GetComponent<MeshCollider>();
            enemyLevel = 2;
        }
    }

    void Update()
    {
        if(currentHealth <= 0)
        {
            if (Soundisactive == false)
            {
                Sounds[1].Play();
            }
            Soundisactive = true;

            if (gameObject.tag == "EnemyBig")
            {
                anim.Play("Death");
            }
            else
            {
                anim.Play("Tod");
            }
            Store.timeBetweenKills = Store.storeTimeBetweenKills;

            if (gameObject.tag == "EnemySmall" || gameObject.tag == "EnemySmallWave")
            {
                hP.damageToGive = 0;
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                mc.isTrigger = true;
                ECS.enabled = false;
            }

            timeTillDeath -= Time.deltaTime;

            if(timeTillDeath <= 0)
            {
                //giveExperience();
                calculateLoot();
                if (gameObject.tag == "EnemyBig")
                {
                    alt.activePillars++;
                }
                Destroy(gameObject);
            }

            if(gameObject.tag == "EnemyMedium")
            {
                hP.damageToGive = 0;
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                mc.isTrigger = true;
                ECM.enabled = false;

                timeTillDeath -= Time.deltaTime;
                if (timeTillDeath <= 0)
                {
                    //giveExperience();
                    calculateLoot();
                    if (gameObject.tag == "EnemyBig")
                    {
                        alt.activePillars++;
                    }
                    Destroy(gameObject);
                }
            }

            if (gameObject.tag == "EnemyBig")
            {
                hP.damageToGive = 0;
                rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
                mcB.isTrigger = true;
                ECB.enabled = false;

                timeTillDeath -= Time.deltaTime;
                if (timeTillDeath <= 0)
                {
                    //giveExperience();
                    calculateLoot();
                    if (gameObject.tag == "EnemyBig")
                    {
                        alt.activePillars++;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    public void HurtEnemy(int damage)
    {
        Particleposition.x = transform.position.x;
        Particleposition.y = Particle_y_postition;
        Particleposition.z = transform.position.z;

        Debug.Log(damage);
        currentHealth -= damage;
        anim.Play("Verletzt");
        var clone = Instantiate(hitParticle, Particleposition, transform.rotation);
        Destroy(clone, 1.0f);
        Sounds[0].Play();
        Instantiate(DamagePopupText, transform.position, Quaternion.Euler(60, 0, 0));
        DamagePopupText.GetComponent<TextMesh>().text = damage.ToString();
    }

    /*public void giveExperience()
    {     
        if(Store.playerLevel < 2)
        {
            if(Store.playerLevel == 0)
            {
                if(Store.playerExperience < Store.neededExperienceForLevelUpTo1)
                {
                    Store.playerExperience = Store.playerExperience + enemyExperience;
                }
                if(Store.playerExperience > Store.neededExperienceForLevelUpTo1)
                {
                    Store.playerExperience = Store.playerExperience + 0;
                }
            }
            if(Store.playerLevel == 1)
            {
                if (Store.playerExperience < Store.neededExperienceForLevelUpTo2)
                {
                    Store.playerExperience = Store.playerExperience + enemyExperience;
                }
                if(Store.playerExperience > Store.neededExperienceForLevelUpTo2)
                {
                    Store.playerExperience = Store.playerExperience + 0;
                }
            }
        }
        if(Store.playerLevel >= 2)
        {
            enemyExperience = 0;
        }
    }*/

    [System.Serializable]
    public class DropCurrency
    {
        public string name;
        public GameObject Dropitem;
        public int dropRarity;
    }

    public List<DropCurrency> LootTable = new List<DropCurrency>();
    public int dropChance;

    public void calculateLoot()
    {
        SoulSpawnPosition.x = transform.position.x;
        SoulSpawnPosition.y = 1.0f;
        SoulSpawnPosition.z = transform.position.z;

        int calc_dropChance = Random.Range(0, 101);

        if (calc_dropChance > dropChance)
        {
            return;
        }

        if (calc_dropChance <= dropChance)
        {
            int itemWeight = 0; //itemweight = Droprarity

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[i].dropRarity;
            }

            int randomValue = Random.Range(0, itemWeight);

            for (int j = 0; j < LootTable.Count; j++)
            {
                if (randomValue <= LootTable[j].dropRarity)
                {
                    Instantiate(LootTable[j].Dropitem, SoulSpawnPosition, Quaternion.identity);
                    return;                                                                         //return weg lassen, wenn alle items gedropt werden sollen
                }
                randomValue -= LootTable[j].dropRarity;
            }
        }
    }
}
