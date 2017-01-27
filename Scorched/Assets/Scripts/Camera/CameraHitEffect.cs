using UnityEngine;
using System.Collections;

public class CameraHitEffect : MonoBehaviour
{
    public OLDTVScreen OLDTVS;

    protected float maxChromaticMagnetudeState1 = 0.014f;
    protected float maxNoiseMagnetudeState1 = 0.074f;
    protected float maxStaticMagnetudeState1 = 0.025f;
    protected float maxStaticVertical = 1f;
    protected float minStaticVertical = 0f;
    public bool state1;

    protected float maxChromaticMagnetudeState2 = 0.02f;
    protected float maxNoiseMagnetudeState2 = 0.261f;
    protected float maxStaticMagnetudeState2 = 0.116f;
    protected bool state2;

    public float currentChromaticMagnetude;
    public float currentNoiseMagnetude;
    public float currentStaticMagnetude;
    public float currentStaticVertical;

    public float changeCountdownState1;
    public float changeCountdownBackToNormalState1;
    public float changeCountdownState2;
    public float changeCountdownBackToNormalState2;
    public float storeChangeCountdownState1;
    public float storeChangeCountdownState2;

	// Use this for initialization
	void Start ()
    {
        OLDTVS = GetComponent<OLDTVScreen>();

        OLDTVS.chromaticAberrationMagnetude = 0f;
        currentChromaticMagnetude = OLDTVS.chromaticAberrationMagnetude;
        OLDTVS.noiseMagnetude = 0f;
        currentNoiseMagnetude = OLDTVS.noiseMagnetude;
        OLDTVS.staticMagnetude = 0f;
        currentStaticMagnetude = OLDTVS.staticMagnetude;
        OLDTVS.staticVertical = 1f;
        currentStaticVertical = OLDTVS.staticVertical;

        storeChangeCountdownState1 = changeCountdownState1;
        changeCountdownBackToNormalState1 = storeChangeCountdownState1;
        changeCountdownState1 = 0f;
        storeChangeCountdownState2 = changeCountdownState2;
        changeCountdownState2 = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(state1 == false)
        {
            changeEffectToState1();
        }
        if(state1 == true)
        {
            changeEffectBackToNormalState1();
        }
	}

    void changeEffectToState1()
    {
        if(changeCountdownState1 > 0)
        {
            changeCountdownState1 -= Time.deltaTime;
            if (currentChromaticMagnetude < maxChromaticMagnetudeState1)
            {
                currentChromaticMagnetude = currentChromaticMagnetude + 0.00125f;                                       //default: 0.00025f;
                OLDTVS.chromaticAberrationMagnetude = currentChromaticMagnetude;
            }
            if (currentNoiseMagnetude < maxNoiseMagnetudeState1)
            {
                currentNoiseMagnetude = currentNoiseMagnetude + 0.00125F;                                                     //defalut: 0.001f;
                OLDTVS.noiseMagnetude = currentNoiseMagnetude ;
            }
            if (currentStaticMagnetude < maxStaticMagnetudeState1)
            {
                currentStaticMagnetude = currentStaticMagnetude + 0.00125F;                                                   //defalut: 0.001f;
                OLDTVS.staticMagnetude = currentStaticMagnetude;
            }
            if (currentStaticVertical + 0.005f > maxStaticVertical)
            {
                currentStaticVertical = 0f;
                OLDTVS.staticVertical = currentStaticVertical;
            }
            if (currentStaticVertical + 0.005f < maxStaticVertical)
            {
                currentStaticVertical = currentStaticVertical + 0.005f;
                OLDTVS.staticVertical = currentStaticVertical;
            }
        }
        if(changeCountdownState1 < 0)
        {
            state1 = true;
        }
    }

    void changeEffectState2()
    {
        if(changeCountdownState2 > 0)
        {
            changeCountdownState2 -= Time.deltaTime;
            if (currentChromaticMagnetude < maxChromaticMagnetudeState2)
            {
                currentChromaticMagnetude = currentChromaticMagnetude + 0.00025f;
                OLDTVS.chromaticAberrationMagnetude = currentChromaticMagnetude;
            }
            if (currentNoiseMagnetude < maxNoiseMagnetudeState2)
            {
                currentNoiseMagnetude = currentNoiseMagnetude + 0.001F;
                OLDTVS.noiseMagnetude = currentNoiseMagnetude;
            }
            if (currentStaticMagnetude < maxStaticMagnetudeState2)
            {
                currentStaticMagnetude = currentStaticMagnetude + 0.001F;
                OLDTVS.staticMagnetude = currentStaticMagnetude;
            }
            if (currentStaticVertical + 0.005f > maxStaticVertical)
            {
                currentStaticVertical = 0f;
                OLDTVS.staticVertical = currentStaticVertical;
            }
            if (currentStaticVertical + 0.005f < maxStaticVertical)
            {
                currentStaticVertical = currentStaticVertical + 0.005f;
                OLDTVS.staticVertical = currentStaticVertical;
            }
        }
    }

    void changeEffectBackToNormalState1()
    {
        if(state1 == true)
        {
            if(changeCountdownBackToNormalState1 > 0)
            {
                changeCountdownBackToNormalState1 -= Time.deltaTime;
                if(currentChromaticMagnetude > 0)
                {
                    if(currentChromaticMagnetude - 0.00125f > 0)
                    {
                        currentChromaticMagnetude = currentChromaticMagnetude - 0.00125f;
                        OLDTVS.chromaticAberrationMagnetude = currentChromaticMagnetude;
                    }
                    if(currentChromaticMagnetude - 0.00125f <= 0)
                    {
                        currentChromaticMagnetude = 0;
                        OLDTVS.chromaticAberrationMagnetude = currentChromaticMagnetude;
                    }
                }

                if(currentNoiseMagnetude > 0)
                {
                    if(currentNoiseMagnetude - 0.00125F > 0)
                    {
                        currentNoiseMagnetude = currentNoiseMagnetude - 0.00125F;
                        OLDTVS.noiseMagnetude = currentNoiseMagnetude;
                    }
                    if(currentNoiseMagnetude - 0.00125F <= 0)
                    {
                        currentNoiseMagnetude = 0;
                        OLDTVS.noiseMagnetude = currentNoiseMagnetude;
                    }
                }

                if(currentStaticMagnetude > 0)
                {
                    if(currentStaticMagnetude - 0.00125F > 0)
                    {
                        currentStaticMagnetude = currentStaticMagnetude - 0.00125F;
                        OLDTVS.staticMagnetude = currentStaticMagnetude;
                    }
                    if(currentStaticMagnetude - 0.00125F <= 0)
                    {
                        currentStaticMagnetude = 0;
                        OLDTVS.staticMagnetude = currentStaticMagnetude;
                    }
                }
            }
            if(changeCountdownBackToNormalState1 < 0)
            {
                state1 = false;
            }
        }
    }
}
