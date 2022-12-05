using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillDragScript : MonoBehaviour, IPointerClickHandler
{

    public Image profile;
    public Text name;
    public Text description;

    private SkillInfo skill;

    public SkillInfo Skill
    {
        get => skill;
        set
        {
            skill = value;
            profile.sprite = Resources.Load<Sprite>(skill.Profile);
            name.text = skill.Name.ToString();
            description.text = skill.Description.ToString();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(123);
    }

}
