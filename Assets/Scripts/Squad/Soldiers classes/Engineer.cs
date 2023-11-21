using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : MonoBehaviour
{
    private float healPoint = 125f;
    [SerializeField] public float moveSpeed = 1f;
    private float skillCoolDown = 120f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Little_Machine_Gun;

    public float GetHealPoint()
    {
        return healPoint;
    }
    public Weapon.Weapons_Type GetWeaponType()
    {
        return weapon;
    }
}
