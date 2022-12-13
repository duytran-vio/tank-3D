using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootNode : Node
{
    NodeContext nodeContext;
    float fireAfterTime;

    public ShootNode(float fireAfterTime, NodeContext nodeContext)
    {
        this.fireAfterTime = fireAfterTime;
        this.nodeContext = nodeContext;
        nodeContext.timeSinceLastFire = fireAfterTime;
    }

    public override NodeState Evaluate()
    {
        if (nodeContext.timeSinceLastFire < fireAfterTime)
        {
            nodeContext.timeSinceLastFire += Time.deltaTime;
            _nodeState = NodeState.FAILURE;
        }
        else
        {
            nodeContext.manager.Fire(nodeContext.manager.tankInfo.turretAngle);
            nodeContext.timeSinceLastFire = 0f;
            _nodeState = NodeState.SUCCESS;
        }

        return _nodeState;
    }
}
