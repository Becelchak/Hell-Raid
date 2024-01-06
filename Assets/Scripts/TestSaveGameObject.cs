using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaveGameObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
