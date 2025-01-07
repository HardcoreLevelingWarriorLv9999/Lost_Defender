using System.Collections;
using System.Collections.Generic;
using QuestsSystem;
using UnityEngine;

public class MissionTrigger5 : MonoBehaviour
{
    private void OnInteractive()
    {
        // Check for quest existing in active quests
        if (QuestsManager.Instance.IsQuestActive(QuestsNames.AMessage2))
        {
            // gameObject.SetActive(false);
            QuestsManager.Instance.UpdateQuestProgress(QuestsNames.AMessage2);
        }
    }

    private void OnDestroy()
    {
        OnInteractive();
    }
}
