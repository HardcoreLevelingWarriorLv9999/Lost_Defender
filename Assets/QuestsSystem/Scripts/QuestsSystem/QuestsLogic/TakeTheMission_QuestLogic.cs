using UnityEngine;
using System;

namespace QuestsSystem
{
    public class TakeTheMission_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.TakeTheMission;

        public override string QuestTastText => $"Read the mission. ({readMission}/{totalMission})";

        private int readMission = 0;
        private int totalMission = 1;

        public override void OnAccept()
        {
            GameObject.FindObjectOfType<MissionTrigger6>(true).gameObject.SetActive(true);
            GameObject.FindObjectOfType<Letter>(true).gameObject.SetActive(true);
        }

        public override void Logic()
        {
            //The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            readMission++;
            if (readMission == totalMission)
            {
                Complete();
            }
        }

        public override void OnComplete()
        {
            QuestsManager.Instance.AddQuest(QuestsNames.GetToTheCar);
        }
    }
}