﻿using UnityEngine;
using System.Collections;

public class EnemyController_Medium : MonoBehaviour {

    public float distance;
    public Transform target;
    public GameObject Player;
    public float lookAtDistance = 20.0f;
    public float attackRange = 10.0f;
    public float moveSpeed = 5.0f;
    public float smoothness = 7.0f;

    public GameObject Store;
    public PlayerStore pS;

	// Use this for initialization
	void Start ()
    {
        Store = GameObject.Find("Store");
        pS = Store.GetComponent<PlayerStore>();

        if(pS.playerLevel < 2)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if(pS.playerLevel == 2)
        {
            Player = GameObject.FindGameObjectWithTag("PlayerBig");
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
            if(pS.playerLevel < 2)
            {
                Player = GameObject.FindGameObjectWithTag("Player");
            }
            if(pS.playerLevel == 2)
            {
                Player = GameObject.FindGameObjectWithTag("PlayerBig");
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
        var rotation = Quaternion.LookRotation(target.position - transform.position);           //Google mal Später
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * smoothness);
    }

    void attack()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
