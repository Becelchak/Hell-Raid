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

    [SerializeField]
    protected int attackDamage;

    [SerializeField]
    protected int attackRange;

    [SerializeField]
    private float speed;
    private float attackTimer;
    public bool playerChasing;

    [SerializeField]
    private Transform playerTransform;
    private NavMeshAgent agent;

    [SerializeField]
    private Soldier soldier;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
    }

    public virtual void Attack()
    {
        // TODO Call with a target
        if (attackTimer >= 1f / attackSpeed)
        {
            soldier.TakeDamage(attackDamage);
            attackTimer = 0f;
        }
    }

    public virtual void TakeDamage(int damageValue)
    {
        Health -= damageValue;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move() //У некоторых мобов свои маршруты.
    {
        agent.speed = speed;
        agent.SetDestination(
            new Vector3(
                playerTransform.position.x,
                playerTransform.position.y,
                playerTransform.position.z
            )
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(Weapon.damage);
        }
    }
}
