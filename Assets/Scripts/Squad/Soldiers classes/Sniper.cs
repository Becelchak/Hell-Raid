using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Soldier
{
    protected override void UseSkill()
    {
        var rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddForce(Vector2.left * 6000);
    }
}
