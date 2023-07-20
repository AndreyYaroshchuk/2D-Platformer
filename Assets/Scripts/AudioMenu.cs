using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public Toggle toggle;

    public void MuteAudio()
    {
        if (toggle == true)
        {
            audioSource.Pause();

        }
        else
        {
            audioSource.Play();
        }

    }
}
