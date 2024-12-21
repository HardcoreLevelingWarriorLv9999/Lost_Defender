using System.Collections;
using System.Collections.Generic;
using QuestsSystem;
using UnityEngine;

public class Gas_Interactive : MonoBehaviour
{
        private void OnInteractive()
        {
            // Check for quest existing in active quests
            if (QuestsManager.Instance.IsQuestActive(QuestsNames.FuelUp))
            {
                // gameObject.SetActive(false);
                QuestsManager.Instance.UpdateQuestProgress(QuestsNames.FuelUp);
            }
        }

        private void OnDestroy()
        {
            OnInteractive();
        }
}
