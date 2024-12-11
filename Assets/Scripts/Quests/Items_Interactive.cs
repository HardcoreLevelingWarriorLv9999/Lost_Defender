using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestsSystem;

public class Items_Interactive : MonoBehaviour
{
        private void OnInteractive()
        {
            // Check for quest existing in active quests
            if (QuestsManager.Instance.IsQuestActive(QuestsNames.PlaneCrash))
            {
                // gameObject.SetActive(false);
                QuestsManager.Instance.UpdateQuestProgress(QuestsNames.PlaneCrash);
            }
        }

        private void OnDestroy()
        {
            OnInteractive();
        }
}
