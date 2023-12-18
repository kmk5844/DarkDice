using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;
using Unity.VisualScripting;

public class GameDirector : MonoBehaviour
{

    public GameTurn gameTurn;
    Player_Scritable playerData; // �÷��̾� ���� �����͸� �ҷ��´�.
    MonsterData monsterData;

    public GameObject[] ItemObject_Data; // ������ ���� �����͸� �ҷ��´�.
    Item_Scritable[] item;

    public GameObject stage_Data; // �������� ���� �����͸� �ҷ��´�(���� Ŭ����� ����)
    Stage_Scripter stage;
    public DataTable Data; // ���� ���� �� �������� ������ �������� ������ ���̺�
    GameManager gameManager;
    public GameObject Dice_Director; //�ֻ��� ���� ������
    public GameObject Monster_Director; // ���� ���� ������

    [SerializeField]
    private string ItemName; //����Ϸ��� ������
    public bool ItemFlag; //�÷��� ����� �������� ����Ұ��� ������
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;

    int RoundNum; //���� ����
    int DiceNum; //�ֻ����� ���� Ƚ�� -> ���� ������ ����� ��쿡 �̿���
    int atksum; // ���ݷ�
    int defSum; // ����
    int MonsterCount; // �ش� ������ ���� �� ������, �����ϴ� ī��Ʈ
    int ItemCount; // ������ Ƚ�� -> ���� �������� ���� ��, �� �ٸ� ������ ������� �ʵ��� ��
    int conditionsDefeat; //�й� ����

    SkeletonAnimation playerAni; //�÷��̾� �ִϸ��̼�
    SkeletonAnimation monsterAni; // ���� �ִϸ��̼�
    Vector3 playerPosition; // �÷��̾� ������
    Vector3 monsterPosition; // ���� ������

    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI Player_Atk_Text;
    public TextMeshProUGUI Player_Def_Text;
    public TextMeshProUGUI Monster_Atk_Text;
    public TextMeshProUGUI Monster_Def_Text;

    public GameObject Play_Button; //���� ���� ��ư
    public GameObject PlayDice_UI;//�ֻ��� ���� �� ������ UI
    public GameObject Play_UI; // ��ü���� �÷��� UI
    public GameObject Win_UI;  // �̰��� ���� UI
    public TextMeshProUGUI Win_UI_Button_Text; // �������� 5�� ��쿡, �ؽ�Ʈ ����
    public GameObject Lose_UI; // ���� ���� UI
    public GameObject PlayerObject; //�÷��̾� ������Ʈ
    public GameObject Hurt_Image; //�¾��� ���� UI

    public Transform mosterGroup; //���� �׷� ������Ʈ
    int mosterChildCount; // ���� �׷쿡 �ִ� ������ Ƚ��
    GameObject[] monster;

    public Button AtkButton;
    public Button DiceButton;
    public Button ItemGroup_Button; // ������ â�� ���� ��ư
    bool ItemButton_OpenFlag;

    public TextMeshProUGUI reward_Coin; // ���� ���� �ؽ�Ʈ ����
    public Image[] reward_Image; // ���� �̹��� ����

    public GameObject Item_Group; // ������ �׷쿡 �ִ� �ִϸ��̼��� ���ϱ� ����
    Animator Ani_ItemGroup;

    //��ƼŬ �κ�-----------------------------------------------------------------------------------
    public GameObject[] Hit_Part; //0, 1�� �÷��̾� ��Ʈ, 2�� ���� ��Ʈ, 3�� ���� ��Ʈ 4�� ���潺
    public GameObject[] Buff_Part; //0�� ��, 1�� ��Ȱ, 2�� atk, 3�� def
    public GameObject Heal_Part;
    public GameObject Revival_Part;
    public GameObject[] ItemChoice_Part;

    public Transform BackGround;

    AudioSource Sound_BGM;
    InGame_Sound Sound_SFX;

    void Start()
    {
        Sound_BGM = GameObject.Find("Bgm").GetComponent<AudioSource>();
        Sound_SFX = GetComponent<InGame_Sound>();

        ItemName = "";
        RoundNum = 0;
        DiceNum = 0;
        MonsterCount = 0;
        ItemCount = 0;

        if (SceneManager.GetActiveScene().name.Equals("Stage5"))
        {
            conditionsDefeat = 15;
        }
        else
        {
            conditionsDefeat = 10;
        }

        playerAni = PlayerObject.GetComponentInChildren<SkeletonAnimation>();
        mosterChildCount = mosterGroup.childCount;
        monster = new GameObject[mosterChildCount];
        ItemFlag = false;

        ItemButton_OpenFlag = false;
        Ani_ItemGroup = Item_Group.GetComponent<Animator>();

        gameTurn = GameTurn.BeforeFight;
        try
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        catch
        {
            Debug.Log("���� �Ŵ����� ���� ���̾�~");
        }

        for (int i = 0; i < mosterChildCount; i++)
        {
            monster[i] = mosterGroup.GetChild(i).gameObject; //���� �׷쿡 �ִ� ���͸� ������ �Ѵ�.
        }

        RoundText.text = RoundNum + " ����";
        item = new Item_Scritable[ItemObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>(); // �÷��̾� �����͸� �ҷ��´�.
        monsterData = monster[MonsterCount].GetComponent<MonsterData>(); // ���� �����͸� �ҷ��´�.
        stage = stage_Data.GetComponent<Stage_Scripter>(); // �������� �����͸� �ҷ��´�.


        //������������ ��� ����
        if(stage.curretStageNum == 1 || stage.curretStageNum == 2)
        {
            BackGround.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Background/Stage1&2");
        }else if(stage.curretStageNum == 3)
        {
            BackGround.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Background/Stage3");
        }
        else if(stage.curretStageNum == 4)
        {
            BackGround.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Background/Stage4");
        }else if(stage.curretStageNum == 5)
        {
            BackGround.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Background/Stage5");
        }

        //���� ������ ��� �̵� / 3������ ��쿡 ��� ���� �þ
        if (mosterChildCount == 1)
        {
            BackGround.position = new Vector3(-2, 1, 700);
        }
        else if(mosterChildCount == 2)
        {
            BackGround.position = new Vector3(19, 1, 700);
        }
        else if(mosterChildCount == 3)
        {
            BackGround.position = new Vector3(8, 1, 700);
            BackGround.localScale = new Vector3(16, 1, 1.7f);
        }

        for (int i = 0; i < ItemObject_Data.Length; i++)
        {
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>(); // ������ �����͸� �ҷ��´�.
        }

        for (int i = 0; i < Item_Toggle.Length; i++)
        {
            if (playerData.item[i].ItemName == "Default")
            {
                Item_Toggle[i].interactable = false; // ���� ������ ������ �⺻�̶�� ��ٴ�.
            }
            Item_BackGround[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage; //��׶���� �̹����� �����Ѵ�.
            Item_BackGround[i].GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        /*        for (int i = 0; i < Item_Toggle.Length; i++)
                {
                    Item_BackGround[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage; //��׶���� �̹����� �����Ѵ�.
                    Item_BackGround[i].GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = playerData.item[i].ItemImage;
                }*/

        Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
        Player_Def_Text.text = playerData.def.ToString();
        Monster_Atk_Text.text = monsterData.atk.ToString();
        Monster_Def_Text.text = monsterData.def.ToString();
    }
    void LateUpdate()
    {
        if(Time.timeScale == 0)
        {
            return; // Ÿ�ӽ������� 0�̶�� �����.
        }

        Monster_Atk_Text.text = monsterData.atk.ToString();
        Monster_Def_Text.text = monsterData.def.ToString();

        if (ItemCount == 1) // ������ ��� ���� ���, �ݴ´�.
        {
            ItemButton_OpenFlag = false;
            ItemGroup_Button.interactable = false;
            Ani_ItemGroup.SetBool("ItemOpenCloseFlag", false);
        }

        if (playerData.hp <= 0) // -0.5 ��Ȳ�� ������ �ʵ��� ����
        {
            playerData.hp = 0;
        }

        if(monsterData.hp <= 0)
        {
            monsterData.hp = 0;
        }

        if (conditionsDefeat == 10)
        {
            if (RoundNum >= 7)
            {
                RoundText.color = Color.red; // 7���� �̻��� �Ǹ� ���� ����
            }
        }else if(conditionsDefeat == 15)
        {
            if (RoundNum >= 12)
            {
                RoundText.color = Color.red; // 7���� �̻��� �Ǹ� ���� ����
            }
        }
        

        if (Item_Toggle[0].isOn) //�������� Ŭ�� ���� ��, ����� ������ ���� �� ��ƼŬ ����
        {
            ItemName = Item_Toggle[0].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
            ItemChoice_Part[0].SetActive(true);
            ItemChoice_Part[1].SetActive(false);
            ItemChoice_Part[2].SetActive(false);
        }
        else if (Item_Toggle[1].isOn)
        {
            ItemName = Item_Toggle[1].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
            ItemChoice_Part[0].SetActive(false);
            ItemChoice_Part[1].SetActive(true);
            ItemChoice_Part[2].SetActive(false);
        }
        else if (Item_Toggle[2].isOn)
        {
            ItemName = Item_Toggle[2].GetComponentInChildren<Image>().sprite.name;
            ItemFlag = true;
            ItemChoice_Part[0].SetActive(false);
            ItemChoice_Part[1].SetActive(false);
            ItemChoice_Part[2].SetActive(true);
        }
        else if (!Item_Toggle[0].isOn && !Item_Toggle[1].isOn && !Item_Toggle[2].isOn)
        {
            ItemFlag = false;
            ItemName = "";
            ItemChoice_Part[0].SetActive(false);
            ItemChoice_Part[1].SetActive(false);
            ItemChoice_Part[2].SetActive(false);
        }

        if (gameTurn == GameTurn.PlayerTurn_StartMoving) // �÷��̾ �����̱� ����
        {
            if (playerAni.AnimationName != "Run") 
            {
                playerAni.state.SetAnimation(0, "Run", true);
                Sound_SFX.PlayerWalk_SFX(0);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -3f)
            {
                PlayerObject.GetComponent<Transform>().Translate(10f * Time.deltaTime, 0, 0);
            } // ������ ��ġ���� ������
            else
            {
                if (playerAni.AnimationName != "Idle")
                {
                    playerAni.state.SetAnimation(0, "Idle", true);
                    Sound_SFX.PlayerWalk_SFX(1);
                }
                gameTurn = GameTurn.PlayerTurn_Attack; // �������� �÷��̾ ������ �� ����
            }// ���� ���� ��, idle�� ����
        }

        if(gameTurn == GameTurn.PlayerTurn_EndMoving) // �÷��̾ �ٽ� ���ư� ��
        {
            PlayerObject.transform.localScale = new Vector3(-1, 1, 1);
            float playerScale_Circle = PlayerObject.transform.GetChild(1).localScale.y;
            PlayerObject.transform.GetChild(1).localScale = new Vector3(-playerScale_Circle, playerScale_Circle, playerScale_Circle);
            if (playerAni.AnimationName != "Run")
            {
                playerAni.state.SetAnimation(0, "Run", true);
                Sound_SFX.PlayerWalk_SFX(0);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - playerPosition.x >= 0)
            {
                PlayerObject.GetComponent<Transform>().Translate(-10f * Time.deltaTime, 0, 0);
            }// ������ ��ġ���� ������
            else
            {
                PlayerObject.transform.localScale = new Vector3(1, 1, 1);
                PlayerObject.transform.GetChild(1).localScale = new Vector3(playerScale_Circle, playerScale_Circle, playerScale_Circle);

                if (playerAni.AnimationName != "Idle")
                {
                    playerAni.state.SetAnimation(0, "Idle", true);
                    Sound_SFX.PlayerWalk_SFX(1);
                }
                gameTurn = GameTurn.Waiting; // �������� �ϴ� ��ٸ�
            }// ���� ���� ��, idle�� ����
        }

        if (gameTurn == GameTurn.MonsterTurn_StartMoving) //���Ͱ� �����̱� ����
        {
            if (monsterAni.AnimationName != "Walk")
            {
                monsterAni.state.SetAnimation(0, "Walk", true);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -3f)
            {
                monster[MonsterCount].GetComponent<Transform>().Translate(-10f * Time.deltaTime, 0, 0);
            } // ������ġ���� �ɾ
            else
            {
                if (monsterAni.AnimationName != "Idle")
                {
                    monsterAni.state.SetAnimation(0, "Idle", true);
                }
                gameTurn = GameTurn.MonsterTurn_Attack; // ���� ���� �� ���� ����
            }// idle�� ����
        }

        if(gameTurn == GameTurn.MonsterTurn_EndMoving) // ���Ͱ� �ٽ� �ǵ��ư� ��, �÷��̾�� �����.
        {
            float monsterScale = monster[MonsterCount].transform.localScale.y;
            float monsterScale_Circle = monster[MonsterCount].transform.GetChild(0).localScale.y;
            monster[MonsterCount].transform.localScale = new Vector3(monsterScale, monsterScale, monsterScale);
            monster[MonsterCount].transform.GetChild(0).localScale = new Vector3(monsterScale_Circle, monsterScale_Circle, monsterScale_Circle);
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
                monster[MonsterCount].transform.localScale = new Vector3(-monsterScale, monsterScale, monsterScale);
                monster[MonsterCount].transform.GetChild(0).localScale = new Vector3(-monsterScale_Circle, monsterScale_Circle, monsterScale_Circle);
                if (monsterAni.AnimationName != "Idle")
                {
                    monsterAni.state.SetAnimation(0, "Idle", true);
                }

                gameTurn = GameTurn.Waiting; // �������� �ϴ� ��ٸ�
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

    public void ItemUse() // ������ ����� ��.
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i].item_name == ItemName)
            {
                item[i].Use_Item();
                Sound_SFX.playerBuff_SFX();
            }
        }
    }

    public void After_Useing_ItemToggle(Toggle toggle) // ������ ��� ������ ��� -> ���� ��ư�� ����Ǿ�����
    {
        if (toggle.isOn)
        {
            switch (toggle.name)
            {
                case "Item1":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    ItemChoice_Part[0].SetActive(false);
                    break;
                case "Item2":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    ItemChoice_Part[1].SetActive(false);
                    break;
                case "Item3":
                    toggle.isOn = false;
                    toggle.interactable = false;
                    ItemChoice_Part[2].SetActive(false);
                    break;
            }
        }
    }

    public void ChanceToggleOff() // �ֻ��� ������ ��ư�� �����Ǿ�������, �ѹ� �� ������ ��, ���.
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
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
        atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum;
        defSum = playerData.def +  GameObject.Find("DiceDirector").GetComponent<Dice>().defSum;
        //�ֻ��� �� ���� ��, �÷��̾ ���� �߰��� �� �ֵ��� ���� ����
        StartCoroutine(playerTurn()); // �÷��̾� �� ����
    }

    IEnumerator playerTurn()
    {
        if (ItemFlag == true)
        {
            if (ItemName == "DoubleAtk")
            {
                ItemUse();
                BuffParticlePlay(2);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum * 2;
                ItemCount++;
            }
            else if (ItemName == "DoubleDef")
            {
                ItemUse();
                BuffParticlePlay(3);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                defSum = playerData.def + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum * 2;
                ItemCount++;
            }
            else if (ItemName == "Heal")
            {
                ItemUse();
                BuffParticlePlay(0);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                playerData.hp += 1;
                ItemCount++;
            }
            ItemFlag = false; // ��� ������ �ٽ� false�� ���ư�
        }

        Player_Atk_Text.text = atksum.ToString();
        Player_Def_Text.text = defSum.ToString();

        //�� �������� �̵� ��, ������
        playerPosition = PlayerObject.GetComponent<Transform>().position;
        monsterPosition = monster[MonsterCount].GetComponent<Transform>().position;
        gameTurn = GameTurn.PlayerTurn_StartMoving;
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();

        yield return new WaitUntil(() => gameTurn == GameTurn.PlayerTurn_Attack);

        int AttakcRand = Random.Range(0, 2);

        if (atksum > monsterData.def) // ���� �������� ��
        {
            if (AttakcRand == 0)
            {
                Hit_Part[0].SetActive(true);
                Sound_SFX.playerAttack_SFX(0);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Attack1", false));
                Hit_Part[0].SetActive(false);
            }
            else if (AttakcRand == 1)
            {
                Hit_Part[1].SetActive(true);
                Sound_SFX.playerAttack_SFX(1);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Attack2", false));
                Hit_Part[1].SetActive(false);
            }
            monsterData.hp -= 1;
        }
        else if (atksum == monsterData.def) // ���� ���� �¾��� ��
        {
            Hit_Part[2].SetActive(true);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            monsterAni.state.SetAnimation(0, "Attack", false);
            Hurt_Image.SetActive(true);
            if (AttakcRand == 0)
            {
                Hit_Part[0].SetActive(true);
                Sound_SFX.playerAttack_SFX(0);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Attack1", false));
                Hit_Part[0].SetActive(false);
            }
            else if (AttakcRand == 1)
            {
                Hit_Part[1].SetActive(true);
                Sound_SFX.playerAttack_SFX(1);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Attack2", false));
                Hit_Part[1].SetActive(false);
            }
            Hit_Part[2].SetActive(false);
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else // ���� ������ ��
        {
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            yield return new WaitForSeconds(0.4f);
            Hit_Part[4].SetActive(true);
            playerAni.state.SetAnimation(0, "Defence", false).TimeScale = 1.2f;
            yield return new WaitForSeconds(0.8f);
            Hit_Part[4].SetActive(false);
        }

        if (monsterData.hp <= 0) {  // �÷��̾� �Ͽ��� ���Ͱ� ���� �׾��� �� -> �״� �ִϸ��̼�
            monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
            Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
            monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
        }

        gameTurn = GameTurn.PlayerTurn_EndMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.Waiting); // �������� ������ ������ ��ٸ�


        if (playerData.hp == 0)
        {
            StartCoroutine(playerDie());
            gameTurn = GameTurn.Waiting;
        }
        else if (monsterData.hp == 0)
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
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
        gameTurn = GameTurn.MonsterTurn_StartMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.MonsterTurn_Attack);

        if(defSum < monsterData.atk) // ��� ����
        {
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            Hit_Part[3].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            Hurt_Image.SetActive(true);
            playerAni.state.SetAnimation(0, "Hurt", false).TimeScale = 1.2f;
            yield return new WaitForSeconds(0.8f);
            playerData.hp -= 1;
        }
        else if(defSum == monsterData.atk) { // ���� ����
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.playerAttack_SFX(2);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            Hit_Part[2].SetActive(true);
            Hurt_Image.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            playerAni.state.SetAnimation(0, "Attack1", false).TimeScale = 2f;
            yield return new WaitForSeconds(0.8f);
            monsterData.hp -= 0.5f;
            playerData.hp -= 0.5f;
        }
        else // ��� ����
        {
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            yield return new WaitForSeconds(0.4f);
            Hit_Part[4].SetActive(true);
            playerAni.state.SetAnimation(0, "Defence", false).TimeScale = 1.2f;
            yield return new WaitForSeconds(0.8f);
            playerAni.state.SetAnimation(0, "Idle", true);
        }

        gameTurn = GameTurn.MonsterTurn_EndMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.Waiting);

        if (playerData.hp <= 0 || RoundNum == conditionsDefeat) // �й� ���� -> �÷��̾� ü���� 0�̰ų� 10������ ���
        {
            if(RoundNum == conditionsDefeat && playerData.hp >= 0 && monsterData.hp == 0) {
                monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
                Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
                monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
                StartCoroutine(monsterDieDelay());
            }
            else
            {
                StartCoroutine(playerDie());
                if (monsterData.hp == 0)
                {
                    monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
                    Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
                    monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
                }
            }
        }else if(monsterData.hp == 0) // ���� ü���� 0�ϰ��
        {
            monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
            Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
            monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
            StartCoroutine(monsterDieDelay());
        }
        else // ����Ͽ� ����
        {
            PlayDice_UI.SetActive(true);
            ItemCount = 0;
            ItemGroup_Button.interactable = true;
            Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
            Player_Def_Text.text = playerData.def.ToString();
        }
    }
    
    IEnumerator playerDie() // ���� �÷��̾ �׾��� ��쿡, ������ â�� �߰� ��.
    {
        playerAni.state.SetAnimation(0, "Death", false).TimeScale = 0.8f;
        Sound_SFX.PlayerDead_SFX();
        yield return new WaitForSeconds(3f);
        Play_UI.SetActive(false);
        Lose_UI.SetActive(true);
    }

    IEnumerator monsterDieDelay() // ���Ͱ� �׾��� ��, ����ϰų� ����
    {
        yield return new WaitForSeconds(2f);
        MonsterCount++;
        gameTurn = GameTurn.BeforeFight;
        if (monster.Length == MonsterCount)
        {
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            if (SceneManager.GetActiveScene().name.Equals("Stage5"))
            {
                Win_UI_Button_Text.text = "���ǿ���";
            }
            Play_UI.SetActive(false);
            Win_UI.SetActive(true);
            Change_Reward(); // Ŭ��� ���� ������ �ٲ۴�.
            TotalWin(); // ���� ���
        }
        else //�ִϸ��̼� �ְ� �;�����, �����ϴ� �κ��� �ٸ�
        {
            monsterData = monster[MonsterCount].GetComponent<MonsterData>();
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
            Player_Def_Text.text = playerData.def.ToString();
            Play_Button.SetActive(true);
            ItemCount = 0;
            ItemGroup_Button.interactable = true;
        }
    }

    public void OnAD_Try() // ������ ��ư Ŭ������ ���
    {
        StartCoroutine(OnAD_Ani());
    }

    IEnumerator OnAD_Ani() // ���� �� �� ��, �÷��̾� ��Ȱ
    {
        playerData.hp++;
        Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
        Player_Def_Text.text = playerData.def.ToString();
        if (RoundNum == conditionsDefeat)
        {
            RoundNum--;
            RoundText.text = RoundNum + " ����";
        }
        Time.timeScale = 1;
        PlayDice_UI.SetActive(true);
        Play_UI.SetActive(true);
        Lose_UI.transform.GetChild(1).GetComponent<Transform>().gameObject.SetActive(false);
        Lose_UI.SetActive(false);
        BuffParticlePlay(1);
        if (monsterData.hp == 0)
        {
            PlayDice_UI.SetActive(false);
            StartCoroutine(monsterDieDelay());
        }
        Sound_SFX.playerBuff_SFX();
        yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
        playerAni.state.SetAnimation(0, "Idle", true);
    }

    void Change_Reward() // ����Ŭ��� ���� �޶���.
    {
        reward_Coin.text = Data.stage_Data[stage.curretStageNum - 1].reward_coin.ToString() + "G";

        if (stage.curretStageNum == stage.stageNum)
        {
            if (Data.stage_Data[stage.curretStageNum - 1].reward_point == 3)
            {
                reward_Image[0].sprite = Resources.Load<Sprite>("Reward/icon_stat3");
            }
            else if (Data.stage_Data[stage.curretStageNum - 1].reward_point == 4)
            {
                reward_Image[0].sprite = Resources.Load<Sprite>("Reward/icon_stat4");
            }

            if (Data.stage_Data[stage.curretStageNum - 1].reward_hp == 0)
            {
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
            }
            else
            {
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/icon_hp");
            }
        }
        else
        {
            reward_Image[0].sprite = Resources.Load<Sprite>("Reward/default");
            reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
        }
    }

    public void TotalWin() // ���տ��
    {
        stage.WinStage_Stage();
        playerData.RewardCoin_Player(Data.stage_Data[stage.curretStageNum-1].reward_coin);
        if (stage.curretStageNum == stage.stageNum)
        {
            playerData.RewardStatus_Player(Data.stage_Data[stage.curretStageNum - 1].reward_point);
            playerData.RewardHp_Player(Data.stage_Data[stage.curretStageNum - 1].reward_hp);
        }
    }

    public void OnClearMain() //���� ��� ����, �������� ���ư���
    {
        try
        {
            if (SceneManager.GetActiveScene().name.Equals("Stage5"))
            {
                Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/End_BGM");
                Sound_BGM.Play();
                gameManager.NextLevel("1-1.Toon");
            }
            else
            {
                Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Loby_BGM");
                Sound_BGM.Play();
                gameManager.NextLevel("1.StageChoice");
            }
        }
        catch
        {
            if (SceneManager.GetActiveScene().name.Equals("Stage5"))
            {
                SceneManager.LoadScene("1-1.Toon");
            }
            else
            {
                SceneManager.LoadScene("1.StageChoice");
            }
        }

        if (GameObject.Find("Sfx_Player").GetComponent<AudioSource>().isPlaying) // �ȴ� �κп��� Loop�� �����־ ������ ���� ���� -> �÷��̾� �ȴ� ȿ���� ���� ���� ��쿡 �������� ���ư��� ���� �߻�
        {
            Sound_SFX.PlayerWalk_SFX(1);
        }
    }

    public void OnMain() // �������� ���ư���
    {
        Time.timeScale = 1;
        try
        {
            Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Loby_BGM");
            Sound_BGM.Play();
            gameManager.NextLevel("1.StageChoice");
        }
        catch
        {
            SceneManager.LoadScene("1.StageChoice");
        }

        if (GameObject.Find("Sfx_Player").GetComponent<AudioSource>().isPlaying) // �ȴ� �κп��� Loop�� �����־ ������ ���� ���� -> �÷��̾� �ȴ� ȿ���� ���� ���� ��쿡 �������� ���ư��� ���� �߻�
        {
            Sound_SFX.PlayerWalk_SFX(1);
        }
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

    public void BuffParticlePlay(int num) // ���Z ��ƼŬ�� ���� �޶���
    {//0�� ��, 1�� ��Ȱ, 2�� atk, 3�� def
        if (num == 0)
        {
            Buff_Part[0].SetActive(true);
        }
        else if(num == 1)
        {
            Buff_Part[1].SetActive(true);
        }else if(num == 2)
        {
            Buff_Part[2].SetActive(true);
        }else if(num == 3)
        {
            Buff_Part[3].SetActive(true);
        }
    }
}

public enum GameTurn {  // ���� ��
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