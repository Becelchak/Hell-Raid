using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    [SerializeField] private float healPoint = 150;
    [SerializeField] public float moveSpeed = 1f;
    [SerializeField] private float skillCoolDown = 45f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type weapon = global::Weapon.Weapons_Type.Pistol;
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
