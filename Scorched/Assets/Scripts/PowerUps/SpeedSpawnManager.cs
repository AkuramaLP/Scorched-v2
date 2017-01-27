using UnityEngine;
using System.Collections;

public class SpeedSpawnManager : MonoBehaviour {

    public GameObject[] speedpickup; 

    private Vector3 spawnPoint;

    public int Amount;
    public float SpawnTime;

    public int Positionx;
    public int Positonz;

	void Update ()
    {
        speedpickup = GameObject.FindGameObjectsWithTag("SpeedPickUp");
        Amount = speedpickup.Length;

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

        Instantiate(speedpickup[UnityEngine.Random.Range(0, speedpickup.Length - 1)], spawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
