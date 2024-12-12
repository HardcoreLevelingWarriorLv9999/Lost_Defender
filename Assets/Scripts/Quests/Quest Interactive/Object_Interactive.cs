using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestsSystem;

namespace QuestGameSample
{
    public class Object_Interactive : MonoBehaviour
    {
        private void OnInteractive()
        {
            // Check for quest existing in active quests
            if (QuestsManager.Instance.IsQuestActive(QuestsNames.ExploringTheNeighborhood))
            {
                // gameObject.SetActive(false);
                QuestsManager.Instance.UpdateQuestProgress(QuestsNames.ExploringTheNeighborhood);
            }
        }

        private void OnDestroy()
        {
            OnInteractive();
        }
    }
}
