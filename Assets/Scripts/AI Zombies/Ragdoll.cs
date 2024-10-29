using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();
    }

    // bật ragdoll
    public void DeactivateRagdoll()
    {
        foreach(var rigiBody in rigidBodies)
        {
            rigiBody.isKinematic = true;
        }
        animator.enabled = true;
    }


    // tắt ragdoll
    public void ActivateRagdoll()
    {
        foreach(var rigiBody in rigidBodies)
        {
            rigiBody.isKinematic = false;
        }
        animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigiBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigiBody.AddForce(force, ForceMode.VelocityChange);
    }
}
