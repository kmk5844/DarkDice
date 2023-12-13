using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    AudioSource SFX_Source;
    AudioClip wood;
    AudioClip startBattle;
    AudioClip buy;
    AudioClip armor;
    AudioClip addcomplete;
    AudioClip popUp;

    private void Start()
    {
        SFX_Source = GameObject.Find("Sfx").GetComponent<AudioSource>();
        wood = Resources.Load<AudioClip>("Sound/SFX/button_wood");
        startBattle = Resources.Load<AudioClip>("Sound/SFX/button_startbattle");
        buy = Resources.Load<AudioClip>("Sound/SFX/button_buy");
        armor = Resources.Load<AudioClip>("Sound/SFX/button_armor");
        addcomplete = Resources.Load<AudioClip>("Sound/SFX/button_addcomplete");
        popUp = Resources.Load<AudioClip>("Sound/SFX/button_popup");
    }

    public void Button_Wood_SFX()
    {
        SFX_Source.PlayOneShot(wood);
    }
    
    public void Button_StartBattle_SFX()
    {
        SFX_Source.PlayOneShot(startBattle);
    }

    public void Button_Buy_SFX()
    {
        SFX_Source.PlayOneShot(buy);
    }

    public void Button_Armor_SFX()
    {
        SFX_Source.PlayOneShot(armor);
    }

    public void Button_AddComplete_SFX()
    {
        SFX_Source.PlayOneShot(addcomplete);
    }

    public void Button_PopUp_SFX()
    {
        SFX_Source.PlayOneShot(popUp);
    }
}