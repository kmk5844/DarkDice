using Spine.Unity;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    InGame_Sound SFX_Sound;

    public GameObject Player;
    public Transform monsterGroup;
    [SerializeField]
    private GameObject[] monster;
    public GameObject BackGround;

    public GameObject GameDirector;

    public GameObject Play_UI;
    int monsterGroup_childCount;
    int Monster_DieCount;

    // Start is called before the first frame update
    void Start()
    {
        SFX_Sound = GetComponent<InGame_Sound>();

        Monster_DieCount = 0;
        monsterGroup_childCount = monsterGroup.childCount;
        monster = new GameObject[monsterGroup_childCount];
        for (int i = 0; i < monsterGroup_childCount; i++)
        {
            monster[i] = monsterGroup.GetChild(i).gameObject;
        }
    }

    private void LateUpdate()
    {
        if (GameDirector.GetComponent<GameDirector>().gameTurn == GameTurn.BeforeFight) // ���� ���� ��
        {
            if (Monster_DieCount < monster.Length)
            {
                if (Player.transform.position.x - monster[Monster_DieCount].transform.position.x <= -11f)// ���� ������
                {
                    monster[Monster_DieCount].SetActive(true);
                    Play_UI.SetActive(false);
                    if (monster[Monster_DieCount].GetComponent<SkeletonAnimation>().AnimationName != "Walk")
                    {
                        monster[Monster_DieCount].GetComponent<SkeletonAnimation>().state.SetAnimation(0, "Walk", true);
                    }

                    if (Player.GetComponentInChildren<SkeletonAnimation>().AnimationName != "Run DUELIST")
                    {
                        Player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "Run DUELIST", true);
                        SFX_Sound.PlayerWalk_SFX(0);
                    }
                    BackGround.transform.Translate(10 * Time.deltaTime, 0 , 0);
                    monster[Monster_DieCount].transform.Translate(-10.0f * Time.deltaTime, 0, 0);
                    if (Monster_DieCount > 0)
                    {
                        monster[Monster_DieCount - 1].transform.Translate(-12.0f * Time.deltaTime, 0, 0);
                    }
                }
                else // ���� ���� ��,
                {
                    if (monster[Monster_DieCount].GetComponent<SkeletonAnimation>().AnimationName != "Idle")
                    {
                        monster[Monster_DieCount].GetComponent<SkeletonAnimation>().state.SetAnimation(0, "Idle", true);
                    }

                    if (Player.GetComponentInChildren<SkeletonAnimation>().AnimationName != "Idle")
                    {
                        SFX_Sound.PlayerWalk_SFX(1);
                        Player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "Idle", true);
                    }

                    if (Monster_DieCount > 0)
                    {
                        monster[Monster_DieCount - 1].SetActive(false);
                    }
                    Play_UI.SetActive(true);
                    GameDirector.GetComponent<GameDirector>().gameTurn = GameTurn.Fighting; // �ο� �غ������� ����
                }
            }
            else if (Monster_DieCount == monster.Length) // ���� ���Ͱ� �� �׾��ٸ�
            {
                if (Player.GetComponentInChildren<SkeletonAnimation>().AnimationName != "Run DUELIST")
                {
                    SFX_Sound.PlayerWalk_SFX(0);
                    Player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "Run DUELIST", true);
                }

                if (monster[Monster_DieCount - 1].transform.position.x - Player.transform.position.x >= -11f)
                {
                    Player.transform.Translate(10.0f * Time.deltaTime, 0, 0);
                }
                else
                {
                    Player.SetActive(false);
                    SFX_Sound.PlayerWalk_SFX(1);
                } // �÷��̾ �������� �ִϸ��̼�
            }
        }
    }

    public void monsterDie()
    {
        Monster_DieCount++;
    }
}
