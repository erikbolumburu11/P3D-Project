using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource SFXSource;

    public AudioClip currentMusic;

    public static SoundManager instance;

    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClip clip){
        if(SFXSource == null) return;
        SFXSource.PlayOneShot(clip);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        SceneAudioData sceneAudioData = FindObjectOfType<SceneAudioData>();

        if(sceneAudioData == null) return;
        if(sceneAudioData.music == null) return;
        if(sceneAudioData.music == currentMusic) return;

        currentMusic = sceneAudioData.music;
        musicSource.clip = currentMusic;
        musicSource.volume = sceneAudioData.musicVolume;
        musicSource.Play();

        if(Camera.main == null) return;
        SFXSource = Camera.main.gameObject.GetComponent<AudioSource>();
    }

}
