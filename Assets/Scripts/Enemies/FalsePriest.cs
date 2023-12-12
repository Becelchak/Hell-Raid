using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class FalsePriest : Enemies
{
    List<Collider2D> colliders2D = new List<Collider2D>();
    HashSet<Enemies> enemiesHash = new HashSet<Enemies>();

    private float baffRange = 30f;

    public override void Attack()
    {
        int colliders = Physics2D.OverlapBox(
            transform.position,
            new Vector2(baffRange, baffRange),
            90f,
            new ContactFilter2D().NoFilter(),
            colliders2D
        );
        foreach (var collider in colliders2D)
        {
            var enemy = collider.gameObject.GetComponent<Enemies>();
            if (enemy != null)
            {
                if (!enemiesHash.Contains(enemy) && !enemy.isBuffed)
                {
                    enemy.attackDamage += 20;
                    enemy.attackSpeed += 0.5f;
                    enemy.speed += 0.3f;
                    enemy.isBuffed = true;
                    enemiesHash.Add(enemy);
                }
                float distance = (float)
                    Math.Round(
                        (gameObject.transform.position - enemy.transform.position).sqrMagnitude
                    );
                if (distance > baffRange)
                {
                    enemy.attackDamage -= 20;
                    enemy.attackSpeed -= 0.5f;
                    enemy.speed -= 0.3f;
                    enemy.isBuffed = false;
                    enemiesHash.Remove(enemy);
                }
            }
        }
    }

    public override void Move() { }
}
