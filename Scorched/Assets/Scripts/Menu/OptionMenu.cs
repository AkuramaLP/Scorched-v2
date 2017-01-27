using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour {

    public int MainMenuScene;
    public AudioSource hoverSound;

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void PlayAudio()
    {
        hoverSound.Play();
    }
}
