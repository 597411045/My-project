using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Attack_Script : MonoBehaviour, IPointerClickHandler
{
    static string FirstPressedButton = "";
    static string SecondPressedButton = "";

    public Action ButtonAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ButtonAction != null)
        {
            ButtonAction();
        }
    }

    private void Start()
    {
        //ButtonAction = () =>
        //{
        //    if (FirstPressedButton == "")
        //    {
        //        FirstPressedButton = this.gameObject.name.Split('_')[0];
        //    }
        //    else
        //    {
        //        if (SecondPressedButton == "")
        //        {
        //            SecondPressedButton = this.gameObject.name.Split('_')[0];
        //            LuaScript.Instance.ManualAttack(FirstPressedButton, SecondPressedButton);
        //            FirstPressedButton = "";
        //            SecondPressedButton = "";
        //        }
        //    }
        //};
    }

}
