using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidBodies;
    Animator animator;
    Collider mainCollider; // Collider chính của đối tượng
    Rigidbody mainRigidbody; //Rigidbody chính của đối tượng
    void Start()
    {
        rigidBodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();

        DeactivateRagdoll();


        mainCollider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();
        mainRigidbody.isKinematic = false;
        // Tìm tất cả collider con của đối tượng
        Collider[] bodyPartColliders = GetComponentsInChildren<Collider>();

        foreach (Collider bodyPartCollider in bodyPartColliders)
        {
            if (bodyPartCollider != mainCollider)
            {
                // Bỏ qua va chạm giữa collider chính và các collider con
                Physics.IgnoreCollision(mainCollider, bodyPartCollider);
            }
        }
    }

    // Bật ragdoll
    public void DeactivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = true;
        }
        animator.enabled = true;
    }

    // Tắt ragdoll
    public void ActivateRagdoll()
    {
        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = false;
        }
        animator.enabled = false;
    }

    public void ApplyForce(Vector3 force)
    {
        var rigidBody = animator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigidBody.AddForce(force, ForceMode.VelocityChange);
    }
}
