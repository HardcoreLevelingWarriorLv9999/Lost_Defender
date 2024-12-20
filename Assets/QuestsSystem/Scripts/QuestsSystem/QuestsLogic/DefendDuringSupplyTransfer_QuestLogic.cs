using UnityEngine;
using System;

namespace QuestsSystem
{
    public class DefendDuringSupplyTransfer_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.DefendDuringSupplyTransfer;

        public override string QuestTastText => "TASK_TEXT";

        public override void OnAccept()
        {
            //Called when a quest is accepted
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
























