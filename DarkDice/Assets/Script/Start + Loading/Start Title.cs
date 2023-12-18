using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTitle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    GameManager gameManager;
    AudioSource Sound_BGM;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Sound_BGM = GameObject.Find("Bgm").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UI.activeSelf == false)
        {
            Sound_BGM.clip = Resources.Load<AudioClip>("Sound/BGM/Loby_BGM");
            Sound_BGM.Play();
            if (PlayerPrefs.GetInt("Guide_Count") == 0)
            {
                gameManager.NextLevel("1-0.Toon");
            }
            else
            {
                gameManager.NextLevel("1.StageChoice");
            }
        }
    }
}
