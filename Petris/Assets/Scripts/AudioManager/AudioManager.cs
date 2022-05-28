using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip start;
    public AudioClip petris;
    public float volumeMain;
    public float volumeInterface;

    public static AudioManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        start = Resources.Load<AudioClip>("Audio/voice_slow_start");
        petris = Resources.Load<AudioClip>("Audio/voice_slow_petris");
        volumeMain = 1f;
        volumeInterface = 1f;
    }

    void Update()
    {
        
    }


    public void PlayStart()
    {
        AudioSource.PlayClipAtPoint(start, Camera.main.transform.position, volumeInterface * volumeMain);
    }

    public void PlayPetris()
    {
        AudioSource.PlayClipAtPoint(petris, Camera.main.transform.position, volumeInterface * volumeMain);
    }
}
