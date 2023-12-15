using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTitle : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gameManager;
    public GameObject UI;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UI.activeSelf == false)
        {
            if (PlayerPrefs.GetInt("Guide_Count") == 0)
            {
                gameManager.NextLevle("1-0.Toon");
            }
            else
            {
                gameManager.NextLevle("1.StageChoice");
            }
        }
    }
}
