using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip buttonClickAC;
    public AudioClip gatePassAC;
    public AudioClip gateFailAC;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundButtonClick()
    {
        audioSource.PlayOneShot(buttonClickAC);
    }

    public void PlaySoundGatePass()
    {
        audioSource.PlayOneShot(gatePassAC);
    }

    public void PlaySoundGateFail()
    {
        audioSource.PlayOneShot(gateFailAC);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
