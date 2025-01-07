using UnityEngine;
using System;
using JUTPS.UI;

namespace QuestsSystem
{
    public class FollowAndProtectTheCarFromZombies_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.FollowAndProtectTheCarFromZombies;

        public override string QuestTastText => "Waring: The car can be damaged by bullets";

        public override void OnAccept()
        {
            GameObject.FindObjectOfType<UIVehicleHealthBar>(true).gameObject.SetActive(true);
            GameObject.FindObjectOfType<MissionTrigger7>(true).gameObject.SetActive(true);
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
























