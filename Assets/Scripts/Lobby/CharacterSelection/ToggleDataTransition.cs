using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDataTransition : MonoBehaviour
{
    public GameObject characterIconObject;
    public GameObject characterNameObject;
    public GameObject bulletObject;
    public GameObject skillInfoObject;
    public GameObject aboutCharacterObject;

    [NonSerialized]
    public Image characterIcon;

    [NonSerialized]
    public Image bulletImage;

    [NonSerialized]
    public TextMeshProUGUI characterName = new();

    [NonSerialized]
    public TextMeshProUGUI skillInfoTxt = new();

    [NonSerialized]
    public TextMeshProUGUI aboutCharacterTxt = new();
}
