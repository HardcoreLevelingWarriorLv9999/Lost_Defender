using UnityEngine;
using System;

namespace QuestsSystem
{
    public class Tutorial_QuestLogic : QuestLogic
    {
        public override QuestsNames QuestName => QuestsNames.Tutorial;

        public override string QuestTastText => $"Can you do it? ({completedSteps}/{totalSteps})";

        private int completedSteps = 0;
        private int totalSteps = 5;

        public override void OnAccept()
        {
            MissionTrigger1[] interactiveObjects = GameObject.FindObjectsOfType<MissionTrigger1>(true);
            foreach (var obj in interactiveObjects)
            {
                obj.gameObject.SetActive(true);
            }
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
