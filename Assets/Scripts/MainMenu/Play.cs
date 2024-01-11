using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Play : MonoBehaviour
{
    private bool isNewGame;

    [SerializeField]
    private GameObject continueGameWindow;

    [SerializeField]
    private SceneTransition SceneManager;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void GetLoad()
    {
        if (YandexGame.savesData.level == 1)
            isNewGame = true;
    }

    public void ChooseAction()
    {
        if (!isNewGame)
            continueGameWindow.SetActive(true);
        else
            SceneManager.ChangeScene(1);
    }
}
