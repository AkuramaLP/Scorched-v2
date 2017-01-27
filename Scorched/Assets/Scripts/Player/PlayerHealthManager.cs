using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour {

    //Varibals
    public float startingHealth;
    public float currentHealth;
    public float maxHealth;                 //set to Private
    public float startingHealthLvl1;        //set to Private
    public float startingHealthLvl2;        //set to Private

    public int Scene;

    //Healthbar
    public Slider healthBar;
    public int Pickuphealth;

    public GameObject playerMesh;
    private PlayerHitFeedback pHF;

    //Death
    public float deathCountdown = 1f;

    //Sound
    AudioSource[] Sounds;

    //Hiteffects
    public GameObject hitParticle;
    private Vector3 Particleposition;
    public float Particle_y_postition;
    Animator anim;
    //public GameObject blood;
    //public float showBloodScreenCountdown;
    //private float showBloodScreenCountdownStore;
    private bool hitIsActive;

    //PlayerStore
    private GameObject Store;
    private PlayerStore pS;

    void Start ()
    {
        //Set Values
        currentHealth = startingHealth;
        startingHealthLvl1 = startingHealth * 2;
        startingHealthLvl2 = startingHealthLvl1 * 2;
        maxHealth = startingHealth;
        //showBloodScreenCountdownStore = showBloodScreenCountdown;

        //Find Objects
        Store = GameObject.Find("Store");

        //Get Components
        pHF = playerMesh.GetComponent<PlayerHitFeedback>();
        Sounds = GetComponents<AudioSource>();
        anim = GetComponent<Animator>();
        pS = Store.GetComponent<PlayerStore>();

        //blood = pS.blood;
        //blood.SetActive(false);
	}
		
	void Update ()
    {
        if(currentHealth <= 0)
        {
            deathTrigger();
        }

        pHF.changeColor();
        healthBar.value = currentHealth;

        //if(hitIsActive == true)
        //{
        //    if(showBloodScreenCountdown > 0)
        //    {
        //        blood.SetActive(true);
        //        showBloodScreenCountdown -= Time.deltaTime;
        //    }
        //    else
        //    {
        //        blood.SetActive(false);
        //        showBloodScreenCountdown = showBloodScreenCountdownStore;
        //        hitIsActive = false;
        //    }
        //}
	}

    public void HurtPlayer(int damageAmount)
    {
        Particleposition.x = transform.position.x;
        Particleposition.y = Particle_y_postition;
        Particleposition.z = transform.position.z;

        anim.Play("Hurt");
        currentHealth -= damageAmount;
        pHF.visualFeedback();
        var clone = Instantiate(hitParticle, Particleposition, transform.rotation);
        Destroy(clone, 1.0f);
        Sounds[0].Play();
        //hitIsActive = true;
    }

    public void deathTrigger()
    {
        anim.Play("Death");
        deathCountdown -= Time.deltaTime;
        if(deathCountdown <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(Scene);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthPickUP"))
        {
            Sounds[1].Play();
            other.gameObject.SetActive(false);
            currentHealth = currentHealth + Pickuphealth;
        }
    }
}
