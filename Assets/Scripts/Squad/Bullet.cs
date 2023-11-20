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
    void Start()
    {

    }

    void Update()
    {
        if (self.transform.position == directionMove) Destroy(self);
        self.transform.position = Vector3.MoveTowards(self.transform.position, directionMove, speed * Time.deltaTime);
        Destroy(self,5f);
    }

    public void Instantiate(GameObject bullet, Vector3 direction, float speed)
    {
        self = bullet;
        directionMove = new Vector3(direction.x, direction.y, 1); ;
        this.speed = speed;

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);

        // Get Angle in Radians
        var angleRad = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x);
        // Get Angle in Degrees
        angleDirection = (180 / Mathf.PI) * angleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, angleDirection);
    }
}