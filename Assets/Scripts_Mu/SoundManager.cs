using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    private AudioSource audioSource;

    [SerializeField] private AudioClip mainScene;
    [SerializeField] private AudioClip drugScene;
    [SerializeField] private AudioClip effectStart;
    [SerializeField] private AudioClip[] effectSounds;

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(SoundManager).ToString());
                    _instance = singleton.AddComponent<SoundManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "effect":
                int randomIndex = Random.Range(0, effectSounds.Length);
                audioSource.PlayOneShot(effectSounds[randomIndex]);
                break;
            case "effectStart":
                audioSource.PlayOneShot(effectStart);
                break;
            case "mainScene":
                audioSource.clip = mainScene;
                audioSource.loop = true;
                audioSource.Play();
                break;
            case "drugScene":
                audioSource.clip = drugScene;
                audioSource.loop = true;
                audioSource.Play();
                break;

        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopSound();
        if(scene.name == "JA")
            SoundManager.Instance.PlaySound("mainScene");
    }
}
