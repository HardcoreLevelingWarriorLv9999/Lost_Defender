using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChaseToObject : Action
{
    public float speed = 0;
    public SharedTransform target;
    public float chaseRange = 10.0f; // Phạm vi để bắt đầu quay

    public override TaskStatus OnUpdate()
    {
        // Kiểm tra khoảng cách giữa hai đối tượng
        float distance = Vector3.Distance(transform.position, target.Value.position);
        if (distance <= chaseRange)
        {
            // Tính toán hướng quay chỉ theo trục X và Z
            Vector3 direction = new Vector3(target.Value.position.x - transform.position.x, 0, target.Value.position.z - transform.position.z);
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5.0f);
        }

        // Trả về trạng thái thành công nếu đã đến mục tiêu
        if (Vector3.SqrMagnitude(transform.position - target.Value.position) < 0.1f)
        {
            return TaskStatus.Success;
        }

        // Di chuyển về phía mục tiêu nhưng giữ nguyên độ cao y của đối tượng
        Vector3 targetPosition = new Vector3(target.Value.position.x, transform.position.y, target.Value.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        return TaskStatus.Running;
    }
}
