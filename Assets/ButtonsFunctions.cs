using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour
{
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseSoldierPickMenu()
    {
        var menu = GameObject.Find("PanelSoldiersPick");
        menu.SetActive(false);
    }
}
