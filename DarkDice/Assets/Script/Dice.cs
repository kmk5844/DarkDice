using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField]
    float delay = 0.1f;

    public Sprite[] Poket;
    public Image Dice1;
    public Image Dice2;
    public Button button;
    bool rollingFlag = false;
    TextMeshProUGUI ButtonText;

    private void Start()
    {
        ButtonText = button.GetComponentInChildren<TextMeshProUGUI>();
    }

    IEnumerator coroutine;

    IEnumerator DiceRolling(Image image1, Image image2)
    {
        yield return new WaitForSeconds(delay);
        int rand1 = Random.Range(0, 6);
        int rand2= Random.Range(0, 6);
        image1.sprite = Poket[rand1];
        image2.sprite = Poket[rand2];
        StartMethod();
    }

    void StartMethod()
    {
        coroutine = DiceRolling(Dice1, Dice2);
        StartCoroutine(coroutine);
    }

    void StopMethod()
    {
        StopCoroutine(coroutine);
    }

    public void OnRollingDice()
    {
        if (!rollingFlag)
        {
            StartMethod();
            ButtonText.text = "∏ÿ√ﬂ±‚";
            rollingFlag = true;
        }
        else
        {
            StopMethod();
            ButtonText.text = "±º∏Æ±‚";
            rollingFlag = false;
            Debug.Log(Dice1.sprite.name);
            Debug.Log(Dice2.sprite.name);
        }
    }

}
