using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_control : MonoBehaviour
{
    [Header("Move parameter")]
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool Is_Player = false;
    [SerializeField] private GameObject Target_Follow;
    private bool canFollow = true;
    private float collDownFollow = 0.1f;

    [Header("Weapon parameter")] 
    private float bullet_speed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        if(Is_Player)
        {
            // Moving
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
            }
            // Mouse control
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z += Camera.main.nearClipPlane;
            var weapon = transform.GetChild(1);

            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            weapon.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

            // Shooting
            if(Input.GetMouseButtonDown(0))
                weapon.GetComponent<Weapon>().Shoot(bullet_speed);
        }
        else
        {
            var target_pos = Target_Follow.transform.position;

            if(Input.GetKey(KeyCode.A))
                transform.position = Vector3.MoveTowards(transform.position, target_pos, -Speed * Time.deltaTime);
            else if(canFollow)
                transform.position = Vector3.MoveTowards(transform.position, target_pos, Speed * Time.deltaTime);
            else
            {
                collDownFollow -= Time.deltaTime;
            }
        }
    }

    public void SetTarget(GameObject target)
    {
        Target_Follow = target;
    }

    public void SetControlAi()
    {
        Is_Player = false;
    }

    public void SetControlPlayer()
    {
        Is_Player = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canFollow = false;
        collDownFollow = 0.5f;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canFollow = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        canFollow = false;
        collDownFollow = 0.5f;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        canFollow = true;
    }
}
