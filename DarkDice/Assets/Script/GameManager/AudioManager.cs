using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer mixer;
    public AudioSource source;
    public float bgmVolume;
    public float sfxVolume;
    private GameObject[] musics;

    private void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("Music");

        if(musics.Length >= 2)
        {
            Destroy(this.gameObject);
        }

        source = GetComponent<AudioSource>();
        bgmVolume = PlayerPrefs.GetFloat("BGM", 0.75f);
        sfxVolume = PlayerPrefs.GetFloat("SFX", 0.75f);
    }

    private void Start()
    {
            mixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
            mixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
    }
}