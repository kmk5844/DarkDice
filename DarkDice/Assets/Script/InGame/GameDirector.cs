using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    int MonsterCount = 0; // 해당 몬스터의 수가 딱 맞으면, 종료하는 카운트(?)
    int ItemCount = 0; //??

    public TextMeshProUGUI RoundText;
    public TextMeshPro[] InGameText;
    public TextMeshProUGUI[] StatusText;

    public GameObject Play_Button; //전투 개시 버튼
    public GameObject PlayDice_UI;//주사위 굴릴 때 나오는 UI
    public GameObject Play_UI; // 전체적인 플레이 UI
    public GameObject Win_UI;  // 이겼을 때의 UI
    public GameObject Lose_UI; // 졌을 때의 UI
    public GameObject PlayerObject; //플레이어 오브젝트

    public Transform mosterGroup; //몬스터 오브젝트
    GameObject[] monster;
    int mosterChildCount;

    public bool ItemFlag = false; //플레이 당시의 아이템을 사용할건지 말건지
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;

    public Button AtkButton;
    public Button DiceButton;
    public GameObject Dice_Director;
    public GameObject Monster_Director;

    Player_Scritable playerData;
    Monster_Scritable monsterData;

    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;

    public GameObject stage_Data;
    Stage_Scripter stage;

    public GameObject ON_OFF_ItemGroup;

    void Start()
    {
        mosterChildCount = mosterGroup.childCount;
        monster = new GameObject[mosterChildCount];
        for (int i = 0; i < mosterChildCount; i++)
        {
            monster[i] = mosterGroup.GetChild(i).gameObject;
        }

        RoundText.text = RoundNum + " 라운드";
        item = new Item_Scritable[ItemObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        monsterData = monster[MonsterCount].GetComponent<Monster_Scritable>();
        stage = stage_Data.GetComponent<Stage_Scripter>(); 

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
            Item_BackGround[i].GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        //이것도 변경할 예정 -> UI 변동 사항이 생기기 때문.
        StatusText[0].text = playerData.atk.ToString();
        StatusText[1].text = playerData.def.ToString();
        StatusText[2].text = monsterData.atk.ToString();
        StatusText[3].text = monsterData.def.ToString();

    }
    void Update()
    {
        StatusText[2].text = monsterData.atk.ToString();
        StatusText[3].text = monsterData.def.ToString();

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
        if (DiceNum == 2 && Dice_Director.GetComponent<Dice>().delay > 0.56f)
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
            ItemCount++;
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
                ItemCount++;
            }
            else if (ItemName == "DoubleDef")
            {
                ItemUse();
                Debug.Log("방어력 2배 적용!");
                defSum = playerData.def + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum * 2;
                ItemCount++;
            }
            else if(ItemName == "Heal")
            {
                ItemUse();
                Debug.Log("회복 성공!");
                playerData.hp += 1;
                Debug.Log("=======================================");
                ItemCount++;
            }
            ItemFlag = false;
        }
        Debug.Log("Player Atk : " + playerData.atk + " + " + playerData.weapon.WeaponAtk + " + " + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum + " = " + atksum);
        StatusText[0].text = atksum.ToString();
        Debug.Log("=======================================");

        if (atksum > monsterData.def)
        {
            monsterData.hp -= 1;
        }
        else if(atksum == monsterData.def)
        {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else
        {
            Debug.Log("공격 실패!");
            monsterData.hp -= 0.5f;
        }

        if (monsterData.hp <= 0)
        {
            MonsterCount++;
            if (monster.Length == MonsterCount)
            {
                Monster_Director.GetComponent<MonsterMoving>().monsterDie();
                Play_UI.SetActive(false);
                Win_UI.SetActive(true);
                stage.Win();
            }
            else
            {
                monsterData = monster[MonsterCount].GetComponent<Monster_Scritable>();
                Monster_Director.GetComponent<MonsterMoving>().monsterDie();
                StatusText[1].text = playerData.atk.ToString();
                StatusText[2].text = playerData.def.ToString();
                Play_Button.SetActive(true);
            }
        }
        else
        {
            StartCoroutine(monsterTurn());
        }
    }

    IEnumerator monsterTurn()
    {
        Debug.Log("몬스터 턴");
        StatusText[1].text = defSum.ToString();

        yield return new WaitForSeconds(monster_Atk_Delay);

        if(defSum < monsterData.atk)
        {
            Debug.Log("몬스터 공격 성공!");
            playerData.hp -= 1;
        }
        else if(defSum == monsterData.atk) {
            Debug.Log("서로 공격 맞음");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else
        {
            Debug.Log("몬스터 공격 실패!");
            playerData.hp -= 0.5f;
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
        yield return new WaitForSeconds(4f);
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

    public void OnMain()
    {
        SceneManager.LoadScene("1.StageChoice");
    }

    public void OnTry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
