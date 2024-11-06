using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsHealthUnder : Conditional
{
    public SharedInt healthThreshold;
    protected ZombieHealth zombieHealth;

    public override void OnAwake()
    {
        zombieHealth = GetComponent<ZombieHealth>();
    }

    public override TaskStatus OnUpdate()
    {
        return zombieHealth.currentHealth < healthThreshold.Value ? TaskStatus.Success : TaskStatus.Failure;
    }
}
