using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    void Awake()
    {
/*        // Make the game run as fast as possible
        Application.targetFrameRate = -1;
        // Limit the framerate to 60
        Application.targetFrameRate = 60;*/
        DontDestroyOnLoad(gameObject);
    }

    public void NextLevle(string SceneName)
    {
        StartCoroutine(LoadLevel(SceneName));
    }

    IEnumerator LoadLevel(string SceneName)
    {
        transitionAnim.gameObject.SetActive(true);
        transitionAnim.SetTrigger("StartAni");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(SceneName);
        transitionAnim.SetTrigger("EndAni");
    }
}
