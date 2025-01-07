using UnityEngine;
using System;
using QuestGameSample;


namespace QuestsSystem
{
    public class ExploringTheNeighborhood_QuestLogic : QuestLogic
    {

        public override QuestsNames QuestName => QuestsNames.ExploringTheNeighborhood;

        public override string QuestTastText => $"Search for hidden mission items in houses, be wary of lurking zombies! ({collectedSupplies}/{totalSupplies})";

        private int collectedSupplies = 0;
        private int totalSupplies = 3;
        public override void OnAccept()
        {
            Object_Interactive[] interactiveObjects = GameObject.FindObjectsOfType<Object_Interactive>(true);
            foreach (var obj in interactiveObjects)
            {
                obj.gameObject.SetActive(true);
            }
        }


        public override void Logic()
        {
            //The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            collectedSupplies++;
            if (collectedSupplies == totalSupplies)
            {
                Complete();
            }
        }

        public override void OnComplete()
        {
            //Called when a quest is completed
            QuestsManager.Instance.AddQuest(QuestsNames.AMessage1);
        }
    }
}
