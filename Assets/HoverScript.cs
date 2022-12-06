using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScript : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    GameObject go;
    Animator animator;

    public bool RotateMode;
    public bool RouletteMode;

    public void OnDrag(PointerEventData eventData)
    {
        if (RotateMode)
            go.transform.Rotate(Vector3.up, -eventData.delta.normalized.x);
    }

    private void Awake()
    {
        if (RotateMode)
            go = MyUtil.FindOneInChildren(GameObject.Find(NameMap.PanelCharacter).transform, "RolePosition").gameObject;
        if (RouletteMode)
        {
            go = MyUtil.FindOneInChildren(GameObject.Find(NameMap.PanelSelectCharacter).transform, "Wheel").gameObject;
            animator = go.GetComponent<Animator>();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.name.Contains("Right"))
        {
            animator.SetBool(NameMap.AniTurnLeft, true);
            Module_PlayerInfo.CharacterId = (Module_PlayerInfo.CharacterId + 4 - 1) % 4;
        }
        if (this.gameObject.name.Contains("Left"))
        {
            animator.SetBool(NameMap.AniTurnRight, true);
            Module_PlayerInfo.CharacterId = (Module_PlayerInfo.CharacterId + 1) % 4;
        }
    }
}
