using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class LevelStart : MonoBehaviour
{
    public List<GameObject> squad;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void GetLoad()
    {
        YandexGame.savesData.level = SceneManager.GetActiveScene().buildIndex - 1;
        if (YandexGame.savesData.level == 1)
        {
            YandexGame.savesData.EnemiesDeath = 0;
        }
        YandexGame.SaveProgress();
        for (int i = 0; i < squad.Count; i++)
        {
            squad[i].GetComponent<SpriteRenderer>().sprite = YandexGame.savesData.sprites[i];
            var soldierControl = squad[i].GetComponent<Soldier_control>();
            soldierControl.type = YandexGame.savesData.soldierClasses[i];
            squad[i].AddComponent(Type.GetType(soldierControl.type.ToString()));
            var soldier = squad[i].GetComponent<Soldier>();
            soldier.squadLogic = soldierControl.squadLogic;
            soldier.Health = YandexGame.savesData.hpSoldiers[i];
            soldier.Weapon = YandexGame.savesData.weaponsTypes[i];
            var weapon = squad[i].transform.Find("Weapon").gameObject.GetComponent<Weapon>();
            weapon.ammo_in_magazine = YandexGame.savesData.currentsAmmo[i];
            weapon.count_magazine = YandexGame.savesData.ammoInMagazines[i];
            weapon.ammo_temp = YandexGame.savesData.tempsAmmo[i];
        }

        Enemies.EnemiesDeath = YandexGame.savesData.EnemiesDeath;
    }
}
