using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class GameDirector : MonoBehaviour
{
    [SerializeField]
    private string ItemName;
    public GameTurn gameTurn;

    int RoundNum;
    int DiceNum;
    int atksum;
    int defSum;
    int MonsterCount; // �ش� ������ ���� �� ������, �����ϴ� ī��Ʈ
    int ItemCount;

    SkeletonAnimation playerAni;
    SkeletonAnimation monsterAni;
    Vector3 playerPosition;
    Vector3 monsterPosition;

    public TextMeshProUGUI RoundText;
    public TextMeshPro[] InGameText;
    public TextMeshProUGUI[] StatusText;

    public GameObject Play_Button; //���� ���� ��ư
    public GameObject PlayDice_UI;//�ֻ��� ���� �� ������ UI
    public GameObject Play_UI; // ��ü���� �÷��� UI
    public GameObject Win_UI;  // �̰��� ���� UI
    public GameObject Lose_UI; // ���� ���� UI
    public GameObject PlayerObject; //�÷��̾� ������Ʈ

    public Transform mosterGroup; //���� ������Ʈ
    GameObject[] monster;
    int mosterChildCount;

    public bool ItemFlag; //�÷��� ����� �������� ����Ұ��� ������
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;

    public Button AtkButton;
    public Button DiceButton;
    public GameObject Dice_Director;
    public GameObject Monster_Director;

    Player_Scritable playerData;
    MonsterData monsterData;

    public GameObject[] ItemObject_Data;
    Item_Scritable[] item;

    public GameObject stage_Data;
    Stage_Scripter stage;

    public DataTable Data;

    public Button ItemGroup_Button;
    bool ItemButton_OpenFlag;
    public GameObject Item_Group;
    Animator Ani_ItemGroup;

    GameManager gameManager;

    void Start()
    {
        ItemFlag = false;
        ItemButton_OpenFlag = false;
        ItemCount = 0;
        ItemName = "";
        RoundNum = 0;
        DiceNum = 0;
        MonsterCount = 0;
        mosterChildCount = mosterGroup.childCount;
        monster = new GameObject[mosterChildCount];
        Ani_ItemGroup = Item_Group.GetComponent<Animator>();

        playerAni = PlayerObject.GetComponentInChildren<SkeletonAnimation>();

        gameTurn = GameTurn.BeforeFight;
        try
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        catch
        {
            print("���� �Ŵ����� ���� ���̾�~");
        }

        for (int i = 0; i < mosterChildCount; i++)
        {
            monster[i] = mosterGroup.GetChild(i).gameObject;
        }

        RoundText.text = RoundNum + " ����";
        item = new Item_Scritable[ItemObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>();
        monsterData = monster[MonsterCount].GetComponent<MonsterData>();
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



        //�̰͵� ������ ���� -> UI ���� ������ ����� ����.
        StatusText[0].text = playerData.atk.ToString();
        StatusText[1].text = playerData.def.ToString();
        StatusText[2].text = monsterData.atk.ToString();
        StatusText[3].text = monsterData.def.ToString();
    }
    void LateUpdate()
    {
        if(Time.timeScale == 0)
        {
            return;
        }

        StatusText[2].text = monsterData.atk.ToString();
        StatusText[3].text = monsterData.def.ToString();

        if (ItemCount == 1)
        {
            ItemButton_OpenFlag = false;
            ItemGroup_Button.interactable = false;
            Ani_ItemGroup.SetBool("ItemOpenCloseFlag", false);
        }

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

        if (gameTurn == GameTurn.PlayerTurn_StartMoving)
        {
            if (playerAni.AnimationName != "Run")
            {
                playerAni.state.SetAnimation(0, "Run", true);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -2f)
            {
                PlayerObject.GetComponent<Transform>().Translate(10f * Time.deltaTime, 0, 0);
            }
            else
            {
                gameTurn = GameTurn.PlayerTurn_Attack;
            }
        }

        if(gameTurn == GameTurn.PlayerTurn_EndMoving)
        {
            PlayerObject.transform.localScale = new Vector3(-1, 1, 1);
            if (playerAni.AnimationName != "Run")
            {
                playerAni.state.SetAnimation(0, "Run", true);
            }


            if (PlayerObject.GetComponent<Transform>().position.x - playerPosition.x >= 0)
            {
                PlayerObject.GetComponent<Transform>().Translate(-10f * Time.deltaTime, 0, 0);
            }
            else
            {
                PlayerObject.transform.localScale = new Vector3(1, 1, 1);
                if(playerAni.AnimationName != "Idle")
                {
                    playerAni.state.SetAnimation(0, "Idle", true);
                }

                gameTurn = GameTurn.Waiting;
            }
        }

        if (gameTurn == GameTurn.MonsterTurn_StartMoving)
        {
            if (monsterAni.AnimationName != "Walk")
            {
                monsterAni.state.SetAnimation(0, "Walk", true);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -3f)
            {
                monster[MonsterCount].GetComponent<Transform>().Translate(-10f * Time.deltaTime, 0, 0);
            }
            else
            {
                gameTurn = GameTurn.MonsterTurn_Attack;
            }
        }

        if(gameTurn == GameTurn.MonsterTurn_EndMoving)
        {
            float scale = monster[MonsterCount].transform.localScale.y;
            monster[MonsterCount].transform.localScale = new Vector3(scale, scale, scale);
            if (monsterAni.AnimationName != "Walk")
            {
                monsterAni.state.SetAnimation(0, "Walk", true);
            }


            if (monster[MonsterCount].GetComponent<Transform>().position.x - monsterPosition.x <= 0)
            {
                monster[MonsterCount].GetComponent<Transform>().Translate(10f * Time.deltaTime, 0, 0);
            }
            else
            {
                monster[MonsterCount].transform.localScale = new Vector3(-scale, scale, scale);
                if (monsterAni.AnimationName != "Idle")
                {
                    monsterAni.state.SetAnimation(0, "Idle", true);
                }

                gameTurn = GameTurn.Waiting;
            }
        }

        // ���� �������� ���� ��쿡 �ٸ� ������ ������ �ȵǵ��� �ϱ�
        // �⺻������ ���� ����Ͽ� ������ �׷� �ݰ� �� �� ��ưŬ�� ��Ȱ��ȭ
        // -> ������ ������, ������ �׷��� ��ü������ �ݱ�
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

    public void ChanceToggleOff()
    {
        DiceNum++;
        if (DiceNum == 3 && ItemName == "Chance")
        {
            ItemUse();
            ItemCount++;
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
        RoundText.text = RoundNum + " ����";
        DiceNum = 0;
        atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum;
        defSum = playerData.def +  GameObject.Find("DiceDirector").GetComponent<Dice>().defSum;
        if (ItemFlag == true)
        {
            if (ItemName == "DoubleAtk")
            {
                ItemUse();
                Debug.Log("���ݷ� 2�� ����!");
                atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum * 2;
                ItemCount++;
            }
            else if (ItemName == "DoubleDef")
            {
                ItemUse();
                Debug.Log("���� 2�� ����!");
                defSum = playerData.def + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum * 2;
                ItemCount++;
            }
            else if(ItemName == "Heal")
            {
                ItemUse();
                Debug.Log("ȸ�� ����!");
                playerData.hp += 1;
                Debug.Log("=======================================");
                ItemCount++;
            }
            ItemFlag = false;
        }

        StartCoroutine(playerTurn());
    }

    IEnumerator playerTurn()
    {
        Debug.Log("�÷��̾� ��");

        Debug.Log("Player Atk : " + playerData.atk + " + " + playerData.weapon.WeaponAtk + " + " + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum + " = " + atksum);
        StatusText[0].text = atksum.ToString();
        Debug.Log("=======================================");

        //�� �������� �̵� ��, ������
        playerPosition = PlayerObject.GetComponent<Transform>().position;
        monsterPosition = monster[MonsterCount].GetComponent<Transform>().position;
        gameTurn = GameTurn.PlayerTurn_StartMoving;

        yield return new WaitUntil(() => gameTurn == GameTurn.PlayerTurn_Attack);
        playerAni.state.SetAnimation(0, "Attack1", false).TimeScale = 2f;

        if (atksum > monsterData.def)
        {
            Debug.Log("���� ����!");
            monsterData.hp -= 1;
        }
        else if (atksum == monsterData.def)
        {
            Debug.Log("���� ���� ����");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else
        {
            Debug.Log("���� ����!");
            monsterData.hp -= 0.5f;
        }

        if (monsterData.hp == 0)
        {
            monsterData.hp = 0;
        }

        yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Attack1", false));

        if (monsterData.hp == 0)
        {
            monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
            monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
        }

        gameTurn = GameTurn.PlayerTurn_EndMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.Waiting);

        if (monsterData.hp == 0)
        {
            StartCoroutine(monsterDieDelay());
        }
        else
        {
            StartCoroutine(monsterTurn());
        }
    }

    IEnumerator monsterTurn()
    {
        Debug.Log("���� ��");
        StatusText[1].text = defSum.ToString();
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
        gameTurn = GameTurn.MonsterTurn_StartMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.MonsterTurn_Attack);

        if(defSum < monsterData.atk)
        {
            Debug.Log("���� ���� ����!");
            playerData.hp -= 1;
        }
        else if(defSum == monsterData.atk) {
            Debug.Log("���� ���� ����");
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else
        {
            Debug.Log("���� ���� ����!");
            playerData.hp -= 0.5f;
        }

        monsterAni.state.SetAnimation(0, "Attack", false);
        yield return new WaitForSeconds(0.4f);
        playerAni.state.SetAnimation(0, "Hurt", false).TimeScale = 1.2f;
        yield return new WaitForSeconds(0.8f);

        gameTurn = GameTurn.MonsterTurn_EndMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.Waiting);

        if (playerData.hp <= 0 || RoundNum == 10)
        {
            playerData.hp = 0;
            StartCoroutine(playerDie());
        }
        else
        {
            PlayDice_UI.SetActive(true);
            ItemCount = 0;
            ItemGroup_Button.interactable = true;
            StatusText[0].text = playerData.atk.ToString();
            StatusText[1].text = playerData.def.ToString();
        }
    }
    
    IEnumerator playerDie()
    {
        playerAni.state.SetAnimation(0, "Death", false).TimeScale = 0.8f;
        yield return new WaitForSeconds(3f);
        Play_UI.SetActive(false);
        Lose_UI.SetActive(true);
    }

    IEnumerator monsterDieDelay()
    {
        yield return new WaitForSeconds(2f);
        MonsterCount++;
        gameTurn = GameTurn.BeforeFight;
        if (monster.Length == MonsterCount)
        {
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            Play_UI.SetActive(false);
            Win_UI.SetActive(true);
            TotalWin();
        }
        else
        {
            //�� �������� ������ �ִϸ��̼� ����
            monsterData = monster[MonsterCount].GetComponent<MonsterData>();
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            StatusText[1].text = playerData.atk.ToString();
            StatusText[2].text = playerData.def.ToString();
            Play_Button.SetActive(true);
            ItemCount = 0;
            ItemGroup_Button.interactable = true;
        }
    }

    public void OnAD_Try()
    {
        StartCoroutine(OnAD_Ani());
    }

    IEnumerator OnAD_Ani()
    {
        playerData.hp++;
        Time.timeScale = 1;
        PlayDice_UI.SetActive(true);
        Play_UI.SetActive(true);
        Lose_UI.transform.GetChild(1).GetComponent<Transform>().gameObject.SetActive(false);
        Lose_UI.SetActive(false);
        yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
        playerAni.state.SetAnimation(0, "Idle", true);
    }

    public void TotalWin()
    {
        stage.Win();
        for(int i = 0; i < Data.stage_Data.Count; i++)
        {
            if(stage.curretStageNum == Data.stage_Data[i].number)
            {
                playerData.plusCoin(Data.stage_Data[i].reward_coin);
                if(stage.curretStageNum == stage.stageNum)
                {
                    playerData.plusStatus(Data.stage_Data[i].reward_point);
                    if (Data.stage_Data[i].reward_hp != 0)
                    {
                        playerData.plusHp(Data.stage_Data[i].reward_hp);
                    }
                }
                break;
            }
        }
    }

    public void OnMain()
    {
        try
        {
            gameManager.NextLevle("1.StageChoice");
        }
        catch
        {
            SceneManager.LoadScene("1.StageChoice");
        }
        Time.timeScale = 1;
    }

    public void OnClickItemButton()
    {
        if (!ItemButton_OpenFlag)
        {
            Ani_ItemGroup.SetBool("ItemOpenCloseFlag", true);
            ItemButton_OpenFlag = true;
        }
        else
        {
            Ani_ItemGroup.SetBool("ItemOpenCloseFlag", false);
            ItemButton_OpenFlag = false;
        }
    }
}

public enum GameTurn { 
    BeforeFight,
    Fighting,
    PlayerTurn_StartMoving,
    PlayerTurn_Attack,
    PlayerTurn_EndMoving,
    Waiting,
    MonsterTurn_StartMoving,
    MonsterTurn_Attack,
    MonsterTurn_EndMoving
}