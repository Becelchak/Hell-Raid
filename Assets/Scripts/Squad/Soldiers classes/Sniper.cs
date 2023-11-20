using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private float healPoint = 100;
    [SerializeField] public float moveSpeed = 0.3f;
    [SerializeField] private float skillCoolDown = 5;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Sniper_Rifle;
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
