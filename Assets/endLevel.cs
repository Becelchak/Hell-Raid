using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLevel : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            var winPanel = GameObject.Find("Win").GetComponent<CanvasGroup>();
            winPanel.alpha = 1f;
            winPanel.interactable = true;
            winPanel.blocksRaycasts = true;
        }
    }
}
