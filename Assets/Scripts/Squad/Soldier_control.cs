using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier_control : MonoBehaviour
{
    [Header("Move parameter")]
    private float speed = 1f;

    [SerializeField]
    private bool isPlayer = false;

    [SerializeField]
    private GameObject targetFollow;
    private bool canFollow = true;
    private bool onGround = true;

    private Weapon.Weapons_Type weapon;

    [Header("Class parameter")]
    [SerializeField]
    public SoldierClass type;
    private float healPointTotal;

    [SerializeField]
    private float healPointTemp;

    void Start()
    {
        switch (type)
        {
            case SoldierClass.Commander:
                var soldierCLass1 = GetComponent<Commander>();
                speed = soldierCLass1.moveSpeed;
                healPointTotal = soldierCLass1.GetHealPoint();
                weapon = soldierCLass1.GetWeaponType();
                break;
            case SoldierClass.Engineer:
                var soldierCLass2 = GetComponent<Engineer>();
                speed = soldierCLass2.moveSpeed;
                healPointTotal = soldierCLass2.GetHealPoint();
                weapon = soldierCLass2.GetWeaponType();
                break;
            case SoldierClass.Grenadier:
                var soldierCLass3 = GetComponent<Grenadier>();
                speed = soldierCLass3.moveSpeed;
                healPointTotal = soldierCLass3.GetHealPoint();
                weapon = soldierCLass3.GetWeaponType();
                break;
            case SoldierClass.Machine_gunner:
                var soldierCLass4 = GetComponent<MachineGunner>();
                speed = soldierCLass4.moveSpeed;
                healPointTotal = soldierCLass4.GetHealPoint();
                weapon = soldierCLass4.GetWeaponType();
                break;
            case SoldierClass.Medic:
                var soldierCLass5 = GetComponent<Medic>();
                speed = soldierCLass5.moveSpeed;
                healPointTotal = soldierCLass5.GetHealPoint();
                weapon = soldierCLass5.GetWeaponType();
                break;
            case SoldierClass.Sniper:
                var soldierCLass6 = GetComponent<Sniper>();
                speed = soldierCLass6.moveSpeed;
                healPointTotal = soldierCLass6.GetHealPoint();
                weapon = soldierCLass6.GetWeaponType();
                break;
        }

        healPointTemp = healPointTotal;
    }

    void Update()
    {
        if (isPlayer)
        {
            // Moving
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-speed * 5 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(speed * 5 * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.Space) && onGround)
            {
                transform.position += new Vector3(0, 2f, 0);
                onGround = false;
            }
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            var weapon = transform.GetChild(1).GetComponent<Weapon>();
            weapon.SetWeapon(this.weapon);

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(
                mousePosition.y - transform.position.y,
                mousePosition.x - transform.position.x
            );
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            weapon.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            // Shooting
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                if (weapon.timerShoot < 0)
                {
                    weapon.Shoot();
                    weapon.RestoreTimer();
                }
            }
            weapon.timerShoot -= Time.deltaTime;
        }
        else
        {
            var target_pos = targetFollow.transform.position;

            var weapon = transform.GetChild(1).GetComponent<Weapon>();
            weapon.SetWeapon(this.weapon);

            if (Input.GetKey(KeyCode.A))
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target_pos,
                    -speed * 5 * Time.deltaTime
                );
                if (CheckJumpLeft())
                    transform.position += new Vector3(-1f, 10f * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target_pos,
                    speed * 5 * Time.deltaTime
                );
            else if (canFollow)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    target_pos,
                    speed * 5 * Time.deltaTime
                );
                if (CheckJumpRight())
                    transform.position += new Vector3(0, 10f * Time.deltaTime, 0);
            }
        }
    }

    public float GetHealthPoint()
    {
        var percent = healPointTotal / 100;
        // Debug.Log($"{healPointTemp / (percent * 100)}");
        return healPointTemp / (percent * 100);
    }

    private bool CheckJumpRight()
    {
        return targetFollow.transform.position.y - transform.position.y > 0
            || targetFollow.transform.position.x - transform.position.x > 5;
    }

    private bool CheckJumpLeft()
    {
        return targetFollow.transform.position.x - transform.position.x < 1.9f;
    }

    public void SetTarget(GameObject target)
    {
        targetFollow = target;
    }

    public void SetControlAi()
    {
        isPlayer = false;
    }

    public void SetControlPlayer()
    {
        isPlayer = true;
    }

    public void TakeDamage(float damage)
    {
        healPointTemp -= damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Bullet"))
            return;
        canFollow = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Bullet"))
            return;
        canFollow = true;
    }

    public enum SoldierClass
    {
        Commander = 0,
        Engineer = 1,
        Grenadier = 2,
        Machine_gunner = 3,
        Medic = 4,
        Sniper = 5,
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            onGround = true;
    }
}
