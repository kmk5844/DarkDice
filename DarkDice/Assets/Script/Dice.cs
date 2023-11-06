using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField]
    float delay = 0.01f;

    public Sprite[] Pocket;
    public Image Dice1;
    public Image Dice2;
    public Image PocketDice1;
    public Image PocketDice2;
    public int attackSum = 0;
    public Button Play_Button;
    public Button Dice_Button;
    public Button Attack_Button;
    public bool rollingFlag = false;
    TextMeshProUGUI ButtonText;

    private void Start()
    {
        ButtonText = Dice_Button.GetComponentInChildren<TextMeshProUGUI>();
    }

    IEnumerator coroutine;

    IEnumerator DiceRolling(Image image1, Image image2, Image Pocket1, Image Pocket2)
    {
        int rand1 = Random.Range(0, 6);
        int rand2 = Random.Range(0, 6);
        if (rollingFlag == false)
        {
            delay += 0.05f;
            if (delay > 0.56f)
            {
                StopCoroutine(coroutine);
                Attack_Button.interactable = true;
                Pocket1.sprite = Pocket[rand1];
                Pocket2.sprite = Pocket[rand2];
            }
        }
        image1.sprite = Pocket[rand1];
        image2.sprite = Pocket[rand2];
        yield return new WaitForSeconds(delay);
        StartMethod();
    }

    void StartMethod()
    {
        coroutine = DiceRolling(Dice1, Dice2, PocketDice1, PocketDice2);
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
            StartMethod();
            ButtonText.text = "���߱�";
        }
        else
        {
            rollingFlag = false;
            ButtonText.text = "������";
            Dice_Button.interactable = false;
        }
    }

    public void OnAttackButton()
    {
        Play_Button.gameObject.SetActive(true);
        attackSum = PlusAttack(Dice1, Dice2);
        Dice_Button.interactable = true;
    }

    private int PlusAttack(Image image1, Image image2)
    {
        return DiceToInt(image1) + DiceToInt(image2);
    }

    private int DiceToInt(Image image)
    {
        switch (image.sprite.name) {
            case "Dice1":
                return 1;
            case "Dice2":
                return 2;
            case "Dice3":
                return 3;
            case "Dice4":
                return 4;
            case "Dice5":
                return 5;
            case "Dice6":
                return 6;
        }
        return 0;
    }
}