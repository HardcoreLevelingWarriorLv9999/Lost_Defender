using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JUTPS.AI
{
    public class JUPathFinder
    {
        /// <summary>
        /// Calculates the path from the source position to a target position. It is necessary to bake NavMesh in the scene.
        /// </summary>
        /// <param name="SourcePosition">Source position</param>
        /// <param name="TargetPosition">Target position</param>
        /// <returns></returns>
        public static Vector3[] CalculatePath(Vector3 SourcePosition, Vector3 TargetPosition, int NavmeshArea = 1)
        {
            // Check NavMesh Existence for Source Position
            NavMeshHit hitNv;
            NavMesh.SamplePosition(SourcePosition, out hitNv, 100, NavmeshArea);
            if (!hitNv.hit)
            {
                Debug.LogWarning("Source position is not on the NavMesh: " + SourcePosition);
                return new Vector3[0] { };
            }

            // Check NavMesh Existence for Target Position
            NavMeshHit hitEnd;
            NavMesh.SamplePosition(TargetPosition, out hitEnd, 100, NavmeshArea);
            if (!hitEnd.hit)
            {
                Debug.LogWarning("Target position is not on the NavMesh: " + TargetPosition);
                return new Vector3[0] { };
            }

            // Calculate path
            NavMeshPath navmesh_path = new NavMeshPath();
            NavMesh.CalculatePath(hitNv.position, hitEnd.position, NavmeshArea, navmesh_path);

            // Handle incomplete paths
            if (navmesh_path.status != NavMeshPathStatus.PathComplete)
            {
                if (NavMesh.FindClosestEdge(TargetPosition, out NavMeshHit closestEdge, NavmeshArea))
                {
                    NavMesh.CalculatePath(SourcePosition, closestEdge.position, NavmeshArea, navmesh_path);
                }
            }

            // Return the calculated path
            return navmesh_path.corners;
        }

        /// <summary>
        /// Calculates the path from the source position to a target position. It is necessary to bake NavMesh in the scene.
        /// </summary>
        /// <param name="SourcePosition">Source position</param>
        /// <param name="TargetPosition">Target position</param>
        /// <returns></returns>
        public static Vector3[] CalculatePath(Transform SourcePosition, Transform TargetPosition)
        {
            // Calculate path
            NavMeshPath navmesh_path = new NavMeshPath();
            NavMesh.CalculatePath(SourcePosition.position, TargetPosition.position, NavMesh.AllAreas, navmesh_path);

            // Handle incomplete paths
            if (navmesh_path.status == NavMeshPathStatus.PathInvalid)
            {
                NavMeshHit hit;
                NavMesh.FindClosestEdge(SourcePosition.position, out hit, NavMesh.AllAreas);
                NavMesh.CalculatePath(hit.position, TargetPosition.position, NavMesh.AllAreas, navmesh_path);
            }

            // Return the calculated path
            return navmesh_path.corners;
        }

        /// <summary>
        /// Draw the path by lines
        /// </summary>
        /// <param name="path">path to draw</param>
        public static void VisualizePath(Vector3[] path)
        {
            Color color = Color.white;
            color.a = 0.2f;
            for (int i = 0; i < path.Length - 1; i++)
            {
                Debug.DrawLine(path[i], path[i] + Vector3.up * 0.1f, Color.red);
                Debug.DrawLine(path[i], path[i + 1], color);
            }
        }

        /// <summary>
        /// Get the closest walkable point on the NavMesh to the target position
        /// </summary>
        /// <param name="targetPosition">Target position</param>
        /// <param name="offsetDirection">Offset direction</param>
        /// <returns></returns>
        public static Vector3 GetClosestWalkablePoint(Vector3 targetPosition, float offsetDirection = 0.2f)
        {
            Vector3 position = Vector3.zero;

            NavMeshHit hit;
            NavMesh.SamplePosition(targetPosition, out hit, 100, NavMesh.AllAreas);

            Vector3 dir = (targetPosition - hit.position).normalized;
            position = hit.position - dir * offsetDirection;

            return position;
        }
    }
}
