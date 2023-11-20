using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : MonoBehaviour
{
    [SerializeField] private float healPoint = 150;
    [SerializeField] public float moveSpeed = 0.9f;
    [SerializeField] private float skillCoolDown = 30;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Grenade_Launcher;
    void Start()
    {

    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        healPoint -= damage;
    }

    public float GetHealPoint()
    {
        return healPoint;
    }

    public Weapon.Weapons_Type GetWeaponType()
    {
        return weapon;
    }
}
