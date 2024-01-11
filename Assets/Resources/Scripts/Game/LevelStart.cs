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
            print(soldier.squadLogic);
            print(soldierControl.squadLogic);
            soldier.squadLogic = soldierControl.squadLogic;
            print(soldierControl.squadLogic);
            soldier.Health = YandexGame.savesData.hpSoldiers[i];
            soldier.Weapon = YandexGame.savesData.weaponsTypes[i];

            //switch (soldierControl.type)
            //{
            //    case Soldier_control.SoldierClass.Commander:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/командир(2)");
            //        soldier.Weapon = Weapon.Weapons_Type.Pistol;
            //        break;
            //    case Soldier_control.SoldierClass.Medic:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/медик(1)");
            //        soldier.Weapon = Weapon.Weapons_Type.Shoot_Gun;
            //        break;
            //    case Soldier_control.SoldierClass.MachineGunner:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/пулеметчик(3)");
            //        soldier.Weapon = Weapon.Weapons_Type.Heavy_Machine_Gun;
            //        break;
            //    case Soldier_control.SoldierClass.Engineer:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/инженер(1)");
            //        soldier.Weapon = Weapon.Weapons_Type.Little_Machine_Gun;
            //        break;
            //    case Soldier_control.SoldierClass.Grenadier:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/гренадер(1)");
            //        soldier.Weapon = Weapon.Weapons_Type.Grenade_Launcher;
            //        break;
            //    case Soldier_control.SoldierClass.Sniper:
            //        squad[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Texture/Soldiers/снайпер(4)");
            //        soldier.Weapon = Weapon.Weapons_Type.Sniper_Rifle;
            //        break;
            //}
        }
    }
}
