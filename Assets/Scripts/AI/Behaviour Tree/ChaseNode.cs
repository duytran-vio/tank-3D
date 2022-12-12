using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    NodeContext context;

    public ChaseNode(ref NodeContext context)
    {
        this.context = context;
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
            if (distance > 4f)
            {
                //context.agent.isStopped = false;
                //context.agent.destination = context.currentTarget.position;
                context.manager.tankInfo.movementInput = context.currentTarget.position;
                _nodeState = NodeState.RUNNING;
            }
            else
            {
                //context.agent.isStopped = true;
                context.manager.tankInfo.movementInput = context.manager.transform.position;
                _nodeState = NodeState.SUCCESS;
            }
        }

        return _nodeState;
    }
}
