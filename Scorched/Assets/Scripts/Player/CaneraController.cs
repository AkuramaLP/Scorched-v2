using UnityEngine;
using System.Collections;

public class CaneraController : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;
    public Vector3 initialPosition;

    public Quaternion initialRotation;

    public Vector2 initailPosition;

    public float shakeTimer;
    public float shakeIntensity;

    private GameObject Store;
    public PlayerStore pS;

    //public Vector3 initialPosition;

    //public float amplitude = 0.1f;

    void Start ()
    {
        offset = transform.position - player.transform.position;
        initialPosition = transform.localPosition;
        transform.position = player.transform.position;

        Store = GameObject.Find("Store");
        pS = Store.GetComponent<PlayerStore>();

        shakeIntensity = pS.shakeIntensity;
        shakeTimer = pS.shakeTimer;
    }
	
	void LateUpdate ()
    {
        cameraBehavior();
    }

    void cameraShake()
    {
            pS.shakeTimer -= Time.deltaTime;
            transform.position = Random.insideUnitSphere * pS.shakeIntensity + player.transform.position + offset;
    }

    void cameraBehavior()
    {
        if(pS.shakeTimer > 0)
        {
            cameraShake();
        }
        else
        {
            cameraFollow();
        }
    }

    void cameraFollow()
    {
        transform.position = player.transform.position + offset;
    }
}
