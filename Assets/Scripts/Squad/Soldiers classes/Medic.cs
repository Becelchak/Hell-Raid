using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medic : Soldier
{
    protected override void UseSkill()
    {
        var squad = GetSquad();
        var lowHealthSoldier = squad.FindLowHealthSoldier();
        lowHealthSoldier.ReturnFullHealth();
    }
}
