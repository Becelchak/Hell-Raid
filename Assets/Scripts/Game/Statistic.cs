using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class Statistic : MonoBehaviour
{
    [SerializeField]
    private Secundomer secundomer;

    [SerializeField]
    private TextMeshProUGUI levelTime;

    [SerializeField]
    private TextMeshProUGUI totalTime;

    [SerializeField]
    private TextMeshProUGUI totalLevelKills;

    [SerializeField]
    private TextMeshProUGUI totalKills;

    public void LoadStatistic()
    {
        levelTime.text = secundomer.DisplayTime(
            secundomer.EndLevelTimer - secundomer.StartLevelTimer
        );
        totalTime.text = secundomer.DisplayTime(secundomer.Timer);
        totalLevelKills.text = (
            Enemies.EnemiesDeath - YandexGame.savesData.EnemiesDeath
        ).ToString();
        totalKills.text = Enemies.EnemiesDeath.ToString();
    }
}
