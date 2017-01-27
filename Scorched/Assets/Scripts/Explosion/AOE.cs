using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour {

    public int Damage;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemySmall")
        {
            other.gameObject.GetComponent<EnemyManager>().HurtEnemy(Damage);
            //Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "EnemyMedium")
        {
            other.gameObject.GetComponent<EnemyManager>().HurtEnemy(Damage);
            //Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "EnemyBig")
        {
            other.gameObject.GetComponent<EnemyManager>().HurtEnemy(Damage);
            //Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "EnemySmallWave")
        {
            other.gameObject.GetComponent<EnemyManager>().HurtEnemy(Damage);
            //Destroy(other.gameObject);
        }

        Destroy(gameObject, 0.01f);
    }
}
