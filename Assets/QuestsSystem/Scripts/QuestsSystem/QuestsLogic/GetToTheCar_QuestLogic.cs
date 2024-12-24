using UnityEngine;
using System;

namespace QuestsSystem
{
    public class GetToTheCar_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.GetToTheCar;

        public override string QuestTastText => "The car is waiting for you at the end of the road";

        public override void OnAccept()
        {
            GameObject.FindObjectOfType<MissionTrigger>(true).gameObject.SetActive(true);
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
            //Called when a quest is completed
        }
    }
}
























