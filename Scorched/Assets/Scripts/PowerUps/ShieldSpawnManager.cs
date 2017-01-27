using UnityEngine;
using System.Collections;

public class ShieldSpawnManager : MonoBehaviour {

    public GameObject[] shieldpickup;

    private Vector3 spawnPoint;

    public int Amount;
    public float SpawnTime;

    public int Positionx;
    public int Positonz;

    void Update()
    {
        shieldpickup = GameObject.FindGameObjectsWithTag("ShieldPickUp");
        Amount = shieldpickup.Length;

        if (Amount <= 1)
        {
            for (int count = Amount; count <= 1; count++)
            {
                InvokeRepeating("spawnpickup", SpawnTime, 5);
            }
        }
    }

    void spawnpickup()
    {
        spawnPoint.x = Positionx;
        spawnPoint.y = 1;
        spawnPoint.z = Positonz;

        Instantiate(shieldpickup[UnityEngine.Random.Range(0, shieldpickup.Length - 1)], spawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
