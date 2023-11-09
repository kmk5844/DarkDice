using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private float monster_Atk_Delay = 2.0f;
    [SerializeField]
    private string ItemName = "";
    
    public bool atk_or_def; //���� false��� all atk, true��� atk �� def �й�

    int DiceNum = 0;
    int atksum;
    int defSum;

    public bool ItemFlag = false;
    public Toggle[] Item_Toggle;

    public Button DiceButton;

    Player_Scritable playerData;
    Monster_Scritable monsterData;

    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;

    void Start()
    {
        item = new Item_Scritable[ItemObject_Data.Length];
        for(int i = 0; i < ItemObject_Data.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
        }
        playerData = GameObject.Find("Player").GetComponent<Player_Scritable>();
        monsterData = GameObject.Find("Monster").GetComponent<Monster_Scritable>();
    }

    void Update()
    {
        if (Item_Toggle[0].isOn)
        {
            ItemName = Item_Toggle[0].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (Item_Toggle[1].isOn)
        {
            ItemName = Item_Toggle[1].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (Item_Toggle[2].isOn)
        {
            ItemName = Item_Toggle[2].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
        }
        else if (!Item_Toggle[0].isOn && !Item_Toggle[1].isOn && !Item_Toggle[2].isOn)
        {
            ItemFlag = false;
            ItemName = "";
        }
        
        if(DiceNum == 2)
        {
            if(ItemName == "Chance")
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
        if (DiceNum == 3 && ItemName == "Chance")
        {
            ItemUse();
            for(int i = 0; i < item.Length; i++)
            {
                if (Item_Toggle[i].isOn)
                {
                    Item_Toggle[i].isOn = false;
                    Item_Toggle[i].interactable = false;
                }
            }
        }
    }

    public void OnFightButton()
    {
        DiceNum = 0;
        atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum;
        defSum = playerData.def +  GameObject.Find("DiceDirector").GetComponent<Dice>().defSum;
        if (ItemFlag == true)
        {
            if(ItemName == "DoubleAtk")
            {
                ItemUse();
                Debug.Log("���ݷ� 2�� ����!");
                atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum * 2;
            }
            else if(ItemName == "Heal")
            {
                ItemUse();
                Debug.Log("ȸ�� ����!");
                playerData.hp += 1;
                Debug.Log("Player Data : " + playerData.hp);
                Debug.Log("=======================================");
            }else if(ItemName == "DoubleDef")
            {
                ItemUse();
                Debug.Log("���� 2�� ����!");
                defSum = playerData.def + GameObject.Find("DiceDirector").GetComponent <Dice>().defSum * 2;
            }
            ItemFlag = false;
        }
        Debug.Log("Player Atk : " + playerData.atk + " + " + playerData.weapon.WeaponAtk + " + " + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum + " = " + atksum);
        Debug.Log(atksum + " �������� ���� �õ�!");
        Debug.Log("=======================================");

        if (atksum > monsterData.def)
        {
            monsterData.hp -= 1;
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else if(atksum == monsterData.def)
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
        Debug.Log("Player Def : " + playerData.def + " + "  + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum + " = " + defSum);

        yield return new WaitForSeconds(monster_Atk_Delay);

        if(defSum < monsterData.atk)
        {
            Debug.Log("���� ���� ����!");
            playerData.hp -= 1;
            Debug.Log("Player Data : " + playerData.hp);
        }
        else if(defSum == monsterData.atk) {
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

    public void ItemUse()
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].item_name == ItemName)
            {
                item[i].UseItem();
            }
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
