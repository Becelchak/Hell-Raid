using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsFunctions : MonoBehaviour
{
    public void RestartScene()
    { 
        SceneManager.LoadScene($"{SceneManager.GetActiveScene().name}");
    }

    public void CloseSoldierPickMenu()
    {
        var menu = GameObject.Find("PanelSoldiersPick");
        menu.SetActive(false);
    }

    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }
}
