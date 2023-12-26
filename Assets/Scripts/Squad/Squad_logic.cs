using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class Squad_logic : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    [SerializeField]
    [CanBeNull]
    private UiSoldierHud hud;
    private List<GameObject> units = new List<GameObject>();

    void Start()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            units.Add(child);
        }
    }

    void Update() { }

    public void AddNewSoldier()
    {
        // If squad full -> do nothing
        if (units.Count >= 4)
            return;
        var lastSoldier = units[^1];
        var newSoldier = Instantiate(lastSoldier);

        // Spawn new soldier behind squad and set AI control
        newSoldier.GetComponent<Soldier_control>().SetTarget(lastSoldier);
        newSoldier.GetComponent<Soldier_control>().SetControlAi();
        // Add new name and set parent -> squad
        newSoldier.name = $"Soldier_test {units.Count}";
        newSoldier.transform.parent = transform;

        lastSoldier.GetComponentInChildren<BoxCollider2D>().isTrigger = true;
        units.Add(newSoldier);
        hud.RefreshSquad();
    }

    public void DeleteFirstSoldier()
    {
        if (units.Count <= 1)
            return;
        var firstSoldier = units[0];
        var secondSoldier = units[1];

        // Delegate control on unit to player
        var control = secondSoldier.GetComponent<Soldier_control>();
        control.SetControlPlayer();
        control.SetTarget(null);
        secondSoldier.tag = "Player";
        secondSoldier.GetComponent<CapsuleCollider2D>().enabled = true;
        secondSoldier.GetComponent<SpriteRenderer>().enabled = true;
        var weapon = secondSoldier.transform.GetChild(1).gameObject;
        weapon.SetActive(true);

        units.Remove(firstSoldier);
        Destroy(firstSoldier);
        hud.RefreshSquad(units);

        camera.GetComponent<CinemachineVirtualCamera>().Follow = secondSoldier.transform;
    }

    public void DeleteSoldier(Soldier soldier)
    {
        foreach (var unit in units)
        {
            if (unit.GetComponent<Soldier>() == soldier)
            {
                if(units.Count > 1)
                {
                    var secondSoldier = units[1];

                    // Delegate control on unit to player
                    var control = secondSoldier.GetComponent<Soldier_control>();
                    control.SetControlPlayer();
                    control.SetTarget(null);
                    secondSoldier.tag = "Player";
                    secondSoldier.GetComponent<CapsuleCollider2D>().enabled = true;
                    secondSoldier.GetComponent<SpriteRenderer>().enabled = true;
                    var weapon = secondSoldier.transform.GetChild(1).gameObject;
                    weapon.SetActive(true);

                    RefreshTargetPlayer(secondSoldier);
                }

                units.Remove(unit);
                Destroy(unit,1f);
                hud.RefreshSquad(units);
            }
                
        }
    }

    public void ChangeSoldier(int number)
    {

        if(units.Count < number) return;
        var oldSoldier = units[0];
        foreach (
            var soldier in units.Where(
                soldier => soldier.GetComponent<Soldier_control>().IsPlayerControl()
            )
        )
        {
            oldSoldier = soldier;
        }
        var newSoldier = units[number];

        // Off old soldier
        var controlOldSoldier = oldSoldier.GetComponent<Soldier_control>();
        controlOldSoldier.SetControlAi();
        oldSoldier.GetComponent<CapsuleCollider2D>().enabled = false;
        oldSoldier.GetComponent<SpriteRenderer>().enabled = false;
        var oldWeapon = oldSoldier.transform.GetChild(1).gameObject;
        oldWeapon.SetActive(false);

        // Delegate control on unit to player
        var controlNewSoldier = newSoldier.GetComponent<Soldier_control>();
        controlNewSoldier.SetControlPlayer();
        controlNewSoldier.SetTarget(null);
        newSoldier.tag = "Player";
        newSoldier.GetComponent<CapsuleCollider2D>().enabled = true;
        newSoldier.GetComponent<SpriteRenderer>().enabled = true;
        var weapon = newSoldier.transform.GetChild(1).gameObject;
        weapon.SetActive(true);

        hud.RefreshSquad(units);
        camera.GetComponent<CinemachineVirtualCamera>().Follow = newSoldier.transform;

        RefreshTargetPlayer(newSoldier);

    }

    public void AddAmmoSquad()
    {
        foreach (var unit in units)
        {
            var weapon = unit.transform.GetChild(1).gameObject;
            if (!weapon.activeSelf)
            {
                weapon.SetActive(true);
                var unitWeapon = unit.GetComponentInChildren<Weapon>();
                unitWeapon.AddMagazine();
                weapon.SetActive(false);
            }
            else
            {
                var unitWeapon = unit.GetComponentInChildren<Weapon>();
                unitWeapon.AddMagazine();
            }
        }
    }

    public Soldier FindLowHealthSoldier()
    {
        var result = units[0].GetComponent<Soldier>();
        var minHealth = result.Health;
        foreach (var unit in units)
        {
            var soldier = unit.GetComponent<Soldier>();
            var tempHealth = soldier.Health;
            if (tempHealth < minHealth && soldier.Health != soldier.maxHealth)
            {
                minHealth = tempHealth;
                result = soldier;
            }
        }

        return result;
    }

    public void RefreshTargetPlayer(GameObject newSoldier)
    {
        foreach (
            var soldier in units.Where(
                soldier => !soldier.GetComponent<Soldier_control>().IsPlayerControl()
            )
        )
        {
            soldier.GetComponent<Soldier_control>().SetTarget(newSoldier);
        }
    }
}
