using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking.Types;

public class Commander : MonoBehaviour
{
    private static float healPoint = 150;

    [SerializeField]
    public float moveSpeed = 1f;
    private float skillCoolDown = 45f;

    [Header("Weapon parameter")]
    [SerializeField]
    private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Pistol;

    public float GetHealPoint()
    {
        return healPoint;
    }

    public Weapon.Weapons_Type GetWeaponType()
    {
        return weapon;
    }

    public static void TakeDamage(float damage)
    {
        healPoint -= damage;
        if (healPoint > 0)
            print(healPoint);
    }
}
