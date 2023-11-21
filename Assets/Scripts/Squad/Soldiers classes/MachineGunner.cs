using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunner: MonoBehaviour
{
    private float healPoint = 200f;
    [SerializeField] public float moveSpeed = 0.7f;
    private float skillCoolDown = 30f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Heavy_Machine_Gun;

    public float GetHealPoint()
    {
        return healPoint;
    }

    public Weapon.Weapons_Type GetWeaponType()
    {
        return weapon;
    }
}
