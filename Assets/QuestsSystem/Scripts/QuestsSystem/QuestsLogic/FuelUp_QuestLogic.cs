using System;
using UnityEngine;

namespace QuestsSystem
{
    public class FuelUp_QuestLogic : QuestLogic
    {
        public static event Action OnQuestAccepted;
        public static event Action OnQuestCompleted;

        public override QuestsNames QuestName => QuestsNames.FuelUp;

        public override string QuestTastText => $"You have brought {collectedFuel}/{totalFuel} fuel canisters to the car";

        private int collectedFuel = 0;
        private int totalFuel = 3;

        public override void OnAccept()
        {
            QuestsManager.Instance.RemoveQuest(QuestsNames.DefendTheCarToTheGasStation, true);
            OnQuestAccepted?.Invoke(); // Gọi sự kiện khi nhiệm vụ được chấp nhận

            // Object_Interactive[] fuelCanisters = GameObject.FindObjectsOfType<Object_Interactive>(true);
            // foreach (var canister in fuelCanisters)
            // {
            //     canister.gameObject.SetActive(true);
            // }
        }

        public override void Logic()
        {
            //The logic that works all the time while the quest is active
        }

        public override void Progress()
        {
            collectedFuel++;
            if (collectedFuel == totalFuel)
            {
                Complete();
            }
        }

        public void DeliverFuelToCar()
        {
            // Logic để thả bình xăng vào xe
            // Ví dụ: kiểm tra người chơi đang đứng gần xe và thả bình xăng vào xe
            if (IsPlayerNearCar())
            {
                collectedFuel++;
                if (collectedFuel == totalFuel)
                {
                    Complete();
                }
            }
        }

        private bool IsPlayerNearCar()
        {
            // Kiểm tra vị trí của người chơi và xe
            // Return true nếu người chơi đang ở gần xe
            return true; // Thay bằng logic kiểm tra thực tế
        }

        public override void OnComplete()
        {
            QuestsManager.Instance.AddQuest(QuestsNames.ProtectTheCarToTheEndPoint);
            OnQuestCompleted?.Invoke(); // Gọi sự kiện khi nhiệm vụ hoàn thành
        }
    }
}
