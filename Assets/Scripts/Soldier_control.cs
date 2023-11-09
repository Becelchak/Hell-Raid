using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_control : MonoBehaviour
{
    [Header("Move parameter")]
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool Is_Player = false;
    [SerializeField] private GameObject Target_Follow;
    private bool canFollow = true;
    //private float collDownFollow = 0.1f;

    [Header("Weapon parameter")]
    [SerializeField] private Weapon.Weapons_Type Weapon = global::Weapon.Weapons_Type.Pistol;
    void Start()
    {
        
    }

    void Update()
    {
        if(Is_Player)
        {
            // Moving
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                transform.position += new Vector3(0, 10f * Time.deltaTime, 0);
            }
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            var weapon = transform.GetChild(1);
            weapon.GetComponent<Weapon>().SetWeapon(Weapon);
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            weapon.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            // Shooting
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                weapon.GetComponent<Weapon>().timerShoot -= Time.deltaTime;
                if(weapon.GetComponent<Weapon>().timerShoot < 0)
                {
                    weapon.GetComponent<Weapon>().Shoot();
                    weapon.GetComponent<Weapon>().RestoreTimer();
                }
                
            }
        }
        else
        {
            var target_pos = Target_Follow.transform.position;

            if(Input.GetKey(KeyCode.A))
            {
                transform.position = Vector3.MoveTowards(transform.position, target_pos, -Speed * Time.deltaTime);
                if (CheckJumpLeft())
                    transform.position += new Vector3(-1f, 10f * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
                transform.position = Vector3.MoveTowards(transform.position, target_pos, Speed * Time.deltaTime);
            else if(canFollow)
            {
                transform.position = Vector3.MoveTowards(transform.position, target_pos, Speed * Time.deltaTime);
                if (CheckJumpRight())
                    transform.position += new Vector3(0, 10f * Time.deltaTime, 0);
            }

            //else
            //{
            //    collDownFollow -= Time.deltaTime;
            //}

            //var weapon = transform.GetChild(1);
            //weapon.GetComponent<Weapon>().timerShoot -= Time.deltaTime;
            //if (weapon.GetComponent<Weapon>().timerShoot < 0)
            //{
            //    weapon.GetComponent<Weapon>().Shoot();
            //    weapon.GetComponent<Weapon>().RestoreTimer();
            //}
        }
    }

    private bool CheckJumpRight()
    {
        return Target_Follow.transform.position.y - transform.position.y > 0 || Target_Follow.transform.position.x - transform.position.x > 5;
    }
    private bool CheckJumpLeft()
    {
        return Target_Follow.transform.position.x - transform.position.x < 1.9f;
    }

    public void SetTarget(GameObject target)
    {
        Target_Follow = target;
    }

    public void SetControlAi()
    {
        Is_Player = false;
    }

    public void SetControlPlayer()
    {
        Is_Player = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name.Contains("Bullet")) return;
        canFollow = false;
        //collDownFollow = 0.5f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Bullet")) return;
        canFollow = true;
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.name.Contains("Bullet")) return;
    //    canFollow = false;
    //    collDownFollow = 0.5f;
    //}

    //void OnCollisionExit2D(Collision2D other)
    //{
    //    if (other.gameObject.name.Contains("Bullet")) return;
    //    canFollow = true;
    //}
}
