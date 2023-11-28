using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Peasant : Enemies
{
    public override void Attack()
    {
        base.Attack();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerHitBox"))
        {
            Attack();
        }
    }
}
