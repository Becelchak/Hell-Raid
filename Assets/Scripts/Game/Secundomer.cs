using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using YG;

public class Secundomer : MonoBehaviour
{
    public TextMeshProUGUI timerTxt;

    [NonSerialized]
    public float Timer;

    [NonSerialized]
    public float StartLevelTimer;

    [NonSerialized]
    public float EndLevelTimer;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            GetLoad();
    }

    private void GetLoad()
    {
        Timer = YandexGame.savesData.timer;
        if (YandexGame.savesData.level == 1)
            Timer = 0;
        StartLevelTimer = Timer;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        timerTxt.text = DisplayTime(Timer);
    }

    public string DisplayTime(float timeToDisplay)
    {
        var t0 = (int)timeToDisplay;
        float min = Mathf.FloorToInt(timeToDisplay / 60);
        float sec = Mathf.FloorToInt(timeToDisplay % 60);
        float milliSec = (int)((timeToDisplay - t0) * 100);

        return string.Format("{0:0}:{1:00}:{2:00}", min, sec, milliSec);
    }
}
