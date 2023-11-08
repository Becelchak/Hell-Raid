using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string Weapon_Type = "Pistol";
    [SerializeField] private SpriteRenderer Sprite;
    private float coolDownShoot = 0.5f;
    private int damage = 10;
    private int ammo_in_magazine = 15;
    private int count_magazine = 3;

    private float speed_bullet;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Shoot(float speed_b)
    {
        switch (Weapon_Type)
        {
            case "Pistol":
                speed_bullet = speed_b;
                ShootPistol();
                break;
            default:
                break;
        }
    }

    private void ShootPistol()
    {
        //var bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var bullet = Instantiate(Sprite.gameObject);
        bullet.transform.position = transform.GetChild(1).position;

        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z += Camera.main.nearClipPlane;

        bullet.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        bullet.AddComponent<Bullet>().Instantiate(bullet, mousePosition, Quaternion.identity, speed_bullet);

    }
}
