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
    public TextMeshProUGUI[] StatusText;

    public GameObject PlayDice_UI;//주사위 굴릴 때 나오는 UI
    public GameObject Play_UI; // 전체적인 플레이 UI
    public GameObject Lose_UI; // 졌을 때의 UI
    public GameObject PlayerObject;
    public GameObject MonsterObject;
    public bool ItemFlag = false;
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;
    public GameObject[] Item_CheckMark;

    public Button AtkButton;
    public Button DiceButton;
    public GameObject Dice_Director;

    public Player_Scritable playerData;
    public Monster_Scritable monsterData;

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

        StatusText[0].text = playerData.hp.ToString();
        StatusText[1].text = playerData.atk.ToString();
        StatusText[2].text = playerData.def.ToString();
        StatusText[3].text = monsterData.hp.ToString();
        StatusText[4].text = monsterData.atk.ToString();
        StatusText[5].text = monsterData.def.ToString();

    }
    void Update()
    {
        StatusText[0].text = playerData.hp.ToString();
        StatusText[3].text = monsterData.hp.ToString();

        if (RoundNum >= 7)
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
        // 기본적으로 공격 사용하여 아이템 그룹 닫게 한 후 버튼클릭 비활성화
        // -> 공격할 때마다, 아이템 그룹을 전체적으로 닫기
        if (DiceNum == 2)
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
                /*StatusText[0].text = playerData.hp.ToString();*/
                Debug.Log("=======================================");
            }
            ItemFlag = false;
        }
        Debug.Log("Player Atk : " + playerData.atk + " + " + playerData.weapon.WeaponAtk + " + " + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum + " = " + atksum);
        StatusText[1].text = atksum.ToString();
        Debug.Log("=======================================");

        if (atksum > monsterData.def)
        {
            monsterData.hp -= 1;
            /*StatusText[3].text = monsterData.hp.ToString();*/

        }
        else if(atksum == monsterData.def)
        {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            /*StatusText[0].text = playerData.hp.ToString();
            StatusText[3].text = monsterData.hp.ToString();*/
        }
        else
        {
            Debug.Log("공격 실패!");
            monsterData.hp -= 0.5f;
            /*StatusText[3].text = monsterData.hp.ToString();*/
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
        StatusText[2].text = defSum.ToString();

        yield return new WaitForSeconds(monster_Atk_Delay);

        if(defSum < monsterData.atk)
        {
            Debug.Log("몬스터 공격 성공!");
            playerData.hp -= 1;
            /*StatusText[0].text = playerData.hp.ToString();*/

        }
        else if(defSum == monsterData.atk) {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
            /*StatusText[0].text = playerData.hp.ToString();
            StatusText[3].text = monsterData.hp.ToString();*/
        }
        else
        {
            Debug.Log("몬스터 공격 실패!");
            playerData.hp -= -0.5f;
            /*StatusText[0].text = playerData.hp.ToString();*/
        }

        if (playerData.hp <= 0 || RoundNum == 10)
        {
            playerData.hp = 0;
            StartCoroutine(playerDie());
        }
        else
        {
            PlayDice_UI.SetActive(true);
            StatusText[1].text = playerData.atk.ToString();
            StatusText[2].text = playerData.def.ToString();
        }
    }
    
    IEnumerator playerDie()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0;
        Play_UI.SetActive(false);
        Lose_UI.SetActive(true);
    }

    public void OnAD_Retry()
    {
        playerData.hp++;
        Time.timeScale = 1;
        PlayDice_UI.SetActive(true);
        Play_UI.SetActive(true);
        Lose_UI.SetActive(false);
    }
}
