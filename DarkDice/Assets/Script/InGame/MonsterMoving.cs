using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    public GameObject Player;
    public Transform monsterGroup;
    [SerializeField]
    private GameObject[] monster;

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
        if (Monster_DieCount < monster.Length)
        {
            if (Player.transform.position.x - monster[Monster_DieCount].transform.position.x <= -11.0f)
            {
                Play_UI.SetActive(false);
                monster[Monster_DieCount].transform.Translate(-10.0f * Time.deltaTime, 0, 0);
                if (Monster_DieCount > 0)
                {
                    monster[Monster_DieCount - 1].transform.Translate(-15.0f * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                Play_UI.SetActive(true);
            }
        }
        else if (Monster_DieCount == monster.Length)
        {
            if (monster[Monster_DieCount - 1].transform.position.x - Player.transform.position.x >= -11.0f)
            {
                Player.transform.Translate(10.0f * Time.deltaTime, 0, 0);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void monsterDie()
    {
        Monster_DieCount++;
    }
}
