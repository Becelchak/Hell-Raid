using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackBtn : MonoBehaviour
{
    [SerializeField]
    private List<Toggle> toggles;

    public void SetToggle()
    {
        foreach (var toggle in toggles)
        {
            toggle.interactable = true;
            if (toggle.isOn)
                toggle.isOn = false;
        }
    }
}
