using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour {

    public float TextSpeed;
    public float LifeTime;

    /*void Start()
    {
        GetComponent<TextMesh>().text = ;
    }*/

    void Update () {
        LifeTime = LifeTime - Time.deltaTime;
        transform.Translate(0, TextSpeed * Time.deltaTime, 0);

        if(LifeTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
