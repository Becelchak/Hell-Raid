using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;

public class Engineer : Soldier
{
    [SerializeField] private GameObject dronerPrefab;
    private GameObject drone;
    protected override void UseSkill()
    {
        drone = Instantiate(dronerPrefab);
        Destroy(drone, 40f);
    }

    void Update()
    {
        if (base.control.IsPlayerControl())
        {
            if (!base.canUseSkill)
            {
                skillTimer -= Time.deltaTime;
                if (skillTimer <= 0)
                    canUseSkill = true;
                base.skillIcon.fillAmount = 1 - skillTimer / (skillCoolDown / 100 * 100);
            }
            if (base.canUseSkill && Input.GetKeyDown(KeyCode.R))
            {
                skillTimer = skillCoolDown;
                UseSkill();
                base.canUseSkill = false;
                base.skillIcon.fillAmount = 0;
            }
        }

        // Drone control
        if (drone != null)
        {
            drone.transform.position =
                new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            var weapon = drone.transform.GetComponent<Weapon>();

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(
                mousePosition.y - transform.position.y,
                mousePosition.x - transform.position.x
            );
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            drone.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                if (weapon.timerShoot < 0)
                {
                    weapon.Shoot();
                    weapon.RestoreTimer();
                }
            }
            weapon.timerShoot -= Time.deltaTime;
        }
    }
}
