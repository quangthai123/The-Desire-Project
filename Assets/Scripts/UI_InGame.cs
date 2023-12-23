using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    private PlayerStats playerStats;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Image[] flaskUI;
    [SerializeField] private Image[] emptyFlaskUI;
    [SerializeField] private TextMeshProUGUI extinctPointText;

    [SerializeField] private Image dashImage;
    [SerializeField] private GameObject skill1;
    [SerializeField] private GameObject skill2;
    [SerializeField] private GameObject skill3;
    [SerializeField] private GameObject skill4;
    [SerializeField] private GameObject skill5;

    [SerializeField] private TextMeshProUGUI currentSouls;
    [SerializeField] private float dashCooldown;
    private Player player;
    [Header("Boss Health UI")]
    [SerializeField] private EnemyStats bossStats;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private Boss bossRef;
    [SerializeField] private GameObject bossHealthUI;

   
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        dashCooldown = player.dashCooldown;
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateManaUI();
        UpdateFlaskUI();
        UpdateExtinctPointText();
        UpdateSkill();
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UpdateBossHealthUI();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("1");
            SetCooldownOf(dashImage);
        }
        CheckCooldownOf(dashImage,dashCooldown);
        CheckCooldownOf(dashImage, dashCooldown);
    }
    private void UpdateBossHealthUI()
    {
        if (bossRef.startBossCombat) bossHealthUI.SetActive(true);
        if (!bossRef.colTrigger) bossHealthUI.SetActive(false);
        bossHealthSlider.maxValue = bossStats.maxHealth.GetValue();
        bossHealthSlider.value = bossStats.currentHealth;
    }
    private void UpdateHealthUI()
    {
        healthSlider.maxValue = playerStats.maxHealth.GetValue();
        healthSlider.value = playerStats.currentHealth;
    }
    private void UpdateManaUI()
    {
        manaSlider.maxValue = playerStats.maxMana.GetValue();
        manaSlider.value = playerStats.currentMana;
    }
    private void UpdateFlaskUI()
    {
        for (int i = 0; i < playerStats.currentFlask; i++)
        {
            flaskUI[i].enabled = true;
        }
        for (int i = playerStats.currentFlask; i < flaskUI.Length; i++)
        {
            flaskUI[i].enabled = false;
        }
        for (int i = 0; i < playerStats.currentMaxFlask.GetValue(); i++)
        {
            emptyFlaskUI[i].enabled = true;
        }
        for (int i = playerStats.currentMaxFlask.GetValue(); i < playerStats.maxFlask; i++)
        {
            emptyFlaskUI[i].enabled = false;
        }
    }
    private void UpdateExtinctPointText()
    {
        extinctPointText.text = PlayerPrefs.GetInt("extinct") + "";
    }

    private void SetCooldownOf(Image _image)
    {
        

        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }
    public void CheckCooldownOf(Image _image, float _cooldown)
    {
        if(_image.fillAmount>0)
        if (_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }

    public void UpdateSkill()
    {
        if (playerStats.skillsChecker[0] == 1)
        {
            skill1.SetActive(true);
        }
        else skill1.SetActive(false);
        if (playerStats.skillsChecker[1] == 1)
        {
            skill2.SetActive(true);
        }
        else skill2.SetActive(false);
        if (playerStats.skillsChecker[2] == 1)
        {
            skill3.SetActive(true);
        }
        else skill3.SetActive(false);
        if (playerStats.skillsChecker[3] == 1)
        {
            skill4.SetActive(true);
        }
        else skill4.SetActive(false);
        if (playerStats.skillsChecker[4] == 1)
        {
            skill5.SetActive(true);
        }
        else skill5.SetActive(false);
    }
}
