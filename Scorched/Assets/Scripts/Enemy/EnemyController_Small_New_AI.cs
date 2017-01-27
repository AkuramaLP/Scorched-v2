using UnityEngine;
using System.Collections;

public class EnemyController_Small_New_AI : MonoBehaviour {

    public float distance;
    public Transform target;
    public GameObject Player;
    public float lookAtDistance = 20.0f;
    public float attackRange = 10.0f;
    public float moveSpeed = 30f;
    public float smoothness = 7.0f;

    public GameObject Store;
    public PlayerStore pS;
	
    void Start()
    {
        Store = GameObject.Find("Store");
        pS = Store.GetComponent<PlayerStore>();

        if(moveSpeed == 0f)
        {
            moveSpeed = 30f;
        }

        if(pS.playerLevel == 0)
        {
            Player = GameObject.Find("PlayerLV1");
        }
        if(pS.playerLevel == 1)
        {
            Player = GameObject.Find("PlayerLV2");
        }
        if(pS.playerLevel == 2)
        {
            Player = GameObject.Find("PlayerLV3");
        }

        target = Player.GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if(distance > lookAtDistance)
        {

        }
        if(distance < lookAtDistance)
        {
            lookAt();
        }
        if(distance < attackRange)
        {
            attack();
        }

        if(pS.levelChange == true)
        {
            if (pS.playerLevel == 0)
            {
                Player = GameObject.Find("PlayerLV1");
            }
            if (pS.playerLevel == 1)
            {
                Player = GameObject.Find("PlayerLV2");
            }
            if (pS.playerLevel == 2)
            {
                Player = GameObject.Find("PlayerLV3");
            }
            target = Player.GetComponent<Transform>();
            pS.check -= Time.deltaTime;
            if(pS.check <= 0)
            {
                pS.levelChange = false;
            }
        }
	}

    void lookAt()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position);               //Google mal Später
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothness);
    }

    void attack()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
