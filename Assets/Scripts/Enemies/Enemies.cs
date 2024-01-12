using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Enemies : MonoBehaviour, IDamageable
{
    public static int EnemiesDeath;
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
    protected float attackRange;
    public float speed;
    private float attackTimer;

    private NavMeshAgent agent;

    public static Transform playerTransform;
    public static Soldier soldierTarget;
    public static Weapon soldierWeapon;

    public List<GameObject> soldiers;

    public bool isBuffed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        print(EnemiesDeath);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
    }

    private void FixedUpdate()
    {
        Attack();
        print("работает");
    }

    public virtual void Attack()
    {
        print("заходит");
        float distance = (float)
            Math.Round((playerTransform.position - gameObject.transform.position).sqrMagnitude);
        if (attackTimer >= 1f / attackSpeed && distance < attackRange * attackRange)
        {
            //анимаци атаки
            print("атака");
            UseSkill();
            if (distance == 1.0f)
                soldierTarget.TakeDamage(attackDamage);
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
        EnemiesDeath += 1;
        print(EnemiesDeath);
    }

    public virtual void Move() //У некоторых мобов свои маршруты.
    {
        agent.speed = speed;
        agent.SetDestination(new Vector2(playerTransform.position.x, playerTransform.position.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("BulletSniper") || other.gameObject.CompareTag("Grenade"))
        {
            TakeDamage(soldierWeapon.damage);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(soldierWeapon.damage);
            Destroy(other.gameObject);
        }
    }
}
