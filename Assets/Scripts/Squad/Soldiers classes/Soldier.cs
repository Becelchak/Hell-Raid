using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Soldier : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int health;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    protected float skillCoolDown = 60f;
    protected float skillTimer;
    protected bool canUseSkill = false;
    protected Image skillIcon;
    public Squad_logic squadLogic;

    protected Soldier_control control;
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

    public int maxHealth;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    [Header("Weapon parameter")]
    public Weapon.Weapons_Type Weapon;

    void Start()
    {
        skillTimer = skillCoolDown;
        control = GetComponent<Soldier_control>();
        skillIcon = GameObject.Find("IconSkill").GetComponent<Image>();
        skillIcon.fillAmount = 0;
        maxHealth = health;
    }

    void Update()
    {
        if (control.IsPlayerControl())
        {
            if (!canUseSkill)
            {
                skillTimer -= Time.deltaTime;
                if (skillTimer <= 0)
                    canUseSkill = true;
                //skillIcon.fillAmount += 1 / skillCoolDown * Time.deltaTime;
                skillIcon.fillAmount = 1 - skillTimer / (skillCoolDown / 100 * 100);
            }
            if (canUseSkill && Input.GetKeyDown(KeyCode.R))
            {
                skillTimer = skillCoolDown;
                UseSkill();
                canUseSkill = false;
                skillIcon.fillAmount = 0;
            }
        }
    }

    public void TakeDamage(int damageValue)
    {
        Health -= damageValue;
        Debug.Log($"{Health}");
    }

    protected virtual void UseSkill() { }

    public void Die()
    {
        squadLogic.DeleteFirstSoldier();
    }

    protected Squad_logic GetSquad()
    {
        return squadLogic;
    }

    public void ReturnFullHealth()
    {
        Health = maxHealth;
    }
}
