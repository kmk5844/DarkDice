using Spine.Unity;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    public GameObject Player;
    public Transform monsterGroup;
    [SerializeField]
    private GameObject[] monster;

    public GameObject GameDirector;

    public GameObject Play_UI;
    int monsterGroup_childCount;
    int Monster_DieCount;

    // Start is called before the first frame update
    void Start()
    {
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
        if (GameDirector.GetComponent<GameDirector>().gameTurn == GameTurn.BeforeFight) // 몬스터 조우 전
        {
            if (Monster_DieCount < monster.Length)
            {
                if (Player.transform.position.x - monster[Monster_DieCount].transform.position.x <= -11.0f)
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
                    }

                    monster[Monster_DieCount].transform.Translate(-10.0f * Time.deltaTime, 0, 0);
                    if (Monster_DieCount > 0)
                    {
                        monster[Monster_DieCount - 1].transform.Translate(-12.0f * Time.deltaTime, 0, 0);
                    }
                }
                else // 몬스터 조우 후,
                {
                    if (monster[Monster_DieCount].GetComponent<SkeletonAnimation>().AnimationName != "Idle")
                    {
                        monster[Monster_DieCount].GetComponent<SkeletonAnimation>().state.SetAnimation(0, "Idle", true);
                    }

                    if (Player.GetComponentInChildren<SkeletonAnimation>().AnimationName != "Idle")
                    {
                        Player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "Idle", true);
                    }

                    if (Monster_DieCount > 0)
                    {
                        monster[Monster_DieCount - 1].SetActive(false);
                    }
                    Play_UI.SetActive(true);
                    GameDirector.GetComponent<GameDirector>().gameTurn = GameTurn.Fighting; // 싸움 준비턴으로 변경
                }
            }
            else if (Monster_DieCount == monster.Length) // 만약 몬스터가 다 죽었다면
            {
                if (Player.GetComponentInChildren<SkeletonAnimation>().AnimationName != "Run DUELIST")
                {
                    Player.GetComponentInChildren<SkeletonAnimation>().state.SetAnimation(0, "Run DUELIST", true);
                }

                if (monster[Monster_DieCount - 1].transform.position.x - Player.transform.position.x >= -11.0f)
                {
                    Player.transform.Translate(10.0f * Time.deltaTime, 0, 0);
                } // 플레이어가 지나가는 애니메이션
            }
        }
    }

    public void monsterDie()
    {
        Monster_DieCount++;
    }
}
