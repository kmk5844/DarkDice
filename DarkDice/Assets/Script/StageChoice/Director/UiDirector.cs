using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDirector : MonoBehaviour
{
    public GameObject Store_UI;
    public GameObject Status_UI;
    public GameObject Item_UI;
    public GameObject Stage_UI;
    public GameObject Panel_UI; //stage Panel

    void Update()
    {
        if (Store_UI.activeSelf || Status_UI.activeSelf)
        {
            Panel_UI.SetActive(true);
            
        }
        else if (!Store_UI.activeSelf || !Status_UI.activeSelf)
        {
            Panel_UI.SetActive(false);
        }
    }

    public void On_StatusAndStore_UIClcik() {
        if (Item_UI.activeSelf)
        {
            Item_UI.SetActive(false);
        }

        if(Stage_UI.activeSelf)
        { 
            Stage_UI.SetActive(false); 
            
        }
    }

}
