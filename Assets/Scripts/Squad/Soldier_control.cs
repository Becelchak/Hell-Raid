using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Soldier_control : MonoBehaviour
{
    [Header("Move parameter")]
    private float speed = 1f;
    public bool isPlayer = false;

    [SerializeField]
    private GameObject targetFollow;

    //private bool canFollow = true;
    private bool onGround = true;

    private Weapon.Weapons_Type weapon;

    [Header("Class parameter")]
    public SoldierClass type;
    private float healPointTotal;

    [SerializeField]
    private float healPointTemp;
    private Rigidbody2D rigBody;

    private Soldier soldier;
    public Squad_logic squadLogic;

    void Start()
    {
        switch (type)
        {
            case SoldierClass.Commander:
                var soldierCLass1 = GetComponent<Commander>();
                speed = soldierCLass1.MoveSpeed;
                healPointTotal = soldierCLass1.Health;
                weapon = soldierCLass1.Weapon;
                break;
            case SoldierClass.Engineer:
                var soldierCLass2 = GetComponent<Engineer>();
                speed = soldierCLass2.MoveSpeed;
                healPointTotal = soldierCLass2.Health;
                weapon = soldierCLass2.Weapon;
                break;
            case SoldierClass.Grenadier:
                var soldierCLass3 = GetComponent<Grenadier>();
                speed = soldierCLass3.MoveSpeed;
                healPointTotal = soldierCLass3.Health;
                weapon = soldierCLass3.Weapon;
                break;
            case SoldierClass.Machine_gunner:
                var soldierCLass4 = GetComponent<MachineGunner>();
                speed = soldierCLass4.MoveSpeed;
                healPointTotal = soldierCLass4.Health;
                weapon = soldierCLass4.Weapon;
                break;
            case SoldierClass.Medic:
                var soldierCLass5 = GetComponent<Medic>();
                speed = soldierCLass5.MoveSpeed;
                healPointTotal = soldierCLass5.Health;
                weapon = soldierCLass5.Weapon;
                break;
            case SoldierClass.Sniper:
                var soldierCLass6 = GetComponent<Sniper>();
                speed = soldierCLass6.MoveSpeed;
                healPointTotal = soldierCLass6.Health;
                weapon = soldierCLass6.Weapon;
                break;
        }

        healPointTemp = healPointTotal;
        soldier = GetComponent<Soldier>();
        rigBody = GetComponent<Rigidbody2D>();
        squadLogic = GetComponentInParent<Squad_logic>();
    }

    void Update()
    {
        if (isPlayer)
        {
            // Moving
            var axisX = Input.GetAxisRaw("Horizontal");
            rigBody.velocity = new Vector2(axisX * speed * 5, rigBody.velocity.y);

            if (Input.GetButtonDown("Jump") && onGround)
            {
                Jump();
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

            //Change unit
            if (Input.GetKeyDown(KeyCode.Alpha1))
                squadLogic.ChangeSoldier(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                squadLogic.ChangeSoldier(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                squadLogic.ChangeSoldier(2);
            if (Input.GetKeyDown(KeyCode.Alpha4))
                squadLogic.ChangeSoldier(3);
        }
        else
        {
            var target_pos = targetFollow.transform.position;
            transform.position = target_pos;

            #region

            //var target_pos = targetFollow.transform.position;

            //var weapon = transform.GetChild(1).GetComponent<Weapon>();
            //weapon.SetWeapon(this.weapon);

            //if (Input.GetKey(KeyCode.A))
            //{
            //    transform.position = Vector3.MoveTowards(
            //        transform.position,
            //        target_pos,
            //        -speed * 3 * Time.deltaTime
            //    );
            //    //if (CheckJumpLeft())
            //    //    transform.position = new Vector3(target_pos.x - 2f, transform.position.y +2f, 0);
            //}
            //if (Input.GetKey(KeyCode.D))
            //    transform.position = Vector3.MoveTowards(
            //        transform.position,
            //        target_pos,
            //        speed * 3 * Time.deltaTime
            //    );
            //else if (canFollow)
            //{
            //    transform.position = Vector3.MoveTowards(
            //        transform.position,
            //        target_pos,
            //        speed * 5 * Time.deltaTime
            //    );
            //    //if (CheckJumpRight())
            //    //    transform.position = new Vector3(target_pos.x - 2f, transform.position.y + 2f, 0);
            //}
            //if(CkeckTargetFarAway())
            //    transform.position = new Vector3(target_pos.x - 2f, target_pos.y + 2f, 0);

            #endregion
        }

        healPointTemp = soldier.Health;
    }

    public float GetHealthPoint()
    {
        var percent = healPointTotal / 100;
        return healPointTemp / (percent * 100);
    }

    private void Jump()
    {
        rigBody.AddForce(new Vector2(0f, speed * 450));
        onGround = false;
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

    public bool IsPlayerControl()
    {
        return isPlayer;
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
