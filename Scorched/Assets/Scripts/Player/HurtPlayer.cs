using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    public int damageToGiveSmallToSmall;
    public int damageToGiveSmallToMedium;
    public int damageToGiveSmallToBig;
    public int damageToGiveMediumToSmall;
    public int damageToGiveMediumToMedium;
    public int damageToGiveMediumToBig;
    public int damageToGiveBigToSmall;
    public int damageToGiveBigToMedium;
    public int damageToGiveBigToBig;

    public bool isInAttack;
    public float animPlay = 2f;

    public GameObject Enemy;
    public EnemyManager eM;

    public GameObject PlayerStore;
    private PlayerStore pS;

    //For HitFeedback
    public Camera[] cams = new Camera[3];

    private CameraHitEffect effectLV1;
    private CameraHitEffect effectLV2;
    private CameraHitEffect effectLV3;

    private bool PlayerWasHit;

    void Start()
    {
        eM = Enemy. GetComponent<EnemyManager>();
        PlayerStore = GameObject.Find("Store");
        pS = PlayerStore.GetComponent<PlayerStore>();

        cams[0] = GameObject.Find("CameraLevel0").GetComponent<Camera>();
        cams[1] = GameObject.Find("CameraLevel1").GetComponent<Camera>();
        cams[2] = GameObject.Find("CameraLevel2").GetComponent<Camera>();

        //get HitFeedBack script
        effectLV1 = cams[0].GetComponent<CameraHitEffect>();
        effectLV2 = cams[1].GetComponent<CameraHitEffect>();
        effectLV3 = cams[2].GetComponent<CameraHitEffect>();
    }

    public void Update()
    {

    }

	public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" | other.gameObject.tag == "PlayerBig")
        {
            if(eM.enemyLevel == 0)
            {
                if(pS.playerLevel == 0)
                {
                    if(eM.currentHealth > 0)
                    {
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveSmallToSmall);
                        eM.anim.Play("Angriff");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if(effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                    }
                }
                if(pS.playerLevel == 1)
                {
                    if(eM.currentHealth > 0)
                    {
                        Debug.Log(damageToGiveSmallToMedium);
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveSmallToMedium);
                        eM.anim.Play("Angriff");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                    }
                }
                if (pS.playerLevel == 2)
                {
                    if (eM.currentHealth > 0)
                    {
                        eM.currentHealth = 0;
                    }
                }
            }

            if(eM.enemyLevel == 1)
            {
                if(pS.playerLevel == 0)
                {
                    if(eM.currentHealth > 0)
                    {
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveMediumToSmall);
                        eM.anim.Play("Angriff");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                    }
                }
                if(pS.playerLevel == 1)
                {
                    if(eM.currentHealth > 0)
                    {
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveMediumToMedium);
                        eM.anim.Play("Angriff");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                    }
                }
                if(pS.playerLevel == 2)
                {
                    if(eM.currentHealth > 0)
                    {
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveMediumToBig);
                        eM.anim.Play("Angriff");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                    }
                }
            }

            if(eM.enemyLevel == 2)
            {
                if(pS.playerLevel == 0)
                {
                    if(eM.currentHealth > 0)
                    {
                        isInAttack = true;
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveBigToSmall);
                        eM.anim.Play("Attack");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                        isInAttack = false;
                    }
                }
                if(pS.playerLevel == 1)
                {
                    if(eM.currentHealth > 0)
                    {
                        isInAttack = true;
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveBigToMedium);
                        eM.anim.Play("Attack");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                        isInAttack = false;
                    }
                }
                if(pS.playerLevel == 2)
                {
                    if(eM.currentHealth > 0)
                    {
                        isInAttack = true;
                        other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGiveBigToBig);
                        eM.anim.Play("Attack");
                        PlayerWasHit = true;
                        effectLV1.changeCountdownState1 = effectLV1.storeChangeCountdownState1;
                        if (effectLV1.changeCountdownBackToNormalState1 <= 0)
                        {
                            effectLV1.changeCountdownBackToNormalState1 = effectLV1.storeChangeCountdownState1;
                        }
                        if (isInAttack == true)
                        {
                            animPlay -= Time.deltaTime;
                        }
                        if(animPlay <= 0)
                        {
                            isInAttack = false;
                        }
                    }
                }
            }
        }
    }
}
