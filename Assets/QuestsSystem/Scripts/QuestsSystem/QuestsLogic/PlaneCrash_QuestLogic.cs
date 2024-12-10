using UnityEngine;
using System;

namespace QuestsSystem
{
    public class PlaneCrash_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.PlaneCrash;

        public override string QuestTastText => $"You have collected {collectedItems}/{totalItems} final items";

        private int collectedItems = 0;
        private int totalItems = 1;

        public override void OnAccept()
        {
            Items_Interactive[] interactiveObjects = GameObject.FindObjectsOfType<Items_Interactive>(true);
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
            collectedItems++;
            if (collectedItems == totalItems)
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
