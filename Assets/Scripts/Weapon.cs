using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Weapons_Type Weapon_Type = Weapons_Type.Pistol;
    [SerializeField] private GameObject Bullet;
    private float coolDownShoot = 0;
    public float timerShoot = 0;
    private int damage = 0;
    private int ammo_in_magazine = 0;
    private int ammo_temp = 0;
    private int count_magazine = 0;

    private float speed_bullet;
    void Start()
    {
        SetParameterWeapon();
    }

    void Update()
    {
        Debug.Log($"Ammo : {ammo_temp}  Magazine : {count_magazine}");
        
    }

    public void SetWeapon(Weapons_Type weapon)
    {
        var needRefresh = false || Weapon_Type != weapon;
        Weapon_Type = weapon;
        if(needRefresh)
            SetParameterWeapon();
    }
    public void Shoot()
    {
        switch (Weapon_Type)
        {
            case Weapons_Type.Pistol:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootPistol();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            case Weapons_Type.Shoot_Gun:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootShootGun();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            case Weapons_Type.Heavy_Machine_Gun:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootHeavyMachineGun();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            case Weapons_Type.Little_Machine_Gun:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootLittleMachineGun();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            case Weapons_Type.Sniper_Rifle:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootSniper_Rifle();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            case Weapons_Type.Grenade_Launcher:
                if (ammo_temp > 0)
                {
                    ammo_temp -= 1;
                    ShootGrenade_Launcher();
                }
                else
                {
                    if (count_magazine > 0)
                    {
                        ammo_temp = ammo_in_magazine;
                        count_magazine -= 1;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void SetParameterWeapon()
    {
        switch (Weapon_Type)
        {
            case Weapons_Type.Pistol:
                speed_bullet = 14f;
                coolDownShoot = 0.5f;
                damage = 10; 
                
                ammo_in_magazine = 15;
                ammo_temp = ammo_in_magazine;
                count_magazine = 3;
                break;
            case Weapons_Type.Shoot_Gun:
                speed_bullet = 16f;
                coolDownShoot = 1f;
                damage = 10;

                ammo_in_magazine = 8;
                ammo_temp = ammo_in_magazine;
                count_magazine = 3;
                break;
            case Weapons_Type.Heavy_Machine_Gun:
                var array = new int[] { 5, 10, 15 };
                speed_bullet = 20f;
                coolDownShoot = 0.17f;
                damage = array[Random.Range(0, 2)];

                ammo_in_magazine = 100;
                ammo_temp = ammo_in_magazine;
                count_magazine = 2;
                break;
            case Weapons_Type.Little_Machine_Gun:
                speed_bullet = 22f;
                coolDownShoot = 0.2f;
                damage = 7;

                ammo_in_magazine = 30;
                ammo_temp = ammo_in_magazine;
                count_magazine = 2;
                break;
            case Weapons_Type.Sniper_Rifle:
                speed_bullet = 30f;
                coolDownShoot = 1.5f;
                damage = 80;

                ammo_in_magazine = 5;
                ammo_temp = ammo_in_magazine;
                count_magazine = 3;
                break;
            case Weapons_Type.Grenade_Launcher:
                speed_bullet = 8f;
                coolDownShoot = 1f;
                damage = 50;

                ammo_in_magazine = 1;
                ammo_temp = ammo_in_magazine;
                count_magazine = 5;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RestoreTimer()
    {
        timerShoot = coolDownShoot;
    }

    private void ShootPistol()
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);

    }

    private void ShootShootGun(string dir = "")
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        if (dir == "Top")
        {
            mousePosition.x += (float)Math.Sin(mousePosition.x);
            mousePosition.y += (float)Math.Cos(mousePosition.y);
        }

        if (dir == "Bottom")
        {
            mousePosition.x -= (float)Math.Sin(mousePosition.x);
            mousePosition.y -= (float)Math.Cos(mousePosition.y);
        }

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(0.15f, 0.15f, 1);
        

        if(dir == "")
        {
            ShootShootGun("Top");
            ShootShootGun("Bottom");
        }

        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);
    }

    private void ShootHeavyMachineGun()
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(0.1f, 0.15f, 1);
        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);

    }

    private void ShootLittleMachineGun()
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(0.1f, 0.12f, 1);
        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);

    }

    private void ShootSniper_Rifle()
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(1.2f, 0.5f, 1);
        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);

    }

    private void ShootGrenade_Launcher()
    {
        // Create new bullet in start_fire_position
        var bullet = Instantiate(Bullet);
        bullet.transform.position = transform.GetChild(1).position;
        // Direction fly for bullet
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        // Bullet size and instantiate 'Bullet' component
        bullet.transform.localScale = new Vector3(1f, 1f, 1);
        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, speed_bullet);

    }

    public enum Weapons_Type
    {
        Pistol = 0,
        Shoot_Gun = 1,
        Little_Machine_Gun = 2,
        Heavy_Machine_Gun = 3,
        Sniper_Rifle = 4,
        Grenade_Launcher = 5,
    }
}
