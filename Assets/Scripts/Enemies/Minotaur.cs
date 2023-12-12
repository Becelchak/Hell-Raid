using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Enemies
{
    float maxSpeed = 3f;

    public override void Attack()
    {
        base.Attack();
    }

    public override void UseSkill()
    {
        //анимация разгона
        StartCoroutine(Sprint());
    }

    private IEnumerator Sprint()
    {
        for (float i = speed; i < maxSpeed; i += 0.5f)
        {
            speed += 0.5f;
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        speed = 1;
    }
}
