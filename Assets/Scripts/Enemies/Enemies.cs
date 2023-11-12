using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemies : MonoBehaviour, IDamageable
{
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

    [SerializeField]
    private int health;

    [SerializeField]
    private float attackSpeed;
    public int attackDamage;

    [SerializeField]
    private float speed;
    private float attackTimer;
    public bool playerChasing;
    private Transform playerTransform;
    private NavMeshAgent agent;

    private void Awake()
    {
        playerTransform = FindObjectOfType<Soldier_control>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= 1f / attackSpeed)
        {
            Attack();
            print(attackTimer);
            attackTimer = 0f;
        }
        Move();
    }

    public virtual void Attack()
    {
        print("Атака");
    }

    public virtual void TakeDamage(int damageValue)
    {
        Health -= damageValue;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        agent.speed = speed;
        agent.SetDestination(playerTransform.position);
    }
}
