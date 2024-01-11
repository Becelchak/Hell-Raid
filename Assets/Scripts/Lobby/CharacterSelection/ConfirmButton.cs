using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using YG;

public class ConfirmButton : MonoBehaviour
{
    [NonSerialized]
    public int Index;

    [NonSerialized]
    public GameObject soldierPrefab;

    [SerializeField]
    private Slots slots;

    [SerializeField]
    private Transform squadTransform;

    [SerializeField]
    private GameObject startGameBtn;

    [SerializeField]
    private GameObject squad;
    private Squad_logic logic;

    [SerializeField]
    private GameObject soldierClasses;

    [SerializeField]
    private CinemachineVirtualCamera cinemachine;
    private Soldier_control[] soldierControls = new Soldier_control[4];

    public void Start()
    {
        logic = squad.GetComponent<Squad_logic>();
        squadTransform = squadTransform.gameObject.GetComponent<Transform>();
        this.gameObject.SetActive(false);
    }

    public void Confirm()
    {
        for (int i = 0; i < slots.selectButtons.Count; i++)
        {
            if (slots.soldierIcons[i].sprite == slots.emptySlot.sprite)
                slots.selectButtons[i].interactable = true;
        }

        slots.selectButtons[Index].interactable = false;
        slots.removeButtons[Index].interactable = false;

        SpawnSoldier();
        StartLobby();
    }

    private void SpawnSoldier()
    {
        var newSoldier = Instantiate(
            soldierPrefab,
            squadTransform.position,
            Quaternion.identity,
            squadTransform
        );

        var soldierColtrol = newSoldier.GetComponent<Soldier_control>();
        var soldierComponent = newSoldier.GetComponent<Soldier>();
        var colliderSoldier = newSoldier.GetComponent<Collider2D>();
        soldierComponent.squadLogic = logic;
        soldierColtrol.squadLogic = logic;
        var soldierTransform = newSoldier.GetComponent<Transform>();
        if (Index == 0)
        {
            colliderSoldier.enabled = true;
            soldierColtrol.isPlayer = true;
            soldierTransform.Find("Weapon").gameObject.SetActive(true);

            newSoldier.GetComponent<SpriteRenderer>().enabled = true;
            cinemachine.Follow = soldierTransform;
        }
        soldierControls[Index] = soldierColtrol;
        YandexGame.savesData.sprites[Index] = slots.squadImages[Index].sprite;
        YandexGame.savesData.soldierClasses[Index] = soldierColtrol.type;
        YandexGame.savesData.hpSoldiers[Index] = soldierComponent.Health;
        YandexGame.savesData.weaponsTypes[Index] = soldierComponent.Weapon;
    }

    private void StartLobby()
    {
        if (IsSquadFull())
        {
            soldierClasses.SetActive(false);
            startGameBtn.SetActive(true);
            for (int i = 1; i < soldierControls.Length; i++)
            {
                soldierControls[i].SetTarget(soldierControls[0].gameObject);
            }
            YandexGame.SaveProgress();
        }
    }

    private bool IsSquadFull()
    {
        return squad.transform.childCount == 4;
    }
}
