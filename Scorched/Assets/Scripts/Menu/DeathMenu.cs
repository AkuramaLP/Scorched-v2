using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{

    public int RespawnScene;
    public int MainMenuScene;
    public AudioSource hoverSound;

    public void Respawn()
    {
        SceneManager.LoadScene(RespawnScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    public void PlayAudio()
    {
        hoverSound.Play();
    }
}