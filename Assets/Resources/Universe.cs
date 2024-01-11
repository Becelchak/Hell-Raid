using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe : MonoBehaviour
{
    public List<GameObject> levels;
    private Camera mainCamera;
    private Vector2 screenBounds;
    public float choke;

    void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenBounds =
            mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (var level in levels)
        {
            LoadChildObj(level);
        }

    }

    void LoadChildObj(GameObject obj)
    {
        var objectWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - choke;
        var childsNedeed = (int)Mathf.Ceil(screenBounds.x * 2/ objectWidth);
        var clone = Instantiate(obj);
        for (var i = 0; i <= childsNedeed; i++)
        {
            var c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(objectWidth * i, obj.transform.position.y,
                obj.transform.position.z);
            c.name = obj.name + i;
        }

        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }

    void repositionChildObj(GameObject obj)
    {
        var children = obj.GetComponentsInChildren<Transform>();
        if (children.Length > 1)
        {
            var firstChild = children[1].gameObject;
            var lastChild = children[^1].gameObject;
            var halfObjectWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - choke;
            if (transform.position.x + screenBounds.x > lastChild.transform.position.x + halfObjectWidth/2)
            {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfObjectWidth * 2,
                    lastChild.transform.position.y,
                    lastChild.transform.position.z);
            }
            else if (transform.position.x - screenBounds.x < firstChild.transform.position.x - halfObjectWidth/2)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfObjectWidth * 2, 
                    firstChild.transform.position.y,
                    firstChild.transform.position.z);
            }
        }
    }

    void LateUpdate()
    {
        foreach (var obj in levels)
        {
            repositionChildObj(obj);
        }
    }
}
