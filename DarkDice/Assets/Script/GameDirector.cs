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
    public bool ItemFlag = false;
    public Toggle Item1;
    public Toggle Item2;
    public Toggle Item3;

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

    }

    public void OnFightButton()
    {

        int sum;
        sum = playerData.atk + GameObject.Find("DiceDirector").GetComponent<Dice>().attackSum;
        if (ItemFlag == true)
        {
            if(ItemName == "Dice1")
            {
                Debug.Log("2�� ����!");
                sum = sum * 2 - playerData.atk;
            }
            else if(ItemName == "Dice2")
            {
                Debug.Log("ȸ�� ����!");
                playerData.hp += 1;
                Debug.Log("Player Data : " + playerData.hp);
            }
            ItemFlag = false;
        }

        Debug.Log(sum + " �������� ���� �õ�!");
        Debug.Log("=======================================");

        if (sum > monsterData.def)
        {
            monsterData.hp -= 1;
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else if(sum == monsterData.def)
        {
            Debug.Log("���� ���� ����");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            Debug.Log("Player Data : " + playerData.hp);
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else
        {
            Debug.Log("���� ����!");
            monsterData.hp -= 0.5f;
            Debug.Log("Monster Data : " + monsterData.hp);
        }

        if (monsterData.hp == 0)
        {
            Debug.Log("��� �¸�");
        }
        else
        {
            StartCoroutine(monsterTurn());
        }
    }

    IEnumerator monsterTurn()
    {
        Debug.Log("���� ��");
        yield return new WaitForSeconds(monster_Atk_Delay);

        if(playerData.def < monsterData.atk)
        {
            Debug.Log("���� ���� ����!");
            playerData.hp -= 1;
            Debug.Log("Player Data : " + playerData.hp);
        }
        else if(playerData.def == monsterData.atk) {
            Debug.Log("���� ���� ����");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            Debug.Log("Player Data : " + playerData.hp);
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else
        {
            Debug.Log("���� ���� ����!");
            playerData.hp -= -0.5f;
            Debug.Log("Player Data : " + playerData.hp);
        }

        if (playerData.hp == 0)
        {
            Debug.Log("��� �й�");
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
