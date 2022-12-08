using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour
{
    Module_PlayerInfo module;
    public View_PlayerInfo view;
    Control_PlayerInfo control;
    public static SlotManager NPCQuestUI;

    // Start is called before the first frame update
    void Start()
    {
        module = new Module_PlayerInfo(PlayerType.NPC);
        module.Name = "NPC1";
        view = new View_PlayerInfo(PlayerType.NPC);
        control = new Control_PlayerInfo(module, view, PlayerType.NPC);
        module.Quests = new Dictionary<int, Module_QuestInfo>();
        module.Quests.Add(1001, new Module_QuestInfo("NPCQuest", 1001));
        module.Quests.Add(1002, new Module_QuestInfo("NPCQuest", 1002));
        module.Quests.Add(1003, new Module_QuestInfo("NPCQuest", 1003));

        if (NPCQuestUI != null)
        {
            NPCQuestUI = new SlotManager(MyUtil.FindOneInChildren(GameObject.Find("PanelNPCQuest").transform, "Slots").gameObject, SlotType.Quest);
        }
    }


}
