using UnityEngine;
using System.Collections;

public class SwitchMusicOnLoad : MonoBehaviour {

    public AudioClip newTrack;

    private AudioManager theAM;

    void Start()
    {
        theAM = FindObjectOfType<AudioManager>();

        if(newTrack != null)
        theAM.ChangeBGM(newTrack);
    }


    void Update () {
	
	}
}
