using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ani_Loading_Off : MonoBehaviour
{
    public void LoadingWindowOff()
    {
        this.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name.Equals("1.StageChoice"))
        {
            GameObject.Find("TutorialDirector").GetComponent<TutorialDirector>().Check_Guide();
        }
    }
}
