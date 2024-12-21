using System;
using UnityEngine;

namespace QuestsSystem
{
    public class FuelUp_QuestLogic : QuestLogic
    {
        public static event Action OnQuestAccepted;
        public static event Action OnQuestCompleted;

        public override QuestsNames QuestName => QuestsNames.FuelUp;

        public override string QuestTastText => "TASK_TEXT";

        public override void OnAccept()
        {
            QuestsManager.Instance.RemoveQuest(QuestsNames.DefendTheCarToTheGasStation, true);
            OnQuestAccepted?.Invoke(); // Gọi sự kiện khi nhiệm vụ được chấp nhận
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
            QuestsManager.Instance.AddQuest(QuestsNames.ProtectTheCarToTheEndPoint);
            OnQuestCompleted?.Invoke(); // Gọi sự kiện khi nhiệm vụ hoàn thành
        }
    }
}
