using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject self;
    private Vector3 directionMove;
    private float speed;
    private float angleDirection;
    private Rigidbody2D bulletBody;

    private float timerGrenade = 2f;
    private List<GameObject> targets = new List<GameObject>();

    void Start()
    {
        self = gameObject;
        bulletBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (gameObject.tag == "Bullet" || gameObject.tag == "BulletSniper")
        {
            gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
            Destroy(self, 2f);
        }
    }

    void FixedUpdate()
    {
        if (gameObject.tag == "Grenade")
        {
            gameObject.transform.Translate(
                Vector3.right * speed * 2 * Time.deltaTime + Vector3.down * speed * Time.deltaTime
            );
            Explosion();
        }
    }

    public void Instantiate(GameObject bullet, Vector3 direction, float speed)
    {
        self = bullet;
        directionMove = new Vector3(direction.x, direction.y, 1);
        this.speed = speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Get Angle in Radians
        var angleRad = Mathf.Atan2(
            direction.y - transform.position.y,
            direction.x - transform.position.x
        );
        // Get Angle in Degrees
        angleDirection = (180 / Mathf.PI) * angleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, angleDirection);
    }

    private void Explosion()
    {
        timerGrenade -= Time.fixedDeltaTime;
        if (timerGrenade <= 0)
        {
            foreach (var target in targets)
            {
                if (target.tag == "Player")
                    target.GetComponent<Soldier>().TakeDamage(50);
            }
            Destroy(self);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy(self);
        // if (other.gameObject.tag is "Player")
        //     targets.Add(other.gameObject);
        // if (other.gameObject.CompareTag("Enemy"))
        // {
        //     other.GetComponent<Enemies>().TakeDamage(50);
        //     Destroy(self);
        // }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag is "Player")
            targets.Remove(other.gameObject);
    }
}
