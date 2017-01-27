using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public float speed;

    public float lifeTime;

    public int damageToGive;
    public int damageToGiveWhenSmallToSmallEnemy;
    public int damageToGiveWhenSmallToMediumEnemy;
    public int damageToGiveWhenSmallToBigEnemy;
    public int damageToGiveWhenMiddleToSmallEnemy;
    public int damageToGiveWhenMiddleToMediumEnemy;
    public int damageToGiveWhenMiddleToBigEnemy;
    public int damageToGiveWhenBigToSmallEnemy;
    public int damageToGiveWhenBigToMediumEnemy;
    public int damageToGiveWhenBigToBigEnemy;

    public GameObject PlayerStore;
    private PlayerStore pS;

    void Start()
    {
        PlayerStore = GameObject.Find("Store");
        pS = PlayerStore.GetComponent<PlayerStore>();
    }

	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if(lifeTime<= 0)
        {
            Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if(pS.playerLevel == 0)
        {
            if(other.gameObject.tag == "EnemySmall")
            {
                Debug.Log("Blub");
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenSmallToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemySmallWave")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenSmallToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyMedium")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenSmallToMediumEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyBig")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenSmallToBigEnemy);
                Destroy(gameObject);
            }
        }

        if(pS.playerLevel == 1)
        {
            if(other.gameObject.tag == "EnemySmall")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenMiddleToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemySmallWave")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenMiddleToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyMedium")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenMiddleToMediumEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyBig")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenMiddleToBigEnemy);
                Destroy(gameObject);
            }
        }

        if(pS.playerLevel == 2)
        {
            if(other.gameObject.tag == "EnemySmall")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenBigToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemySmallWave")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenBigToSmallEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyMedium")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenBigToMediumEnemy);
                Destroy(gameObject);
            }
            if(other.gameObject.tag == "EnemyBig")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damageToGiveWhenBigToBigEnemy);
                Destroy(gameObject);
            }
        }
        if(other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Pillar")
        {
            Destroy(gameObject);
        }
    }
}
