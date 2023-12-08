using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenadier : Soldier
{
    protected override void UseSkill()
    {
        var positionOne = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 4);
        var positionTwo = new Vector2();
        var positionThree = new Vector2();

        positionTwo.x += (float)Math.Sin(positionOne.x) * 3;
        positionTwo.y += (float)Math.Cos(positionOne.y) * 3;

        positionThree.x -= (float)Math.Sin(positionOne.x) * 4;
        positionThree.y -= (float)Math.Cos(positionOne.y) * 4;

        var weapon = GetComponentInChildren<Weapon>();
        weapon.ShootGrenadeLauncher(positionOne);
        weapon.ShootGrenadeLauncher(positionTwo);
        weapon.ShootGrenadeLauncher(positionThree);
    }
}
