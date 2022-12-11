using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    public Module_PlayerInfo module;
    public View_PlayerInfo view;
    public Control_PlayerInfo control;
    public static SlotManager NPCQuestUI;
    public int Result;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        module = new Module_PlayerInfo(PlayerType.NPC);
        module.Name = this.gameObject.name;
        module.Profile = "NPC";
        view = new View_PlayerInfo(PlayerType.NPC);
        control = new Control_PlayerInfo(module, view, PlayerType.NPC);

        module.dialogList = Module_Dialogue.DialogueDic;
        module.dialogueIndex = 1001;

        if (NPCQuestUI == null)
        {
            NPCQuestUI = new SlotManager(MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelNPCQuest].GameObjectPanel.transform, "Slots").gameObject, SlotType.Quest);
        }
    }
}
