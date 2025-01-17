using UnityEngine;
using System;

namespace QuestsSystem
{
    public class DefendTheCarToTheGasStation_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.DefendTheCarToTheGasStation;

        public override string QuestTastText => "Protect the car till the gas station!";

        public override void OnAccept()
        {
            QuestsManager.Instance.RemoveQuest(QuestsNames.GetToTheCar, true);
            QuestsManager.Instance.AddQuest(QuestsNames.FollowAndProtectTheCarFromZombies);
            GameObject.FindObjectOfType<CarWaypoint>(true).gameObject.SetActive(true);
            GameObject.FindObjectOfType<MissionTrigger2>(true).gameObject.SetActive(true);
        }

        public override void Logic()
        {
            //The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            //Called when there is logic for progress (Call from any other entity)
        }

        public override void OnComplete()
        {

        }
    }
}
























