using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunner: MonoBehaviour
{
    [SerializeField] private float healPoint = 200;
    [SerializeField] public float moveSpeed = 0.7f;
    [SerializeField] private float skillCoolDown = 30f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Heavy_Machine_Gun;
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
