using System;
using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        public int money = 1; // Можно задать полям значения по умолчанию
        public string newPlayerName = "Hello!";
        public bool[] openLevels = new bool[3];

        // Ваши сохранения

        private readonly int squadSize = 4;
        public int level;

        public Sprite[] sprites;
        public Soldier_control.SoldierClass[] soldierClasses;
        public int[] hpSoldiers;

        public Weapon.Weapons_Type[] weaponsTypes;
        public int[] ammoInMagazines;
        public int[] currentsAmmo;
        public int[] tempsAmmo;

        public float timerTxt;
        public float timer;
        public int EnemiesDeath;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива
            openLevels[1] = true;

            sprites = new Sprite[squadSize];
            soldierClasses = new Soldier_control.SoldierClass[squadSize];
            hpSoldiers = new int[squadSize];
            weaponsTypes = new Weapon.Weapons_Type[squadSize];
            ammoInMagazines = new int[squadSize];
            currentsAmmo = new int[squadSize];
            tempsAmmo = new int[squadSize];
        }
    }
}
