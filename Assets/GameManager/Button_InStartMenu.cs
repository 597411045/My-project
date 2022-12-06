using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_InStartMenu : MonoBehaviour
{
    Text InputUserName;
    Text InputPassword;

    private void Awake()
    {
        InputUserName = GameObject.Find(NameMap.InputUserName).GetComponent<Text>();
        InputPassword = GameObject.Find(NameMap.InputPassword).GetComponent<Text>();
    }

    private void Start()
    {

    }

    public void OnUserNameClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelStart, NameMap.PanelLogin);
    }
    public void OnServerNameClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelStart, NameMap.PanelServerList);
        PanelManager.Instance.InitPanelServerList(); ;

    }
    public void OnStartGameClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelStart, NameMap.PanelCharacter);
    }
    public void OnSubmitClick()
    {

    }
    public void OnLoginClick()
    {
        Module_PlayerInfo.LocalUsername = InputUserName.text;
        Module_PlayerInfo.LocalPassword = InputPassword.text;

        PanelManager.Instance.ChangePanel(NameMap.PanelLogin, NameMap.PanelStart);
        PanelManager.Instance.usernameInPanelStart.text = Module_PlayerInfo.LocalUsername;
    }

    public void OnRegisterClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelLogin, NameMap.PanelRegister);
    }

    public void OnCloseClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelRegister, NameMap.PanelLogin);
    }
    public void OnBackClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelServerList, NameMap.PanelStart);
        PanelManager.Instance.servernameInPanelStart.text = PanelManager.ChosenServerUnit.GetComponentInChildren<TextMeshProUGUI>().text;
    }

    public void OnChangeClick()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelCharacter, NameMap.PanelSelectCharacter);
        PanelManager.Instance.MoveRoleFromPanelCharacter();
    }

    public void BackInPanelSelectCharacter()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelSelectCharacter, NameMap.PanelCharacter);
    }

    public void OKInPanelSelectCharacter()
    {
        PanelManager.Instance.ChangePanel(NameMap.PanelSelectCharacter, NameMap.PanelCharacter);
        PanelManager.Instance.MoveRoleFromPanelSelectCharacter();
        Module_PlayerInfo.CurrentCharacterName = MyUtil.FindOneInChildren(GameObject.Find(NameMap.PanelSelectCharacter).transform, "Text").GetComponent<Text>().text;
        PanelManager.Instance.characterNameInPanelCharacter.text = Module_PlayerInfo.CurrentCharacterName;
    }
}
