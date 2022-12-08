using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDragScript : MonoBehaviour, IPointerClickHandler
{
    public static SkillInfo SelectedSkill;
    public Image profile;
    public Text name;
    public Text description;
    public Text hintText;
    public Button UpgradeButton;

    private SkillInfo skill;

    public SkillInfo Skill
    {
        get => skill;
        set
        {
            skill = value;
            profile.sprite = Resources.Load<Sprite>(skill.Profile);
            name.text = "";
            description.text = "";
            hintText.text = "Select a Skill";
            UpgradeButton.interactable = false;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        SelectedSkill = skill;
        name.text = skill.Name.ToString();
        description.text = skill.Description.ToString()
                .Replace("<atk>", skill.Atk.ToString())
                  .Replace("<life>", skill.Life.ToString())+"     Level:"+skill.Level.ToString();
        if (GameManagerInVillage.PlayerControl.module.Coin > skill.Price)
        {
            hintText.text = "Upgrade";
            UpgradeButton.interactable = true;
        }
        else
        {
            hintText.text = "No Enough Coin";
            UpgradeButton.interactable = false;
        }
    }

}
