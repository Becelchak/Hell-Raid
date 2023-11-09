using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject self;
    private Vector3 direction_move;
    private float speed;
    private float angleDir;
    void Start()
    {

    }

    void Update()
    {
        if (self.transform.position == direction_move) Destroy(self);
        self.transform.position = Vector3.MoveTowards(self.transform.position, direction_move, speed * Time.deltaTime);
        Destroy(self,5f);
    }

    void FixedUpdate()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(direction_move.x, direction_move.y) * speed * Time.deltaTime;
    }

    public void Instantiate(GameObject bullet, Vector3 direction, float speed)
    {
        self = bullet;
        direction_move = new Vector3(direction.x, direction.y, 1); ;
        this.speed = speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Get Angle in Radians
        var angleRad = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x);
        // Get Angle in Degrees
        angleDir = (180 / Mathf.PI) * angleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, angleDir);
    }
}