using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject Healthpoint;
    [SerializeField] private AudioSource audioSource;
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void ButtonPlay()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single); 
    }
    public void ButtonOption()
    {
        
        Healthpoint.SetActive(true);
    }
    public void ButtonExit()
    {
        //Application.Quit(); // Закрыть игру 
        Application.Quit();
    }
    public void ButtonCloseMainMenu()
    {

        Healthpoint.SetActive(false);
    }
    public void ButtonMuteAudio()
    {
        audioSource.mute = !audioSource.mute;

    }
}
