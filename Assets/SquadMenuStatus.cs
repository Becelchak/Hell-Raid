using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SquadMenuStatus : MonoBehaviour
{
    [SerializeField] private GameObject squad;
    private List<Soldier_control> soldiers;
    private List<CanvasGroup> squadNow;
    private Medic soldierRecrut;
    private bool isPickSoldier = false;
    void Start()
    {
        squadNow = GameObject.Find("SquadNow").GetComponentsInChildren<CanvasGroup>().ToList();
        soldiers = squad.GetComponentsInChildren<Soldier_control>().ToList();
        for (var i = 0; i < soldiers.Count; i++)
        {
            squadNow[i].alpha = 1f;
            squadNow[i].interactable = true;
            squadNow[i].blocksRaycasts = true;
            var soldierClass = soldiers[i].type.ToString();
            squadNow[i].GetComponentInChildren<Text>().text = soldierClass; 


        }
    }

    void Update()
    {
        
    }

    public void SetNowMedic()
    {
        soldierRecrut = Instantiate(new Medic());
        isPickSoldier = true;
    }

    public void AddRecrutInSquad()
    {
    }
}
