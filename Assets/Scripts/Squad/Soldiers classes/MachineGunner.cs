using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunner : Soldier
{
    private List<GameObject> enemies = new List<GameObject>();
    protected override void UseSkill()
    {
        foreach (var enemy in enemies)
        {
            var directionForce = transform.position  - enemy.transform.position;
            enemy.transform.Translate(-directionForce * 250 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }
}
