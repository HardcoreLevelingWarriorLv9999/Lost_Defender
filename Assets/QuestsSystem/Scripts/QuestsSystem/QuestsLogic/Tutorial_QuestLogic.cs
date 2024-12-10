using UnityEngine;
using System;

namespace QuestsSystem
{
    public class Tutorial_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.Tutorial;

        public override string QuestTastText => $"You have completed {completedSteps}/{totalSteps}";

        private int completedSteps = 0;
        private int totalSteps = 5;

        public override void OnAccept()
        {
            // Called when a quest is accepted
        }

        public override void Logic()
        {
            // The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            completedSteps++;

            if (completedSteps == totalSteps)
            {
                Complete();
            }
        }

        public override void OnComplete()
        {
            QuestsManager.Instance.AddQuest(QuestsNames.ExploringTheNeighborhood);
        }
    }
}
