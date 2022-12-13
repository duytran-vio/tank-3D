using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviationNode : Node
{
    NodeContext nodeContext;
    float maximumAllowedDeviation;

    public DeviationNode(NodeContext nodeContext, float maximumAllowedDeviation)
    {
        this.nodeContext = nodeContext;
        this.maximumAllowedDeviation = maximumAllowedDeviation;
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

            if (Mathf.Abs(deviation - nodeContext.manager.tankInfo.turretAngle) <= maximumAllowedDeviation)
            {
                _nodeState = NodeState.SUCCESS;
            }
            else
            {
                _nodeState = NodeState.FAILURE;
            }
        }

        return _nodeState;
    }
}
