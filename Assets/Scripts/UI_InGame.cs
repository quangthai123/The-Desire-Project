using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateManaUI();
        UpdateFlaskUI();
        UpdateExtinctPointText();

        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log("1");
            SetCooldownOf(dashImage);
        }
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
        for(int i=0; i<playerStats.currentFlask; i++)
        {
            flaskUI[i].enabled = true;
        }
        for(int i=playerStats.currentFlask; i<flaskUI.Length; i++)
        { 
            flaskUI[i].enabled = false;            
        }
        for(int i=0; i<playerStats.currentMaxFlask.GetValue(); i++)
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
        Debug.Log(1);
        if (_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }
}
