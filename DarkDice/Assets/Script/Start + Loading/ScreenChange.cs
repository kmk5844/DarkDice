using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenChange : MonoBehaviour
{
    public Animator tranition;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        tranition.SetTrigger("");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }

}
