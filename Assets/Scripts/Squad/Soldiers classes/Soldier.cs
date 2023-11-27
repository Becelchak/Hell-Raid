using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Soldier : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int health;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float skillCoolDown;

    [SerializeField]
    private Squad_logic squadLogic;
    public int Health
    {
        get => health;
        set
        {
            health = value;
            if (health <= 0)
                Die();
        }
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [Header("Weapon parameter")]
    public Weapon.Weapons_Type Weapon;

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
    }

    public void Die()
    {
        squadLogic.DeleteFirstSoldier();
    }
}
