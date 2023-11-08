using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject self;
    private Vector3 direction_move;
    private float speed;
    void Start()
    {

    }

    void Update()
    {
        if (transform.position == direction_move) Destroy(self);
        transform.position = Vector3.MoveTowards(transform.position, direction_move, speed * Time.deltaTime);
    }

    public void Instantiate(GameObject bullet, Vector3 direction, Quaternion i, float speed)
    {
        self = bullet;
        direction_move = direction;
        this.speed = speed;
        Debug.Log($"B - {bullet.transform.position}");
        Debug.Log($"m - {direction}");
    }
}