using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    NodeContext context;
    NavMeshPath path;

    public ChaseNode(ref NodeContext context)
    {
        this.context = context;
        path = new NavMeshPath();
    }

    public override NodeState Evaluate()
    {
        if (context.currentTarget == null)
        {
            _nodeState = NodeState.FAILURE;
        }
        else
        {
            NavMesh.CalculatePath(context.manager.transform.position, context.currentTarget.position, NavMesh.AllAreas, path);

            Debug.DrawLine(context.manager.tankInfo.position + Vector3.up, path.corners[1] + Vector3.up, Color.red);
            Vector3 direction = path.corners[1] - context.manager.tankInfo.position;
            direction = Vector3.ClampMagnitude(direction, InputManager.Instance.speed * Time.fixedDeltaTime);

            Debug.DrawRay(context.manager.tankInfo.position + Vector3.up, direction, Color.blue);

            context.manager.tankInfo.position = direction + context.manager.tankInfo.position;
            _nodeState = NodeState.RUNNING;
        }

        return _nodeState;
    }
}
