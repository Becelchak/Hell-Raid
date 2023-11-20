using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : MonoBehaviour
{
    [SerializeField] private float healPoint = 125f;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private float skillCoolDown = 120;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Little_Machine_Gun;
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
