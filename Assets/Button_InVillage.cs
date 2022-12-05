using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_InVillage : MonoBehaviour
{

    public void OnProfileClick()
    {
        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelCharacterInfo);
    }

    public void OnRenameClick(Button b)
    {
        PanelManagerInVillage.Instance.NotificationText.text = "Input New Name";
        PanelManagerInVillage.Instance.ChangePanel(null, NameMap.PanelNotification, () =>
        {
            RectTransform PanelRect = PanelManagerInVillage.Instance.Panels[NameMap.PanelNotification].GameObjectPanel.GetComponent<RectTransform>();
            RectTransform AlignedObjectRect = GameObject.Find("Rename").GetComponent<RectTransform>();
            PanelRect.position = AlignedObjectRect.position;
            PanelRect.anchoredPosition += new Vector2(PanelRect.rect.width / 2 + AlignedObjectRect.rect.width / 2, -PanelRect.rect.height / 2 + AlignedObjectRect.rect.height / 2);
        });
    }

    public void OnOKInPanelNotification()
    {
        GameManagerInVillage.Player.Name = PanelManagerInVillage.Instance.NotificationInput.text;
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelNotification, null);
    }

    public void OnCancelInPanelNotification()
    {
        PanelManagerInVillage.Instance.NotificationInput.GetComponentInParent<InputField>().text = "";
        PanelManagerInVillage.Instance.ChangePanel(NameMap.PanelNotification, null);
    }
    public void OnPanelClose(Button b)
    {
        PanelManagerInVillage.Instance.ChangePanel(b.gameObject.transform.parent.gameObject.name, null);
    }
    public void OnPanelOpen(string names)
    {
        foreach (string s in names.Split(','))
        {
            PanelManagerInVillage.Instance.ChangePanel(null, s);
        }
    }
    public void OnTaskClick(string names)
    {
        foreach (string s in names.Split(','))
        {
            PanelManagerInVillage.Instance.ChangePanel(null, s);
        }
    }



}


