using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestsSystem;

public class Weapons_Interactive : MonoBehaviour
{
        private void OnInteractive()
        {
            // Check for quest existing in active quests
            if (QuestsManager.Instance.IsQuestActive(QuestsNames.MilitaryCampaign))
            {
                // gameObject.SetActive(false);
                QuestsManager.Instance.UpdateQuestProgress(QuestsNames.MilitaryCampaign);
            }
        }

        private void OnDestroy()
        {
            OnInteractive();
        }
}
