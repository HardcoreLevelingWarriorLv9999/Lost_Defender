using UnityEngine;
using System;

namespace QuestsSystem
{
    public class MilitaryCampaign_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.MilitaryCampaign;

        public override string QuestTastText => $"You have collected {collectedWeapons}/{totalWeapons} weapons and ammunition";

        private int collectedWeapons = 0;
        private int totalWeapons = 1;

        public override void OnAccept()
        {
            Weapons_Interactive[] interactiveObjects = GameObject.FindObjectsOfType<Weapons_Interactive>(true);
            foreach (var obj in interactiveObjects)
            {
                obj.gameObject.SetActive(true);
            }
        }

        public override void Logic()
        {
            // The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            collectedWeapons++;
            if (collectedWeapons == totalWeapons)
            {
                Complete();
            }
        }

        public override void OnComplete()
        {
            // Called when a quest is completed
            QuestsManager.Instance.AddQuest(QuestsNames.PlaneCrash);
        }
    }
}












