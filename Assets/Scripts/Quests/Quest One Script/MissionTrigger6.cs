using System.Collections;
using System.Collections.Generic;
using QuestsSystem;
using UnityEngine;

public class MissionTrigger6 : MonoBehaviour
{
    private void OnInteractive()
    {
        // Check for quest existing in active quests
        if (QuestsManager.Instance.IsQuestActive(QuestsNames.TakeTheMission))
        {
            // gameObject.SetActive(false);
            QuestsManager.Instance.UpdateQuestProgress(QuestsNames.TakeTheMission);
        }
    }

    private void OnDestroy()
    {
        OnInteractive();
    }
}
