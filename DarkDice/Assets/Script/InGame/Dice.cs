using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField]
    public float delay;

    AudioSource SFX_Audio;
    AudioClip dice_roll_SFX;
    AudioClip dice_end_SFX;
    
    public Sprite PlaySprite;
    public Sprite PauseSprite;
    public Sprite[] AtkPocket;
    public Sprite[] DefPocket;
    public Image Dice1_Image;
    public Image Dice2_Image;
    Transform Dice1;
    Transform Dice2;
    Animator Dice1_Ani;
    Animator Dice2_Ani;

    public int atkSum;
    public int defSum;

    public Button Play_Button; // '전투 개시' 버튼
    public Button Dice_Button; // '굴리기' 및 '멈추기' 버튼
    public Button Attack_Button; //' '공격' 버튼
    public bool rollingFlag;

    private void Start()
    {
        Dice1 = Dice1_Image.GetComponent<Transform>();
        Dice2 = Dice2_Image.GetComponent<Transform>();
        Dice1_Ani = Dice1.GetComponent<Animator>();
        Dice2_Ani = Dice2.GetComponent<Animator>();
        SFX_Audio = GameObject.Find("Sfx").GetComponent<AudioSource>();
        dice_roll_SFX = Resources.Load<AudioClip>("Sound/SFX/dice_roll_loop_new");
        dice_end_SFX = Resources.Load<AudioClip>("Sound/SFX/dice_random_new_new");
        rollingFlag = false;
        delay = 0.01f;
        atkSum = 0;
        defSum = 0;
    }

    IEnumerator coroutine;
    IEnumerator DiceRolling(Image image1, Image image2)
    {
        int rand1 = Random.Range(0, 6);
        int rand2 = Random.Range(0, 6);

        if (rollingFlag == false)
        {
            if(Dice1_Ani.GetBool("Effect") == false && Dice2_Ani.GetBool("Effect") == false)
            {
                Dice1_Ani.SetBool("Effect", true);
                Dice2_Ani.SetBool("Effect", true);
            }
            delay += 0.062f;

            if (delay > 0.56f)
            {
                StopCoroutine(coroutine);
                Attack_Button.interactable = true;
                Dice1_Ani.SetBool("Effect", false);
                Dice2_Ani.SetBool("Effect", false);
            }
        }
        int rand_Dice_X1 = Random.Range(-210, -230);
        int rand_Dice_X2 = Random.Range(210, 230);
        int rand_Dice_Y1 = Random.Range(40, 60);
        int rand_Dice_Y2 = Random.Range(40, 60);

        Dice1.position = new Vector3(rand_Dice_X1 * 0.01f, rand_Dice_Y1 * 0.01f, 0);
        Dice2.position = new Vector3(rand_Dice_X2 * 0.01f, rand_Dice_Y2 * 0.01f, 0);

        image1.sprite = AtkPocket[rand1];
        image2.sprite = DefPocket[rand2];
        yield return new WaitForSeconds(delay);
        StartMethod();
    }

    public void StartMethod()
    {
        coroutine = DiceRolling(Dice1_Image, Dice2_Image);
        StartCoroutine(coroutine);
    }
    public void OnPlayButton()
    {
        Attack_Button.interactable = false;
        Dice_Button.interactable = true;
        Play_Button.gameObject.SetActive(false);
    }

    public void OnDiceButton()
    {
        if (!rollingFlag)
        {
            rollingFlag = true;
            delay = 0.01f;

            SFX_Audio.loop = true;
            SFX_Audio.clip = dice_roll_SFX;
            SFX_Audio.Play();

            StartMethod();
            Dice_Button.GetComponent<Image>().sprite = PauseSprite;
        }
        else
        {
            rollingFlag = false;

            SFX_Audio.loop = false;
            SFX_Audio.clip = dice_end_SFX;
            SFX_Audio.Play();

            Dice_Button.GetComponent<Image>().sprite = PlaySprite;
            Dice_Button.interactable = false;
        }
    }

    public void OnAttackButton()
    {
        atkSum = AtkDiceToInt(Dice1_Image);
        defSum = DefDiceToInt(Dice2_Image);
        Dice_Button.interactable = true;
        Attack_Button.interactable = false;
    }

    private int AtkDiceToInt(Image image)
    {
        switch (image.sprite.name) {
            case "AtkDice1":
                return 1;
            case "AtkDice2":
                return 2;
            case "AtkDice3":
                return 3;
            case "AtkDice4":
                return 4;
            case "AtkDice5":
                return 5;
            case "AtkDice6":
                return 6;
        }
        return 0;
    }

    private int DefDiceToInt(Image image)
    {
        switch (image.sprite.name)
        {
            case "DefDice1":
                return 1;
            case "DefDice2":
                return 2;
            case "DefDice3":
                return 3;
            case "DefDice4":
                return 4;
            case "DefDice5":
                return 5;
            case "DefDice6":
                return 6;
        }
        return 0;
    }
}