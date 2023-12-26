using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Mathematics;
using UnityEngine;

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

    [SerializeField]
    private GameObject cinemaMachineObject;

    [SerializeField]
    private GameObject soldierClasses;
    private Collider2D soldierCollider;
    private Soldier_control soldier;
    private Transform soldierTransform;
    private SpriteRenderer soldierImage;
    private CinemachineVirtualCamera cinemachine;

    public void Start()
    {
        squadTransform = squadTransform.gameObject.GetComponent<Transform>();
        cinemachine = cinemaMachineObject.GetComponent<CinemachineVirtualCamera>();
        // soldierCollider = soldierPrefab.gameObject.GetComponent<Collider2D>();
        // soldierTransform = soldierPrefab.gameObject.GetComponent<Transform>();
        // soldier = soldierPrefab.gameObject.GetComponent<Soldier_control>();
        // soldierImage = soldierPrefab.gameObject.GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);
    }

    public void Confirm() //Выделить методы
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
        if (Index == 0)
        {
            var soliderTransform = newSoldier.GetComponent<Transform>();
            newSoldier.GetComponent<Collider2D>().enabled = true;
            newSoldier.GetComponent<Soldier_control>().isPlayer = true;
            soldierTransform.Find("Weapon").gameObject.SetActive(true);
            newSoldier.GetComponent<SpriteRenderer>().enabled = true;
            cinemachine.Follow = soliderTransform;
        }
        // Instantiate(newSoldier, );
    }

    private void StartLobby()
    {
        if (IsSquadFull())
        {
            soldierClasses.SetActive(false);
            startGameBtn.SetActive(true);
        }
    }

    private bool IsSquadFull()
    {
        return squad.transform.childCount == 4;
    }
}
