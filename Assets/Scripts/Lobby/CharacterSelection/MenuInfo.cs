using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuInfo : ToggleDataTransition
{
    [SerializeField]
    private List<Toggle> toggles;

    [SerializeField]
    private Slot slot;

    private void Awake()
    {
        characterIcon = characterIconObject.GetComponent<Image>();
        characterName = characterNameObject.GetComponent<TextMeshProUGUI>();
        bulletImage = bulletObject.gameObject.GetComponent<Image>();
        skillInfoTxt = skillInfoObject.GetComponent<TextMeshProUGUI>();
        aboutCharacterTxt = aboutCharacterObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetMenu(
        Image characterIcon,
        Image bullet,
        TextMeshProUGUI characterName,
        TextMeshProUGUI skillInfo,
        TextMeshProUGUI aboutCharacter
    )
    {
        gameObject.SetActive(true);
        SetToggleNotInteractable();
        this.characterIcon.sprite = characterIcon.sprite;
        this.bulletImage.sprite = bullet.sprite;
        this.characterName.text = characterName.text;
        this.skillInfoTxt.text = skillInfo.text;
        this.aboutCharacterTxt.text = aboutCharacter.text;
        slot.SetSoldierIcon(this.characterIcon);
    }

    private void SetToggleNotInteractable()
    {
        foreach (var toggle in toggles)
            toggle.interactable = false;
    }
}
