using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForStartBossComBat : MonoBehaviour
{
    [SerializeField] private Boss bossRef;
    [SerializeField] private GameObject bossHealthSlider;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !bossRef.startBossCombat)
        {
            bossRef.startBossCombat = true;
            AudioManager.instance.playBgm = true;
            bossHealthSlider.SetActive(true);
            AudioManager.instance.playerSFX(24);
            AudioManager.instance.PlayBGM(7);
        }
    }
}
