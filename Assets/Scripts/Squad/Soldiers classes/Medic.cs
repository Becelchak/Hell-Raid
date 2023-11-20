using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : MonoBehaviour
{
    [SerializeField] private float healPoint = 125;
    [SerializeField] public float moveSpeed = 1.1f;
    [SerializeField] private float skillCoolDown = 120f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Shoot_Gun;
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
