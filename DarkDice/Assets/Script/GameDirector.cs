using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private float monster_Atk_Delay = 2.0f;
    [SerializeField]
    public string ItemName = "";

    int DiceNum = 0;
    public bool ItemFlag = false;
    public Toggle Item1;
    public Toggle Item2;
    public Toggle Item3;

    public Button DiceButton;

    Player playerData;
    Monster monsterData;
   
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<Player>();
        monsterData = GameObject.Find("Monster").GetComponent<Monster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Item1.isOn)
        {
            ItemName = Item1.GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (Item2.isOn)
        {
            ItemName = Item2.GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (Item3.isOn)
        {
            ItemName = Item3.GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (!Item1.isOn && !Item2.isOn && !Item3.isOn)
        {
            ItemFlag = false;
            ItemName = "";
        }
        
        if(DiceNum == 2)
        {
            if(ItemName == "Dice3")
            {
                DiceButton.interactable = true;
            }
            else
            {
                DiceButton.interactable = false;
            }
        }
        
    }

    public void CountDice()
    {
        DiceNum += 1;
    }

    public void ChanceToggleOff()
    {
        DiceNum++;
        if (DiceNum == 3 && ItemName == "Dice3")
        {
            if (Item1.isOn)
            {
                Item1.isOn = false;
                Item1.interactable = false;
            }
            else if (Item2.isOn)
            {
                Item2.isOn = false;
                Item2.interactable = false;
            }
            else if (Item3.isOn)
            {
                Item3.isOn = false;
                Item3.interactable = false;
            }
        }
    }

    public void OnFightButton()
    {
        DiceNum = 0;
        int sum;
        sum = playerData.atk + GameObject.Find("DiceDirector").GetComponent<Dice>().attackSum;
        if (ItemFlag == true)
        {
            if(ItemName == "Dice1")
            {
                Debug.Log("2배 적용!");
                sum = sum * 2 - playerData.atk;
            }
            else if(ItemName == "Dice2")
            {
                Debug.Log("회복 성공!");
                playerData.hp += 1;
                Debug.Log("Player Data : " + playerData.hp);
            }
            ItemFlag = false;
        }

        Debug.Log(sum + " 데미지로 공격 시도!");
        Debug.Log("=======================================");

        if (sum > monsterData.def)
        {
            monsterData.hp -= 1;
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else if(sum == monsterData.def)
        {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            Debug.Log("Player Data : " + playerData.hp);
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else
        {
            Debug.Log("공격 실패!");
            monsterData.hp -= 0.5f;
            Debug.Log("Monster Data : " + monsterData.hp);
        }

        if (monsterData.hp == 0)
        {
            Debug.Log("용사 승리");
        }
        else
        {
            StartCoroutine(monsterTurn());
        }
    }

    IEnumerator monsterTurn()
    {
        Debug.Log("몬스터 턴");
        yield return new WaitForSeconds(monster_Atk_Delay);

        if(playerData.def < monsterData.atk)
        {
            Debug.Log("몬스터 공격 성공!");
            playerData.hp -= 1;
            Debug.Log("Player Data : " + playerData.hp);
        }
        else if(playerData.def == monsterData.atk) {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            Debug.Log("Player Data : " + playerData.hp);
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else
        {
            Debug.Log("몬스터 공격 실패!");
            playerData.hp -= -0.5f;
            Debug.Log("Player Data : " + playerData.hp);
        }

        if (playerData.hp == 0)
        {
            Debug.Log("용사 패배");
        }
    }

    public void ItemToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            switch (toggle.name)
            {
                case "Item1":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    break;
                case "Item2":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    break;
                case "Item3":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    break;
            }
        }
    }
}
