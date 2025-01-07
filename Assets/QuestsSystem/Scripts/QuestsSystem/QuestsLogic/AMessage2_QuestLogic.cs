using UnityEngine;
using System;

namespace QuestsSystem
{
    public class AMessage2_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.AMessage2;

        public override string QuestTastText => $"Continue explore. ({collectedMessage}/{totalMessage})";

        private int collectedMessage = 0;
        private int totalMessage = 1;

        public override void OnAccept()
        {
            MissionTrigger5[] interactiveObjects = GameObject.FindObjectsOfType<MissionTrigger5>(true);
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
            QuestsManager.Instance.AddQuest(QuestsNames.MilitaryCampaign);
        }
    }
}
























