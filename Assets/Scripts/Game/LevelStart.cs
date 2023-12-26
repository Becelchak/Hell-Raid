using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class LevelStart : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> squad;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void GetLoad()
    {
        for (int i = 0; i < squad.Count; i++)
        {
            squad[i].GetComponent<SpriteRenderer>().sprite = YandexGame.savesData.sprites[i];
            var soldierControl = squad[i].GetComponent<Soldier_control>();
            soldierControl.type = YandexGame.savesData.soldierClasses[i];
            squad[i].AddComponent(Type.GetType(soldierControl.type.ToString()));
            var soldier = squad[i].GetComponent<Soldier>();
            soldier.squadLogic = soldierControl.squadLogic;
            soldier.Health = YandexGame.savesData.hpSoldiers[i];
            print(YandexGame.savesData.weaponsTypes[i]);
            soldier.Weapon = YandexGame.savesData.weaponsTypes[i];
        }
    }
}
