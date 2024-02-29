using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : MonoBehaviour
{
    public static sfxManager Instance { get; private set; }

    [SerializeField] AudioClip lineClearSFX, phoneRingSFX, syringeInjectSFX;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void playSFX(string clip)
    {
        GameObject newAudioObject = new GameObject();
        newAudioObject.transform.parent = transform;
        AudioSource newAudioSource = newAudioObject.AddComponent<AudioSource>();

        switch(clip)
        {
            case "lineClear":
                newAudioSource.clip = lineClearSFX;
                newAudioSource.volume = 0.5f;
                break;

            case "phoneRing":
                newAudioSource.clip = phoneRingSFX;
                newAudioSource.volume = 0.4f;
                break;

            case "syringeInject":
                newAudioSource.clip = syringeInjectSFX;
                newAudioSource.volume = 0.15f;
                break;
        }


        newAudioSource.Play();
        
    }

}
