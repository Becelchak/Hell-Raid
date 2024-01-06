using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private static Image soldierIcon;

    [SerializeField]
    private Button removeButton;

    [SerializeField]
    private Slots slots;

    private void Start()
    {
        removeButton.interactable = false;
    }

    public void SetSoldierIcon(Image soldierIcon)
    {
        Slot.soldierIcon = soldierIcon;
    }

    public void ChooseSlots(int index)
    {
        slots.soldierIcons[index].sprite = soldierIcon.sprite;
        slots.squadImages[index].sprite = soldierIcon.sprite;

        slots.confirmButton.Index = index;
        slots.confirmButton.gameObject.SetActive(true);

        foreach (var button in slots.selectButtons)
            button.interactable = false;

        removeButton.interactable = true;
    }

    public void RemoveSoldier(int index)
    {
        soldierIcon.sprite = slots.soldierIcons[index].sprite;
        slots.soldierIcons[index].sprite = slots.emptySlot.sprite;
        slots.squadImages[index].sprite = slots.emptySlot.sprite;

        for (int i = 0; i < slots.selectButtons.Count; i++)
        {
            if (slots.soldierIcons[i].sprite == slots.emptySlot.sprite)
                slots.selectButtons[i].interactable = true;
        }
        removeButton.interactable = false;
        slots.confirmButton.gameObject.SetActive(false);
    }
}
