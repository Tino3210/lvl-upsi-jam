using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip start;
    public AudioClip petris;
    public AudioClip option;
    public AudioClip exit;

    public AudioClip landPieces;
    public AudioClip breakPieces;

    public AudioClip petrominos;

    public AudioClip music1;
    public AudioClip music2;

    [Range(0, 1)]
    public float volumeMain;
    [Range(0, 1)]
    public float volumeInterface;
    [Range(0, 1)]
    public float volumeMusic;
    [Range(0, 1)]
    public float volumeEffect;

    public bool isInterface;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(StartSongCoroutine());
    }

    private IEnumerator StartSongCoroutine()
    {
        yield return new WaitForSeconds(2f);
        ChangeMusic2To1();
    }

    void Update()
    {
        
    }

    // Change AudioSource music1 by music2
    public void ChangeMusic1To2()
    {
        audioSource.clip = music2;
        audioSource.loop = true;
        ChangeMusicVolume();
        audioSource.Play();
    }

    public void ChangeMusic2To1()
    {
        audioSource.clip = music1;
        audioSource.loop = true;
        ChangeMusicVolume();
        audioSource.Play();
    }

    public void ChangeMusicVolume()
    {
        audioSource.volume = volumeMusic * volumeMain;
    }

    public void ChangeMusicVolume(float mul)
    {
        audioSource.volume = volumeMusic * volumeMain * mul;
    }

    public void PlayStart()
    {
        audioSource.volume = volumeMusic * volumeMain * 0.5f;
        AudioSource.PlayClipAtPoint(start, Camera.main.transform.position, volumeInterface * volumeMain);
        StartCoroutine(ResetSongCoroutine());
    }

    public void PlayOption()
    {
        audioSource.volume = volumeMusic * volumeMain * 0.5f;
        AudioSource.PlayClipAtPoint(option, Camera.main.transform.position, volumeInterface * volumeMain);
        StartCoroutine(ResetSongCoroutine());
    }

    public void PlayExit()
    {
        audioSource.volume = volumeMusic * volumeMain * 0.1f;
        AudioSource.PlayClipAtPoint(exit, Camera.main.transform.position, volumeInterface * volumeMain);
    }

    public void PlayPetris()
    {
        AudioSource.PlayClipAtPoint(petris, Camera.main.transform.position, volumeInterface * volumeMain);
    }

    public void PlayLandPieces()
    {
        AudioSource.PlayClipAtPoint(landPieces, Camera.main.transform.position, volumeEffect * volumeMain);
    }

    public void PlayBreakPieces()
    {
        AudioSource.PlayClipAtPoint(breakPieces, Camera.main.transform.position, volumeEffect * volumeMain);
    }

    public void PlayPetrominos()
    {
        AudioSource.PlayClipAtPoint(petrominos, Camera.main.transform.position, volumeEffect * volumeMain);
    }

    private IEnumerator ResetSongCoroutine()
    {
        yield return new WaitForSeconds(2f);
        audioSource.volume = volumeMusic * volumeMain;
    }
}
