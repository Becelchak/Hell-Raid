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

    private float skillTimer
    {
        get => skillCoolDown;
        set { }
    }

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

    void Update()
    {
        skillTimer -= Time.deltaTime;
        if (skillTimer <= 0)
        {
            skillTimer = skillCoolDown;
            UseSkill();
        }
    }

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        Debug.Log($"{Health}");
    }

    protected virtual void UseSkill()
    {

    }

    public void Die()
    {
        squadLogic.DeleteFirstSoldier();
    }
}
