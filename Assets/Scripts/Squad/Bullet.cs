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

    void Start()
    {
        self = gameObject;
        bulletBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //self.transform.position = Vector3.MoveTowards(
        //    self.transform.position,
        //    directionMove,
        //    speed * Time.deltaTime
        //);
        gameObject.transform.Translate(Vector3.right * speed * Time.deltaTime);
        //bulletBody.AddRelativeForce(new Vector2(directionMove.x,directionMove.y) * speed * Time.deltaTime, ForceMode2D.Impulse);
        Destroy(self, 5f);
    }

    public void Instantiate(GameObject bullet, Vector3 direction, float speed)
    {
        self = bullet;
        self.tag = "Bullet";
        directionMove = new Vector3(direction.x, direction.y, 1);
        this.speed = speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Get Angle in Radians
        var angleRad = Mathf.Atan2(
            direction.y - transform.position.y,
            direction.x - transform.position.x);
        // Get Angle in Degrees
        angleDirection = (180 / Mathf.PI) * angleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, angleDirection);
    }
}
