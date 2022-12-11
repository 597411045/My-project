using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript2 : NPCScript
{

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        module = new Module_PlayerInfo(PlayerType.NPC);
        module.Name = this.gameObject.name;
        module.Profile = "NPC2";
        view = new View_PlayerInfo(PlayerType.NPC);
        control = new Control_PlayerInfo(module, view, PlayerType.NPC);
        module.Quests = new Dictionary<int, Module_QuestInfo>();
        module.Quests.Add(1001, new Module_QuestInfo("NPCQuest", 1001, this));
        module.Quests.Add(1002, new Module_QuestInfo("NPCQuest", 1002, this));
        module.Quests.Add(1003, new Module_QuestInfo("NPCQuest", 1003, this));

        module.dialogList = Module_Dialogue.DialogueDic;
        module.dialogueIndex = 3001;


        if (NPCQuestUI == null)
            NPCQuestUI = new SlotManager(MyUtil.FindOneInChildren(PanelManagerInVillage.Instance.Panels[NameMap.PanelNPCQuest].GameObjectPanel.transform, "Slots").gameObject, SlotType.Quest);
        //foreach (var i in module.Quests)
        //{
        //    NPCQuestUI.TryAddQuest(i.Value);
        //}
    }


}
