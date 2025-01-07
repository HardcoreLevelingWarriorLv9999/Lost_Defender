using UnityEngine;
using System;

namespace QuestsSystem
{
    public class MilitaryCampaign_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.MilitaryCampaign;

        public override string QuestTastText => $"Collect weapons and ammunition, watch out for zombie ambushes! ({collectedWeapons}/{totalWeapons})";

        private int collectedWeapons = 0;
        private int totalWeapons = 2;

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
            GameObject.FindObjectOfType<ReachLocation>(true).gameObject.SetActive(true);
            QuestsManager.Instance.AddQuest(QuestsNames.RoadToTheEndPoint);
        }
    }
}












