using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    NodeContext context;
    NavMeshPath path;
    float stoppingDistance;

    public ChaseNode(float stoppingDistance, ref NodeContext context)
    {
        this.context = context;
        path = new NavMeshPath();
        this.stoppingDistance = stoppingDistance;
    }

    public override NodeState Evaluate()
    {
        if (context.currentTarget == null)
        {
            _nodeState = NodeState.FAILURE;
        }
        else
        {
            float distance = Vector3.Distance(context.currentTarget.position, context.agent.transform.position);
            if (distance > stoppingDistance)
            {
                //context.agent.isStopped = false;
                //context.agent.destination = context.currentTarget.position;

                NavMesh.CalculatePath(context.manager.transform.position, context.currentTarget.position, NavMesh.AllAreas, path);

                Debug.DrawLine(context.manager.transform.position, path.corners[0], Color.red);
                Vector3 direction = path.corners[1] - context.manager.transform.position;
                direction = context.manager.transform.InverseTransformVector(direction);
                direction.Normalize();
                direction.y = 0;

                context.manager.tankInfo.movementInput = direction;
                _nodeState = NodeState.RUNNING;
            }
            else
            {
                //context.agent.isStopped = true;
                //context.agent.ResetPath();
                context.manager.tankInfo.movementInput = Vector3.zero;
                _nodeState = NodeState.SUCCESS;
            }
        }

        return _nodeState;
    }
}
