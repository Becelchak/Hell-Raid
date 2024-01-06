using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour, IDamageable
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
    protected float attackRange;
    public float speed;
    private float attackTimer;

    private NavMeshAgent agent;

    public static Transform playerTransform;
    public static Soldier soldierTarget;
    public static Weapon soldierWeapon;

    public List<GameObject> soldiers;

    private Squad_logic squadLogic;

    // [SerializeField]
    // private List<Soldier_control> soldiersStatus;

    // private List<Soldier> soldiers = new List<Soldier>();
    // private List<Weapon> soldiersWeapon = new List<Weapon>();
    public bool isBuffed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Start()
    {
        // Требуется регулярно обновлять списки солдат, их оружия и статусов, на случай если один из солдат умрет
        // -> иначе будет ошибка ссылки на несуществующие объекты
        // foreach (var soldier in soldiersStatus)
        // {
        //     soldiers.Add(soldier.gameObject.GetComponent<Soldier>());
        // }
        // foreach (var soldier in soldiers)
        // {
        //     soldiersWeapon.Add(soldier.gameObject.transform.GetChild(1).GetComponent<Weapon>());
        // }
        for (int i = 0; i < soldiers.Count; i++) { }
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        Move();
    }

    private void FixedUpdate()
    {
        // FindActivePlayer();
        // soldierWeapon = GetWeapon();
        Attack();
    }

    public virtual void Attack()
    {
        // Место ссылки на потенциально несуществующий объект
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

    // private void FindActivePlayer()
    // {
    //     for (int i = 0; i < soldiers.Count; i++)
    //     {
    //         print("ищу игрока");
    //         // Место ссылки на потенциально несуществующий объект
    //         if (soldiersStatus[i].isPlayer)
    //         {
    //             soldierTarget = soldiers[i];
    //             playerTransform = soldierTarget.transform;
    //         }
    //         print(playerTransform);
    //     }
    // }

    // private Weapon GetWeapon()
    // {
    //     // Место ссылки на потенциально несуществующий объект
    //     foreach (var weapon in soldiersWeapon)
    //     {
    //         if (weapon.gameObject.activeSelf)
    //             return weapon;
    //     }
    //     return null;
    // }
}
