using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiSoldierHud : MonoBehaviour
{
    [SerializeField] private GameObject squad;
    private List<Soldier_control> soldiers;
    private List<CanvasGroup> soldiersIconsCanvasGroups;
    private Image[] soldiersHealthBar = new Image[4];
    void Start()
    {
        soldiers = squad.GetComponentsInChildren<Soldier_control>().ToList();
        soldiersIconsCanvasGroups = GetComponentsInChildren<CanvasGroup>().ToList();

        for (var j = 0; j < soldiersIconsCanvasGroups.Count; j++)
        {
            var g = soldiersIconsCanvasGroups[j].gameObject;
            var i = g.transform.GetChild(1).gameObject.GetComponent<Image>();
            soldiersHealthBar[j] = i;
        }
    }

    void Update()
    {

        for (var i = 0; i < soldiers.Count; i++)
        {
            var icon = soldiersIconsCanvasGroups[i];
            icon.alpha = 1;
            icon.interactable = true;
        }

        HealthUpdate();
    }

    void HealthUpdate()
    {
        for (var i = 0; i < soldiers.Count; i++)
        {
            var soldierHealth = soldiers[i].GetHealthPoint();
            soldiersHealthBar[i].fillAmount = soldierHealth;
        }
    }

    public void RefreshSquad(List<GameObject> units = null)
    {
        soldiers = squad.GetComponentsInChildren<Soldier_control>().ToList();
        if (units != null)
        {
            soldiers.Clear();
            foreach (var unit in units)
            {
                soldiers.Add(unit.GetComponent<Soldier_control>());
            }
        }

        foreach (var icon in soldiersIconsCanvasGroups)
        {
            icon.alpha = 0;
            icon.interactable = false;
        }
    }
}
