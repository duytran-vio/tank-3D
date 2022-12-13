using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtTargetNode : Node
{
    NodeContext nodeContext;

    public AimAtTargetNode(NodeContext nodeContext)
    {
        this.nodeContext = nodeContext;
    }

    public override NodeState Evaluate()
    {
        if (nodeContext.currentTarget == null)
        {
            _nodeState = NodeState.FAILURE;
        }
        else
        {
            Vector3 directionToTarget = nodeContext.currentTarget.transform.position - nodeContext.manager.transform.position;
            float deviation = Quaternion.LookRotation(directionToTarget, nodeContext.manager.transform.up).eulerAngles.y;

            nodeContext.manager.tankInfo.turretAngle = deviation;
            _nodeState = NodeState.SUCCESS;
        }

        return _nodeState;
    }
}
