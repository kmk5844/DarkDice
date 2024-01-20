using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Spine.Unity;

public class GameDirector : MonoBehaviour
{
    public GameTurn gameTurn;
    Player_Scritable playerData; // 플레이어 정보 데이터를 불러온다.
    MonsterData monsterData;

    public GameObject[] ItemObject_Data; // 아이템 정보 데이터를 불러온다.
    Item_Scritable[] item; // 1번_atk2배 2번_힐 3번_다시던지기 4번_def2배 5번_쇠약 6번_부활

    public GameObject stage_Data; // 스테이지 정보 데이터를 불러온다(최초 클리어와 관련)
    Stage_Scripter stage;
    public DataTable Data; // 몬스터 정보 및 스테이지 정보를 가져오는 데이터 테이블
    GameManager gameManager;
    public GameObject Dice_Director; //주사위 디렉터 가져옴
    public GameObject Monster_Director; // 몬스터 디렉터 가져옴

    [SerializeField]
    private string ItemName; //사용하려는 아이템
    public bool ItemFlag; //플레이 당시의 아이템을 사용할건지 말건지
    public Toggle[] Item_Toggle;
    public GameObject[] Item_BackGround;

    int RoundNum; //현재 라운드
    int DiceNum; //주사위를 던진 횟수 -> 찬스 아이템 사용할 경우에 이용함
    int player_atksum; // 플레이어의 공격력
    int player_defSum; // 플레이어의 방어력
    int monster_atk; // 몬스터의 공격력
    int monster_def; // 몬스터의 방어력
    int MonsterCount; // 해당 몬스터의 수가 딱 맞으면, 종료하는 카운트
    int ItemCount; // 아이템 횟수 -> 찬스 아이템을 굴린 후, 또 다른 아이템 허용하지 않도록 함
    int conditionsDefeat; //패배 조건

    SkeletonAnimation playerAni; //플레이어 애니메이션
    SkeletonAnimation monsterAni; // 몬스터 애니메이션
    Vector3 playerPosition; // 플레이어 포지션
    Vector3 monsterPosition; // 몬스터 포지션

    public TextMeshProUGUI RoundText;
    public TextMeshProUGUI Player_Atk_Text;
    public TextMeshProUGUI Player_Def_Text;
    public TextMeshProUGUI Monster_Atk_Text;
    public TextMeshProUGUI Monster_Def_Text;

    public GameObject Play_Button; //전투 개시 버튼
    public GameObject PlayDice_UI;//주사위 굴릴 때 나오는 UI
    public GameObject Play_UI; // 전체적인 플레이 UI
    public GameObject Win_UI;  // 이겼을 때의 UI
    public TextMeshProUGUI Win_UI_Button_Text; // 스테이지 5인 경우에, 텍스트 변경
    public GameObject Lose_UI; // 졌을 때의 UI
    public Button Revival_Button; // 부활 할 때의 버튼
    public GameObject PlayerObject; //플레이어 오브젝트
    public GameObject Hurt_Image; //맞았을 때의 UI

    public Transform mosterGroup; //몬스터 그룹 오브젝트
    int mosterChildCount; // 몬스터 그룹에 있는 몬스터의 횟수
    GameObject[] monster;

    public Button AtkButton;
    public Button DiceButton;

    public TextMeshProUGUI reward_Coin; // 보상 코인 텍스트 변경
    public TextMeshProUGUI reward_Point; // 보상 코인 텍스트 변경
    public Image[] reward_Image; // 보상 이미지 변경

    public GameObject Item_Group; // 아이템 그룹에 있는 애니메이션을 구하기 위함
    Animator Ani_ItemGroup;

    //파티클 부분-----------------------------------------------------------------------------------
    public GameObject[] Hit_Part; //0, 1번 플레이어 히트, 2번 동시 히트, 3번 몬스터 히트 4번 디펜스
    public GameObject[] Buff_Part; //0번 힐, 1번 부활, 2번 atk, 3번 def
    public GameObject Heal_Part;
    public GameObject Revival_Part;
    public GameObject[] ItemChoice_Part;

    //히트 이펙트 부분------------------------------------------------------------------------------
    public GameObject[] Hit_Text_Effect; // 0번 Deal 1번 Miss 2번 Fail 3번 Def

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

        playerAni = PlayerObject.GetComponentInChildren<SkeletonAnimation>();
        mosterChildCount = mosterGroup.childCount;
        monster = new GameObject[mosterChildCount];
        ItemFlag = false;

        Ani_ItemGroup = Item_Group.GetComponent<Animator>();

        gameTurn = GameTurn.BeforeFight;
        try
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        catch
        {
            Debug.Log("게임 매니저가 없을 뿐이야~");
        }

        for (int i = 0; i < mosterChildCount; i++)
        {
            monster[i] = mosterGroup.GetChild(i).gameObject; //몬스터 그룹에 있는 몬스터를 저장을 한다.
        }

        RoundText.text = RoundNum + " 라운드";
        item = new Item_Scritable[ItemObject_Data.Length];
        playerData = PlayerObject.GetComponent<Player_Scritable>(); // 플레이어 데이터를 불러온다.
        monsterData = monster[MonsterCount].GetComponent<MonsterData>(); // 몬스터 데이터를 불러온다.
        stage = stage_Data.GetComponent<Stage_Scripter>(); // 스테이지 데이터를 불러온다.
        if (stage.curretStageNum >= 11)
        {
            conditionsDefeat = 15;
        }
        else
        {
            conditionsDefeat = 10;
        }

        //스테이지마다 배경 변경
        if (stage.curretStageNum == 1 || stage.curretStageNum == 2)
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

        //몬스터 수마다 배경 이동 / 3마리인 경우에 배경 길이 늘어남
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
            item[i] = ItemObject_Data[i].GetComponent<Item_Scritable>(); // 아이템 데이터를 불러온다.
        }

        if (item[5].itemcount == 0)
        {
            Revival_Button.interactable = false;
        }
        else
        {
            Revival_Button.interactable = true;
        }

        for (int i = 0; i < Item_Toggle.Length; i++)
        {
            if (playerData.item[i].ItemName == "Default")
            {
                Item_Toggle[i].interactable = false; // 만약 아이템 정보가 기본이라면 잠근다.
            }
            Item_BackGround[i].GetComponent<Image>().sprite = playerData.item[i].ItemImage; //백그라운드와 이미지를 설정한다.
            Item_BackGround[i].GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = playerData.item[i].ItemImage;
        }

        Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
        Player_Def_Text.text = (playerData.def + playerData.d_weapon.WeaponDef).ToString();
        monster_atk = monsterData.atk;
        monster_def = monsterData.def;
        Monster_Atk_Text.text = monster_atk.ToString();
        Monster_Def_Text.text = monster_def.ToString();
    }
    void LateUpdate()
    {
        if(Time.timeScale == 0)
        {
            return; // 타임스케일이 0이라면 멈춘다.
        }

        Monster_Atk_Text.text = monster_atk.ToString();
        Monster_Def_Text.text = monster_def.ToString();

        if(gameTurn == GameTurn.Fighting || gameTurn == GameTurn.Waiting)
        {
            if (ItemCount == 0)
            {
                Ani_ItemGroup.SetBool("ItemOpenCloseFlag", true);
            }
            else if (ItemCount == 1) // 아이템 사용 했을 경우, 닫는다.
            {
                Ani_ItemGroup.SetBool("ItemOpenCloseFlag", false);
            }
        }
        else
        {
            Ani_ItemGroup.SetBool("ItemOpenCloseFlag", false);
        }
        

        if (playerData.hp <= 0) // -0.5 상황이 나오지 않도록 조절
        {
            playerData.hp = 0;
        }

        if(monsterData.hp <= 0)
        {
            monsterData.hp = 0;
        }

        if (RoundNum >= conditionsDefeat - 3)
        {
            RoundText.color = Color.red; // 7라운드 이상이 되면 색상 변경
        }
        
        if (Item_Toggle[0].isOn) //아이템을 클릭 했을 때, 사용할 아이템 저장 및 파티클 구현
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

        if (gameTurn == GameTurn.PlayerTurn_StartMoving) // 플레이어가 움직이기 시작
        {
            if (playerAni.AnimationName != "Run") 
            {
                playerAni.state.SetAnimation(0, "Run", true);
                Sound_SFX.PlayerWalk_SFX(0);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -3f)
            {
                PlayerObject.GetComponent<Transform>().Translate(10f * Time.deltaTime, 0, 0);
            } // 지정된 위치까지 움직임
            else
            {
                if (playerAni.AnimationName != "Idle")
                {
                    playerAni.state.SetAnimation(0, "Idle", true);
                    Sound_SFX.PlayerWalk_SFX(1);
                }
                gameTurn = GameTurn.PlayerTurn_Attack; // 게임턴이 플레이어가 공격할 때 변경
            }// 도착 했을 때, idle로 변경
        }

        if(gameTurn == GameTurn.PlayerTurn_EndMoving) // 플레이어가 다시 돌아갈 때
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
            }// 지정된 위치까지 움직임
            else
            {
                PlayerObject.transform.localScale = new Vector3(1, 1, 1);
                PlayerObject.transform.GetChild(1).localScale = new Vector3(playerScale_Circle, playerScale_Circle, playerScale_Circle);

                if (playerAni.AnimationName != "Idle")
                {
                    playerAni.state.SetAnimation(0, "Idle", true);
                    Sound_SFX.PlayerWalk_SFX(1);
                }
                gameTurn = GameTurn.Waiting; // 게임턴이 일단 기다림
            }// 도착 했을 때, idle로 변경
        }

        if (gameTurn == GameTurn.MonsterTurn_StartMoving) //몬스터가 움직이기 시작
        {
            if (monsterAni.AnimationName != "Walk")
            {
                monsterAni.state.SetAnimation(0, "Walk", true);
            }

            if (PlayerObject.GetComponent<Transform>().position.x - monster[MonsterCount].GetComponent<Transform>().position.x <= -3f)
            {
                monster[MonsterCount].GetComponent<Transform>().Translate(-10f * Time.deltaTime, 0, 0);
            } // 지정위치까지 걸어감
            else
            {
                if (monsterAni.AnimationName != "Idle")
                {
                    monsterAni.state.SetAnimation(0, "Idle", true);
                }
                gameTurn = GameTurn.MonsterTurn_Attack; // 몬스터 공격 할 때로 변경
            }// idle로 변경
        }

        if(gameTurn == GameTurn.MonsterTurn_EndMoving) // 몬스터가 다시 되돌아갈 때, 플레이어와 비슷함.
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

                gameTurn = GameTurn.Waiting; // 게임턴을 일단 기다림
            }
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

    public void ItemUse() // 아이템 사용할 때.
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

    public void After_Useing_ItemToggle(Toggle toggle) // 아이템 사용 이후의 토글 -> 공격 버튼에 저장되어있음
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

    public void ChanceToggleOff() // 주사위 굴리기 버튼에 지정되어있으며, 한번 더 돌렸을 때, 잠금.
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
        RoundText.text = RoundNum + " 라운드";
        DiceNum = 0;
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
        player_atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum;
        player_defSum = playerData.def + playerData.d_weapon.WeaponDef + GameObject.Find("DiceDirector").GetComponent<Dice>().defSum;
        //주사위 다 굴린 후, 플레이어에 스텟 추가할 수 있도록 변수 저장
        StartCoroutine(playerTurn()); // 플레이어 턴 시작
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
                player_atksum = playerData.atk + playerData.weapon.WeaponAtk + GameObject.Find("DiceDirector").GetComponent<Dice>().atkSum * 2;
                ItemCount++;
            }
            else if (ItemName == "DoubleDef")
            {
                ItemUse();
                BuffParticlePlay(3);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                player_defSum = playerData.def + playerData.d_weapon.WeaponDef +GameObject.Find("DiceDirector").GetComponent<Dice>().defSum * 2;
                ItemCount++;
            }
            else if (ItemName == "Heal")
            {
                ItemUse();
                BuffParticlePlay(0);
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                playerData.hp += 0.5f;
                ItemCount++;
            }else if(ItemName == "Weak") // 쇠약
            {
                ItemUse();
                //파티클 추가
                yield return new WaitForSpineAnimationComplete(playerAni.state.SetAnimation(0, "Buff", false));
                monster_atk -= 2;
                monster_def -= 2;
                ItemCount++;
            }
            ItemFlag = false; // 사용 했으니 다시 false로 돌아감
        }

        Player_Atk_Text.text = player_atksum.ToString();
        Player_Def_Text.text = player_defSum.ToString();

        //이 구간에서 이동 후, 때리기
        playerPosition = PlayerObject.GetComponent<Transform>().position;
        monsterPosition = monster[MonsterCount].GetComponent<Transform>().position;
        gameTurn = GameTurn.PlayerTurn_StartMoving;
        monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();

        yield return new WaitUntil(() => gameTurn == GameTurn.PlayerTurn_Attack);

        int AttakcRand = Random.Range(0, 2);

        if (player_atksum > monster_def) // 공격 성공했을 때
        {
            Hit_Text_Effect[0].SetActive(true);
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
        else if (player_atksum == monster_def) // 공격 서로 맞았을 때
        {
            Hit_Text_Effect[1].SetActive(true);
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
        else // 공격 실패할 때
        {
            Hit_Text_Effect[2].SetActive(true);
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            yield return new WaitForSeconds(0.4f);
            Hit_Part[4].SetActive(true);
            playerAni.state.SetAnimation(0, "Defence", false).TimeScale = 1.2f;
            yield return new WaitForSeconds(0.8f);
            Hit_Part[4].SetActive(false);
        }

        if (monsterData.hp <= 0) {  // 플레이어 턴에서 몬스터가 먼저 죽었을 때 -> 죽는 애니메이션
            monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
            Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
            monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
        }

        gameTurn = GameTurn.PlayerTurn_EndMoving;
        yield return new WaitUntil(() => gameTurn == GameTurn.Waiting); // 게임턴이 변경할 때까지 기다림


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

        if(player_defSum < monster_atk) // 방어 실패
        {
            Hit_Text_Effect[2].SetActive(true);
            monsterAni.state.SetAnimation(0, "Attack", false);
            Sound_SFX.MonsterAttack_SFX(monster[MonsterCount].name);
            Hit_Part[3].SetActive(true);
            yield return new WaitForSeconds(0.4f);
            Hurt_Image.SetActive(true);
            playerAni.state.SetAnimation(0, "Hurt", false).TimeScale = 1.2f;
            yield return new WaitForSeconds(0.8f);
            playerData.hp -= 1;
        }
        else if(player_defSum == monster_atk) { // 서로 맞음
            Hit_Text_Effect[1].SetActive(true);
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
        else // 방어 성공
        {
            Hit_Text_Effect[3].SetActive(true);
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

        if (playerData.hp <= 0 || RoundNum == conditionsDefeat) // 패배 조건 -> 플레이어 체력이 0이거나 10라운드일 경우
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
        }else if(monsterData.hp == 0) // 몬스터 체력이 0일경우
        {
            monsterAni = monster[MonsterCount].GetComponent<SkeletonAnimation>();
            Sound_SFX.MonsterDead_SFX(monster[MonsterCount].name);
            monsterAni.state.SetAnimation(0, "Dead", false).TimeScale = 2f;
            StartCoroutine(monsterDieDelay());
        }
        else // 계속하여 속행
        {
            PlayDice_UI.SetActive(true);
            ItemCount = 0;
            Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
            Player_Def_Text.text = (playerData.def + playerData.d_weapon.WeaponDef).ToString();
            monster_atk = monsterData.atk;
            monster_def = monsterData.def;
        }
    }
    
    IEnumerator playerDie() // 만약 플레이어가 죽었을 경우에, 윈도우 창을 뜨게 함.
    {
        playerAni.state.SetAnimation(0, "Death", false).TimeScale = 0.8f;
        Sound_SFX.PlayerDead_SFX();
        yield return new WaitForSeconds(3f);
        Play_UI.SetActive(false);
        Lose_UI.SetActive(true);
    }

    IEnumerator monsterDieDelay() // 몬스터가 죽었을 때, 계속하거나 끝냄
    {
        yield return new WaitForSeconds(2f);
        MonsterCount++;
        gameTurn = GameTurn.BeforeFight;
        if (monster.Length == MonsterCount)
        {
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            Play_UI.SetActive(false);
            Win_UI.SetActive(true);
            Change_Reward(); // 클리어에 따라 보상을 바꾼다.
            TotalWin(); // 종합 우승
        }
        else //애니메이션 넣고 싶었으나, 구현하는 부분이 다름
        {
            monsterData = monster[MonsterCount].GetComponent<MonsterData>();
            Monster_Director.GetComponent<MonsterMoving>().monsterDie();
            Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
            Player_Def_Text.text = (playerData.def + playerData.d_weapon.WeaponDef).ToString();
            monster_atk = monsterData.atk;
            monster_def = monsterData.def;
            Play_Button.SetActive(true);
            ItemCount = 0;
        }
    }

    public void OnRevival_Try() // 부활 버튼 클릭했을 경우
    {
        StartCoroutine(Revival_Director());
    }

    IEnumerator Revival_Director() //플레이어 부활
    {
        playerData.hp++;
        Player_Atk_Text.text = (playerData.atk + playerData.weapon.WeaponAtk).ToString();
        Player_Def_Text.text = (playerData.def + playerData.d_weapon.WeaponDef).ToString();
        if (RoundNum == conditionsDefeat)
        {
            RoundNum--;
            RoundText.text = RoundNum + " 라운드";
        }
        Time.timeScale = 1;
        item[5].Use_Item(); // 부활권 아이템 사용
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
        ItemCount = 0;
    }

    void Change_Reward() // 최초클리어에 따라 달라짐.
    {
        reward_Coin.text = Data.stage_Data[stage.curretStageNum - 1].reward_coin.ToString() + "G";

        if (stage.curretStageNum == stage.final_stageNum)
        {
            reward_Image[0].sprite = Resources.Load<Sprite>("Reward/statuspt_icon");
            reward_Point.text = Data.stage_Data[stage.curretStageNum - 1].reward_point + "Point";

            if (Data.stage_Data[stage.curretStageNum - 1].reward_hp == 0)
            {
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
            }
            else
            {
                reward_Image[1].sprite = Resources.Load<Sprite>("Reward/hp_icon");
            }
        }
        else
        {
            reward_Point.text = "";
            reward_Image[0].sprite = Resources.Load<Sprite>("Reward/default");
            reward_Image[1].sprite = Resources.Load<Sprite>("Reward/default");
        }
    }

    public void TotalWin() // 종합우승
    {
        stage.WinStage_Stage();
        playerData.RewardCoin_Player(Data.stage_Data[stage.curretStageNum-1].reward_coin);
        if (stage.curretStageNum == stage.final_stageNum)
        {
            playerData.RewardStatus_Player(Data.stage_Data[stage.curretStageNum - 1].reward_point);
            playerData.RewardHp_Player(Data.stage_Data[stage.curretStageNum - 1].reward_hp);
        }
    }

    public void OnClearMain() //종합 우승 이후, 메인으로 돌아가기
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

        if (GameObject.Find("Sfx_Player").GetComponent<AudioSource>().isPlaying) // 걷는 부분에서 Loop가 켜져있어서 강제로 끄게 만듦 -> 플레이어 걷는 효과음 나고 있을 경우에 메인으로 돌아가면 버그 발생
        {
            Sound_SFX.PlayerWalk_SFX(1);
        }
    }

    public void OnMain() // 메인으로 돌아가기
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

        if (GameObject.Find("Sfx_Player").GetComponent<AudioSource>().isPlaying) // 걷는 부분에서 Loop가 켜져있어서 강제로 끄게 만듦 -> 플레이어 걷는 효과음 나고 있을 경우에 메인으로 돌아가면 버그 발생
        {
            Sound_SFX.PlayerWalk_SFX(1);
        }
    }

    public void BuffParticlePlay(int num) // 버프 파티클에 따라 달라짐
    {//0번 힐, 1번 부활, 2번 atk, 3번 def, 4번 쇠약
        Buff_Part[num].SetActive(true);
    }
}

public enum GameTurn {  // 게임 턴
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