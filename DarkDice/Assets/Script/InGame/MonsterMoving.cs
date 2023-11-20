using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoving : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Monster;
    public GameObject Play_UI;
    int Monster_Count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Monster_Count < Monster.Length)
        {
            if (Player.transform.position.x - Monster[Monster_Count].transform.position.x <= -11.0f)
            {
                Play_UI.SetActive(false);
                Monster[Monster_Count].transform.Translate(-10.0f * Time.deltaTime, 0, 0);
                if(Monster_Count > 0)
                {
                    Monster[Monster_Count - 1].transform.Translate(-10.0f * Time.deltaTime, 0, 0);
                }
            }
            else
            {
                Play_UI.SetActive(true);
            }
        }
        else if(Monster_Count == Monster.Length)
        {
            if (Monster[Monster_Count - 1].transform.position.x - Player.transform.position.x >= -11.0f)
            {
                Player.transform.Translate(10.0f * Time.deltaTime, 0, 0);
            }
        }
    }

    public void monsterDie()
    {
        Monster_Count++;
    }
}
