using UnityEngine;
using System.Collections;

public class BombSpawm : MonoBehaviour {

    public Vector3 bombSpawnPosition;
    public int currentBombs;
    public int maxBombs = 10;

    public BombController bomb;

    public Transform spawnPoint;
    public bool bombCheck;
	// Use this for initialization
	void Start ()
    {
        //StartCoroutine(Example());
        //bombCheck = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //StartCoroutine(Example());
    }

    //void bombSpawn()
    //{

    //    bombSpawnPosition.x = Random.Range(-178f, 200f);
    //    bombSpawnPosition.y = 100f;
    //    bombSpawnPosition.z = Random.Range(-100f, 100f);
    //    spawnPoint.position = bombSpawnPosition;


    //    if (bombCheck == false)
    //    {
    //        if(currentBombs < maxBombs)
    //        {
    //            BombController newBomb = Instantiate(bomb, spawnPoint.position, spawnPoint.rotation) as BombController;
    //            currentBombs++;
    //            bombCheck = true;
    //        }
    //    }
    //}

    //IEnumerator Example()
    //{
    //    yield return new WaitForSeconds(2);
        
    //    if (bombCheck == false)
    //    {
    //        BombController newBomb = Instantiate(bomb, spawnPoint.position, spawnPoint.rotation) as BombController;
    //    }
    //    yield return new WaitForSeconds(2);
    //    bombCheck = true;
    //}
}
