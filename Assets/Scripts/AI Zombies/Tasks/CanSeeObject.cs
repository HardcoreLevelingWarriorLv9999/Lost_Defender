using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class CanSeeObject : Conditional
{
    public float fieldOfViewAngle;
    public string targetTag;
    public float sightDistance = 15f;
    public SharedTransform target;
    private Transform[] possibleTargets;
    [SerializeField] private bool isPursuing = false;

    public override void OnAwake()
    {
        var targets = GameObject.FindGameObjectsWithTag(targetTag);
        possibleTargets = new Transform[targets.Length];
        for (int i = 0; i < targets.Length; ++i)
        {
            possibleTargets[i] = targets[i].transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (isPursuing && target.Value != null)
        {
            return TaskStatus.Success;
        }

        for (int i = 0; i < possibleTargets.Length; ++i)
        {
            if (WithinSight(possibleTargets[i], fieldOfViewAngle, sightDistance))
            {
                target.Value = possibleTargets[i];
                isPursuing = true;
                return TaskStatus.Success;
            }
        }

        return TaskStatus.Failure;
    }

    public bool WithinSight(Transform targetTransform, float fieldOfViewAngle, float sightDistance)
    {
        Vector3 direction = targetTransform.position - transform.position;
        return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle && direction.magnitude < sightDistance;
    }
}
