using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkilltreeSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler

{
    public bool unlocked;
    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;


    private Image skillImage;

    [SerializeField] private int skillNumber;

    [SerializeField] private Color lockedSkillColor;

    public UI ui;

    private List<int> skillsChecker;

    public void OnPointerEnter(PointerEventData eventData)
    {
       ui.skillToolTip.ShowToolTip(skillDescription,skillName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillToolTip.HideToolTip();
    }

    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Start()
    {
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        skillImage = GetComponent<Image>();
        skillsChecker = playerStats.skillsChecker;
        skillImage.color = (skillsChecker[skillNumber] == 0) ? lockedSkillColor : Color.white;
        ui=GetComponentInParent<UI>();
        Debug.Log(ui);
    }


}
