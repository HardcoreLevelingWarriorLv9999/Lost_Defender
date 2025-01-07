using UnityEngine;
using System;

namespace QuestsSystem
{
    public class PlaneCrash_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.PlaneCrash;

        public override string QuestTastText => $"Gather the final items and steer clear of zombies with special abilities. ({collectedItems}/{totalItems})";

        private int collectedItems = 0;
        private int totalItems = 3;

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
            QuestsManager.Instance.AddQuest(QuestsNames.AMessage2);
        }
    }
}
