using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SceneTransition : MonoBehaviour
{
    public void ChangeScene(int index)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(index);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(YandexGame.savesData.level + 1);
    }
}
