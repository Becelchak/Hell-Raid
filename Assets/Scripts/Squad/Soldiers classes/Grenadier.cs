using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : MonoBehaviour
{
    private float healPoint = 150;
    [SerializeField] public float moveSpeed = 0.9f;
    private float skillCoolDown = 30;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Grenade_Launcher;

    public float GetHealPoint()
    {
        return healPoint;
    }

    public Weapon.Weapons_Type GetWeaponType()
    {
        return weapon;
    }
}
