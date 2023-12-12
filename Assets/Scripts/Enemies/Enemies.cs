using System;
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
    public float attackSpeed;

    public int attackDamage;

    [SerializeField]
    protected int attackRange;
    public float speed;
    private float attackTimer;

    private Transform playerTransform;
    private NavMeshAgent agent;
    private Soldier soldier;

    [SerializeField]
    private Weapon soldierWeapon;

    [SerializeField]
    private List<Soldier_control> soldiers;
    public bool isBuffed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start() { }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
    }

    private void FixedUpdate()
    {
        playerTransform = FindPlayerTransform();
        Attack();
    }

    public virtual void Attack()
    {
        float distance = (float)
            Math.Round((playerTransform.position - gameObject.transform.position).sqrMagnitude);
        if (attackTimer >= 1f / attackSpeed && distance < attackRange * attackRange)
        {
            //анимаци атаки
            print("атака");
            UseSkill();
            if (distance == 1.0f)
                soldier.TakeDamage(attackDamage);
            attackTimer = 0f;
        }
    }

    public virtual void UseSkill() { }

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
        agent.SetDestination(new Vector2(playerTransform.position.x, playerTransform.position.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(soldierWeapon.damage);
            Destroy(other.gameObject);
        }
    }

    private Transform FindPlayerTransform()
    {
        foreach (var soldier in soldiers)
        {
            print("ищу игрока");
            if (soldier.isPlayer)
            {
                this.soldier = soldier.gameObject.GetComponent<Soldier>();
                return soldier.transform;
            }
        }
        return this.transform;
    }
}
