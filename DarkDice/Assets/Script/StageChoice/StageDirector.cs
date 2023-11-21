using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageDirector : MonoBehaviour { 

    public TextMeshProUGUI stageName;

    public GameObject stageBar;
    public Image[] item_Image;
    public Image[] reward_Image;

    public GameObject playerObject;
    Player_Scritable player;


    void Start()
    {
        player = playerObject.GetComponent<Player_Scritable>();

        for(int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }
    }

    void Update()
    {
        for (int i = 0; i < item_Image.Length; i++)
        {
            item_Image[i].sprite = player.item[i].ItemImage;
        }
    }


    public void OnClickStage(int stageNum)
    {
        string Sub_StageTitle = "";
        switch (stageNum){
            case 1:
                Sub_StageTitle = "성을 향해";
                break;
            case 2 :
                Sub_StageTitle = "어디론가를 향해";
                break;
        }
        stageName.text = "STAGE" + stageNum + " : " + Sub_StageTitle;
        stageBar.SetActive(true);
    }
}
