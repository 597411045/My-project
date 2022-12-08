using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

static class NameMap
{
    public static string PanelLogin = "PanelLogin";
    public static string PanelRegister = "PanelRegister";
    public static string PanelStart = "PanelStart";
    public static string PanelServerList = "PanelServerList";
    public static string PanelCharacter = "PanelCharacter";
    public static string PanelSelectCharacter = "PanelSelectCharacter";
    public static string PanelCharacterInfo = "PanelCharacterInfo";
    public static string PanelNotification = "PanelNotification";
    public static string PanelEquipmnemt = "PanelEquipmnemt";
    public static string PanelInventory = "PanelInventory";
    public static string PanelItemDetail = "PanelItemDetail";
    public static string PanelEquipDetail = "PanelEquipDetail";
    public static string PanelMenu = "PanelMenu";
    public static string PanelQuest = "PanelQuest";
    public static string PanelSkill = "PanelSkill";
    public static string PanelInteractive = "PanelInteractive";
    public static string PanelNPCQuest = "PanelNPCQuest";

    public static string PanelFade = "PanelFade";
    public static string PanelShow = "PanelShow";
    public static string PanelLeftSlideIn = "PanelLeftSlideIn";
    public static string PanelLeftSlideOut = "PanelLeftSlideOut";
    public static string PanelCharacterInfoOut = "PanelCharacterInfoOut";
    public static string PanelCharacterInfoIn = "PanelCharacterInfoIn";

    public static string InputUserName = "InputUserName";
    public static string InputPassword = "InputPassword";

    public static string AniTurnLeft = "TurnLeft";
    public static string AniTurnRight = "TurnRight";
}



public class CustomModel
{
    public GameObject Model;
    public GameObject PositionInPanelSelectCharacter;

    public CustomModel(GameObject a)
    {
        Model = a;
        PositionInPanelSelectCharacter = Model.transform.parent.gameObject;
    }

    public void ChangeLayer(int l)
    {
        MyUtil.ChangeLayerIncludeChildren(Model.transform, l);
    }
}