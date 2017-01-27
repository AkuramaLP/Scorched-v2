using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public int GameLevel;
    public int CreditsScene;
    public int OptionScene;
    public AudioSource hoverSound;

    public void PlayGame()
    {
        //Application.LoadLevel(GameLevel);
        SceneManager.LoadScene(GameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(CreditsScene);
    }

    public void Options()
    {
        SceneManager.LoadScene(OptionScene);
    }

    public void PlayAudio()
    {
        hoverSound.Play();
    }
}
