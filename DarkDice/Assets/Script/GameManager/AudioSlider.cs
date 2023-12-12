using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.EventSystems;


public class SliderUI : MonoBehaviour, IPointerClickHandler
{ 
    private AudioMixer mixer;
    public Slider bgmSlider;
    public Slider vfxSlider;

    void Awake()
    {
        mixer = Resources.Load<AudioMixer>("Sound/GameSound");
        bgmSlider.value = PlayerPrefs.GetFloat("BGM", 0.75f);
        vfxSlider.value = PlayerPrefs.GetFloat("SFX", 0.75f);
    }

    void Start()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        eventData.Use();
    }

    public void BGM_SetLevel(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("BGM", -80f);
        }
        else
        {
            mixer.SetFloat("BGM", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("BGM", sliderValue);
        }
    }
    public void SFX_SetLevel(float sliderValue)
    {
        if (sliderValue == 0)
        {
            mixer.SetFloat("SFX", -80f);
        }
        else
        {
            mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
            PlayerPrefs.SetFloat("SFX", sliderValue);
        }
    }
}