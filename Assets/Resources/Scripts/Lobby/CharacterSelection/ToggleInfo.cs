using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInfo : ToggleDataTransition
{
    [SerializeField]
    private GameObject soldierPrefab;

    [SerializeField]
    private MenuInfo menuInfo;

    [SerializeField]
    private ConfirmButton confirmButton;
    private Toggle toggle;

    private void Awake()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        characterIcon = characterIconObject.GetComponent<Image>();
        bulletImage = bulletObject.gameObject.GetComponent<Image>();
        skillInfoTxt.text = skillInfoObject.GetComponent<TextMeshPro>().text;
        aboutCharacterTxt.text = aboutCharacterObject.GetComponent<TextMeshPro>().text;
        characterName.text = characterNameObject.GetComponent<Text>().text;
    }

    public void SetActiveMenu()
    {
        if (toggle.isOn)
            menuInfo.SetMenu(
                characterIcon,
                bulletImage,
                characterName,
                skillInfoTxt,
                aboutCharacterTxt
            );
    }

    public void SendSoldierPrefab()
    {
        if (toggle.isOn)
            confirmButton.soldierPrefab = soldierPrefab;
    }
}
