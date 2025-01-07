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
            GameObject.FindObjectOfType<MissionTrigger5>(true).gameObject.SetActive(true);
            GameObject.FindObjectOfType<Letter>(true).gameObject.SetActive(true);
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
























