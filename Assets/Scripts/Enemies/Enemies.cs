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
    private Soldier soldierTarget;
    private Weapon soldierWeapon;

    [SerializeField]
    private List<Soldier_control> soldiersStatus;

    private List<Soldier> soldiers = new List<Soldier>();
    private List<Weapon> soldiersWeapon = new List<Weapon>();
    public bool isBuffed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        foreach (var soldier in soldiersStatus)
        {
            soldiers.Add(soldier.gameObject.GetComponent<Soldier>());
        }
        foreach (var soldier in soldiers)
        {
            soldiersWeapon.Add(soldier.gameObject.transform.GetChild(1).GetComponent<Weapon>());
        }
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
    }

    private void FixedUpdate()
    {
        FindActivePlayer();
        soldierWeapon = GetWeapon();
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

    private void FindActivePlayer()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            print("ищу игрока");
            if (soldiersStatus[i].isPlayer)
            {
                soldierTarget = soldiers[i];
                playerTransform = soldierTarget.transform;
            }
        }
    }

    private Weapon GetWeapon()
    {
        foreach (var weapon in soldiersWeapon)
        {
            if (weapon.gameObject.activeSelf)
                return weapon;
        }
        return null;
    }
}
