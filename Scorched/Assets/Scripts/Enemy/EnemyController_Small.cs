using UnityEngine;
using System.Collections;

public class EnemyController_Small : MonoBehaviour {

    private Rigidbody rb;
    public float moveSpeed;

    public PlayerController thePlayer;

    public GameObject Store;
    public PlayerStore pS;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerController>();

        Store = GameObject.Find("Store");
        pS = Store.GetComponent<PlayerStore>();
	}

    void FixedUpdate()
    {
        rb.velocity = (transform.forward * moveSpeed);
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(thePlayer.transform.position);
        if(pS.levelChange == true)
        {
            thePlayer = FindObjectOfType<PlayerController>();
            pS.check -= Time.deltaTime;
            if(pS.check <= 0)
            {
                pS.levelChange = false;
            }
        }
	}
}
