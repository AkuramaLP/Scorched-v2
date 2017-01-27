using UnityEngine;
using System.Collections;

public class GunControllerv2 : MonoBehaviour {

    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;
    public Transform firePoint2;

    public GameObject[] shotParticle;

    public AudioSource shotSound;

    void Start()
    {

    }

    void Update()
    {
        if (isFiring)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShots;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
                BulletController newBullet2 = Instantiate(bullet, firePoint2.position, firePoint2.rotation) as BulletController;
                newBullet2.speed = bulletSpeed;
                var clone = Instantiate(shotParticle[0], firePoint.position, firePoint.rotation);
                Destroy(clone, 1.0f);
                var clone2 = Instantiate(shotParticle[0], firePoint2.position, firePoint2.rotation);
                Destroy(clone2, 1.0f);
                shotSound.Play();
            }
        }
        else
        {
            shotCounter = 0;
        }
    }
}
