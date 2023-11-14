using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private float monster_Atk_Delay = 2.0f;
    [SerializeField]
    private string ItemName = "";

    int RoundNum = 0;
    int DiceNum = 0;
    int atksum;
    int defSum;

    public TextMeshProUGUI RoundText;
    public GameObject PlayDice_UI;
    public GameObject PlayerObject;
    public GameObject MonsterObject;
    public bool ItemFlag = false;
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;
    public GameObject[] Item_CheckMark;

    public Button AtkButton;
    public Button DiceButton;
    public GameObject Dice_Director;

    Player_Scritable playerData;
    Monster_Scritable monsterData;

    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;

    public GameObject ON_OFF_ItemGroup;

    void Start()
    {
        RoundText.text = RoundNum + " 라운드";
        item = new Item_Scritable[ItemObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        monsterData = MonsterObject.GetComponent<Monster_Scritable>();

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>();
        }

        for (int i = 0; i < Item_Toggle.Length; i++)
        {
            if (playerData.item[i].ItemName == "Default")
            {
                Item_Toggle[i].interactable = false;
            }
        }

        for (int i = 0; i < Item_Toggle.Length; i++)
        {
            Item_BackGround[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;
            Item_CheckMark[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage;

        }
    }
    void Update()
    {
        if(RoundNum >= 7)
        {
            RoundText.color = Color.red;
        }

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
        
        // 찬스 아이템을 썻을 경우에 다른 아이템 선택이 안되도록 하기
        // 기본적으로 공격사용하여 아이템 그룹 닫게 한 후 버튼클릭 비활성화
        if(DiceNum == 2)
        {
            if(ItemName == "Chance")
            {
                DiceButton.interactable = true;
                AtkButton.interactable = false;
            }
            else
            {
                DiceButton.interactable = false;
                if (Dice_Director.GetComponent<Dice>().delay > 0.56f)
                {
                    AtkButton.interactable = true;
                }
            }
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

    public void OnClickItemGroup()
    {
        if (ON_OFF_ItemGroup.gameObject.activeSelf == true)
        {
            ON_OFF_ItemGroup.SetActive(false);
        }
        else
        {
            ON_OFF_ItemGroup.SetActive(true);
        }
    }

    public void ChanceToggleOff()
    {
        DiceNum++;
        if (DiceNum == 3 && ItemName == "Chance")
        {
            ItemUse();
            for(int i = 0; i < Item_Toggle.Length; i++)
            {
                if (Item_Toggle[i].isOn)
                {
                    Item_Toggle[i].isOn = false;
                    Item_Toggle[i].interactable = false;
                }
            }
        }
    }
    public void CountDice()
    {
        DiceNum += 1;
    }


    //Play Round
    public void OnFightButton()
    {
        RoundNum++;
        RoundText.text = RoundNum + " 라운드";
        DiceNum = 0;
        atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum;
        defSum = playerData.def +  GameObject.Find("DiceDirector").GetComponent<Dice>().defSum;
        if (ItemFlag == true)
        {

            if (ItemName == "DoubleAtk")
            {
                ItemUse();
                Debug.Log("공격력 2배 적용!");
                atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum * 2;
            }
            else if (ItemName == "DoubleDef")
            {
                ItemUse();
                Debug.Log("방어력 2배 적용!");
                defSum = playerData.def + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum * 2;
            }
            else if(ItemName == "Heal")
            {
                ItemUse();
                Debug.Log("회복 성공!");
                playerData.hp += 1;
                Debug.Log("Player Data : " + playerData.hp);
                Debug.Log("=======================================");
            }
            ItemFlag = false;
        }
        Debug.Log("Player Atk : " + playerData.atk + " + " + playerData.weapon.WeaponAtk + " + " + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum + " = " + atksum);
        Debug.Log(atksum + " 데미지로 공격 시도!");
        Debug.Log("=======================================");

        if (atksum > monsterData.def)
        {
            monsterData.hp -= 1;
            Debug.Log("Monster Data : " + monsterData.hp);
        }
        else if(atksum == monsterData.def)
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
        Debug.Log("Player Def : " + playerData.def + " + "  + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum + " = " + defSum);

        yield return new WaitForSeconds(monster_Atk_Delay);

        if(defSum < monsterData.atk)
        {
            Debug.Log("몬스터 공격 성공!");
            playerData.hp -= 1;
            Debug.Log("Player Data : " + playerData.hp);
        }
        else if(defSum == monsterData.atk) {
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
        }else if(RoundNum == 10)
        {
            Debug.Log("용사 패배");
        }
        else
        {
            PlayDice_UI.SetActive(true);
        }
    }    
}
