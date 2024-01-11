using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class endLevel : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> soldiers;

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

    public void SaveSquad()
    {
        for (int i = 0; i < soldiers.Count; i++)
        {
            if (soldiers[i] == null)
            {
                YandexGame.savesData.hpSoldiers[i] = 0;
                continue;
            }
            YandexGame.savesData.sprites[i] = soldiers[i].GetComponent<SpriteRenderer>().sprite;
            var soldierClass = soldiers[i].GetComponent<Soldier>();
            YandexGame.savesData.hpSoldiers[i] = soldierClass.Health;
            var weapon = soldiers[i].transform.Find("Weapon").gameObject.GetComponent<Weapon>();
            YandexGame.savesData.ammoInMagazines[i] = weapon.count_magazine;
            YandexGame.savesData.tempsAmmo[i] = weapon.ammo_temp;
            YandexGame.savesData.currentsAmmo[i] = weapon.ammo_in_magazine;
        }
        YandexGame.SaveProgress();
        // print(YandexGame.savesData.level);
    }
}
