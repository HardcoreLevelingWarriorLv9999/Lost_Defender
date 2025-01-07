using UnityEngine;
using System;

namespace QuestsSystem
{
    public class AMessage1_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.AMessage1;

        public override string QuestTastText => $"Continue explore. ({collectedMessage}/{totalMessage})";

        private int collectedMessage = 0;
        private int totalMessage = 1;

        public override void OnAccept()
        {
            MissionTrigger4[] interactiveObjects = GameObject.FindObjectsOfType<MissionTrigger4>(true);
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
            collectedMessage++;
            if (collectedMessage == totalMessage)
            {
                Complete();
            }
        }

        public override void OnComplete()
        {
            QuestsManager.Instance.AddQuest(QuestsNames.PlaneCrash);
        }
    }
}
























