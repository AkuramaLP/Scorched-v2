using UnityEngine;
using System.Collections;

public class SlousPickUp : MonoBehaviour
{
    public float distance;
    public Transform target;
    public GameObject Player;
    public float lookAtDistance = 80.0f;
    public float attackRange = 70.0f;
    public float moveSpeed = 30f;
    public float smoothness = 7.0f;

    public GameObject Store;
    public PlayerStore pS;

	// Use this for initialization
	void Start ()
    {
        Store = GameObject.Find("Store");
        pS = Store.GetComponent<PlayerStore>();

        if(moveSpeed == 0f)
        {
            moveSpeed = 70.0f;
        }
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
    }
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector3.Distance(target.position, transform.position);
        if(distance < attackRange)
        {
            lookAtPlayer();
            goToPlayer();
        }
	}

    void lookAtPlayer()
    {
        var rotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothness);
    }

    void goToPlayer()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
