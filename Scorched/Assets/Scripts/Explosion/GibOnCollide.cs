using UnityEngine;
using System.Collections;

public class GibOnCollide : MonoBehaviour {

    public GameObject Explosion;
    public GameObject Explosionparticle;

    void OnCollisionEnter()
    {
        var explosionclone = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(explosionclone, 0.05f);
        var particleclone = Instantiate(Explosionparticle, transform.position, transform.rotation);
        Destroy(particleclone, 0.5f);


        Destroy(gameObject, 0.01f);
    }
}
