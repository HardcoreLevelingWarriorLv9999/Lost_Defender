using System.Collections;
using System.Collections.Generic;
using QuestsSystem;
using UnityEngine;

public class MissionTrigger4 : MonoBehaviour
{
    private void OnInteractive()
    {
        // Check for quest existing in active quests
        if (QuestsManager.Instance.IsQuestActive(QuestsNames.AMessage1))
        {
            // gameObject.SetActive(false);
            QuestsManager.Instance.UpdateQuestProgress(QuestsNames.AMessage1);
        }
    }

    private void OnDestroy()
    {
        OnInteractive();
    }
}
