using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemies
{
    Vector2 sizeSnakeCollider;

    private void Start()
    {
        sizeSnakeCollider = gameObject.GetComponent<CapsuleCollider2D>().size;
        sizeSnakeCollider.x = 1f;
        sizeSnakeCollider.y = 0.2f;
    }

    public override void Attack()
    {
        base.Attack();
    }
}
