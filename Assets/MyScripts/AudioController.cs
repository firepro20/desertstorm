using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    public static AudioController Instance { get { return instance; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
        // DontDestroyOnLoad(gameObject);

        //  Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

    public Slider mainSlider;

    public AudioClip shellExplosionClip;
    public AudioClip shotClip;
    public AudioClip shotChargeClip;
    public AudioClip tankExplosion;
    public AudioClip playerHit;
    public AudioClip pickupClip;
    public AudioClip healthClip;
    public AudioClip scoreClip;
    public AudioClip enemyHit;
    

    private AudioSource mainAudioSource;
    private float volumeLevel = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mainAudioSource = GetComponent<AudioSource>();
        volumeLevel = mainSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        volumeLevel = mainSlider.value;
        mainAudioSource.volume = mainSlider.value;
    }

    public void PlayShot()
    {
        mainAudioSource.PlayOneShot(shotClip, volumeLevel);
    }

    public void PlayShellExplosion()
    {
        mainAudioSource.PlayOneShot(shellExplosionClip, volumeLevel);
    }

    public void PlayCharge()
    {
        mainAudioSource.PlayDelayed(0.2f);
    }

    public void PlayTankExplosion()
    {
        mainAudioSource.PlayOneShot(tankExplosion, volumeLevel);
    }

    public void PlayPlayerHit()
    {
        mainAudioSource.PlayOneShot(playerHit, volumeLevel);
    }

    public void PlayPickUpOffensive()
    {
        mainAudioSource.PlayOneShot(pickupClip, volumeLevel);
    }

    public void PlayHealthPickup()
    {
        mainAudioSource.PlayOneShot(healthClip, volumeLevel);
    }

    public void PlayScoreSound()
    {
        mainAudioSource.PlayOneShot(scoreClip, volumeLevel);
    }

    public void PlayEnemyHit()
    {
        mainAudioSource.PlayOneShot(enemyHit, volumeLevel);
    }
}
