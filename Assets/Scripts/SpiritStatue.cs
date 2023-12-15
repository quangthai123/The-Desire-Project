using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritStatue : MonoBehaviour
{
    public int statueNum;
    [SerializeField] int extinctForThisSkill;
    private Player player;
    [SerializeField] private GameObject Eimage;
    [SerializeField] private GameObject levelupUI;
    [SerializeField] private GameObject finishedUI;
    [SerializeField] private GameObject notEnoughMoneyUI;
    [SerializeField] private GameObject learnedUI;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update()
    {
        if (Eimage.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.playerStats.HealingAll();
                if (player.playerStats.skillsChecker[statueNum]==0)
                    levelupUI.SetActive(true);
                else
                    finishedUI.SetActive(true);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Eimage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Eimage.SetActive(false);
        }
    }
    public void CloseLevelupUI()
    {
        levelupUI.SetActive(false);
    }
    public void CloseFinishUI() 
    { 
        finishedUI.SetActive(false);
    }
    public void CloseNotEnoughMoneyUI()
    {
        notEnoughMoneyUI.SetActive(false);
    }
    public void CloseLearnedUI()
    {
        learnedUI.SetActive(false);
    }
    public void LevelUp()
    {
        if (MoneyManager.instance.GetExtinctPoint() < extinctForThisSkill)
        {
            notEnoughMoneyUI.SetActive(true);
        }
        else
        {
            MoneyManager.instance.DecreaseExtinctPoint(extinctForThisSkill);
            PlayerPrefs.SetInt("Skill" + statueNum.ToString(), 1);
            player.playerStats.gotDamageData = false;
            levelupUI.SetActive(false);
            learnedUI.SetActive(true);
        }
    }
}
