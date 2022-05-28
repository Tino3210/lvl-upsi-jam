using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip start;
    public AudioClip petris;
    public AudioClip breakPieces;
    [Range(0, 1)]
    public float volumeMain;
    [Range(0, 1)]
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

    public void PlayBreakPieces()
    {
        AudioSource.PlayClipAtPoint(breakPieces, Camera.main.transform.position, volumeMain);
    }
}
