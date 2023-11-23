using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Monster_Instance : MonoBehaviour
{
    public Transform monsterGroup;
    void Awake()
    {
/*        switch (SceneManager.GetActiveScene().name)
        {
            case "Test_Stage1":
                var obj = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/soldier1.prefab", typeof(GameObject)), monsterGroup);
                obj.name = "soldier1";
                break;
            case "Test_Stage2":
                obj = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/elite1.prefab", typeof(GameObject)), monsterGroup);
                obj.name = "elite1";
                break;
            case "Test_Stage3":
                obj = Instantiate((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefab/boss1.prefab", typeof(GameObject)), monsterGroup);
                obj.name = "boss1";
                break;
        }*/

    }
}
