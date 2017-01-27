using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpawnController : MonoBehaviour {

    public GameObject[] smallenemies;
    public GameObject[] mediumenemies;
    public GameObject[] bigenemies;

    public int amountSmall;
    public int amountMedium;
    public int amountBig;

    public int maxAmountSmall;
    public int maxAmountMedium;

    private Vector3 spawnPoint;

    public int minrangex;
    public int maxrangex;

    public float height;

    public int minrangez;
    public int maxrangez;

    public int winScreen;

    //Wincondition

    public GameObject AltManger;
    private AltarManager alt;

    void Start()
    {
        alt = AltManger.GetComponent<AltarManager>();
    }

    void Update()
    {
        //Smallenemies
        smallenemies = GameObject.FindGameObjectsWithTag("EnemySmall");
        amountSmall = smallenemies.Length;

        if (amountSmall <= maxAmountSmall)
        {
            for(int count = amountSmall; count < maxAmountSmall; count++)
            {
                InvokeRepeating("spawnEnemy", 2, 4);
            }
        }

        //Mediumenemies
        mediumenemies = GameObject.FindGameObjectsWithTag("EnemyMedium");
        amountMedium = mediumenemies.Length;

        if(amountMedium <= maxAmountMedium)
        {
            for(int count = amountMedium; count < maxAmountMedium; count++)
            {
                InvokeRepeating("spawnEnemy", 5, 10);
            }
        }

        //Bigenemies
        bigenemies = GameObject.FindGameObjectsWithTag("EnemyBig");
        amountBig = bigenemies.Length;

        if(amountBig <= 0 && alt.activePillars >= 6)
        {
            SceneManager.LoadScene(winScreen);
        }

    }

    void spawnEnemy()
    {
        spawnPoint.x = Random.Range(minrangex, maxrangex);   
        spawnPoint.y = height;                               // 1
        spawnPoint.z = Random.Range(minrangez, maxrangez);   

        Instantiate(smallenemies[UnityEngine.Random.Range(0, smallenemies.Length - 1)], spawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
