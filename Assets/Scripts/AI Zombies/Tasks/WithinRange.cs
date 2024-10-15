using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class WithinRange : Conditional
{
    public float attackRange = 1.5f; // Phạm vi để bắt đầu tấn công
    public string targetTag;         // Tag của mục tiêu
    public SharedTransform target;   // Biến mục tiêu được gán khi tìm thấy mục tiêu
    private Transform[] possibleTargets;

    public override void OnAwake()
    {
        // Cache tất cả các transform có tag là targetTag
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        possibleTargets = new Transform[targets.Length];
        for (int i = 0; i < targets.Length; ++i)
        {
            possibleTargets[i] = targets[i].transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        // Kiểm tra nếu mục tiêu trong phạm vi tấn công
        for (int i = 0; i < possibleTargets.Length; ++i)
        {
            if (Vector3.Distance(transform.position, possibleTargets[i].position) <= attackRange)
            {
                Debug.Log("Mục tiêu đang trong phạm vi tấn công!");
                target.Value = possibleTargets[i];
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failure;
    }
}
