using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;

public class Squad_logic : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] [CanBeNull] private UiSoldierHud hud;
    private List<GameObject> units = new List<GameObject>();
    void Start()
    {
        for(var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            units.Add(child);
        }
    }

    void Update()
    {

    }

    public void AddNewSoldier()
    {
        // If squad full -> do nothing
        if(units.Count >= 4) return;
        var lastSoldier = units[^1];
        var newSoldier = Instantiate(lastSoldier);
        
        // Spawn new soldier behind squad and set AI control
        newSoldier.transform.position = new Vector3(newSoldier.transform.position.x - 5,newSoldier.transform.position.y + 1, newSoldier.transform.position.z);
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
        if(units.Count <= 1 ) return;
        var firstSoldier = units[0];
        var secondSoldier = units[1];

        // Delegate control on unit to player
        secondSoldier.GetComponent<Soldier_control>().SetControlPlayer();
        secondSoldier.GetComponent<Soldier_control>().SetTarget(null);
        secondSoldier.tag = "Player";

        units.Remove(firstSoldier);
        Destroy(firstSoldier);
        hud.RefreshSquad(units);

        camera.GetComponent<CinemachineVirtualCamera>().Follow = secondSoldier.transform;
    }
}
