using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    float range;
    NodeContext nodeContext;

    public RangeNode(float range, ref NodeContext nodeContext)
    {
        this.range = range;
        this.nodeContext = nodeContext;
    }

    public override NodeState Evaluate()
    {
        if (!nodeContext.manager.transform || !nodeContext.currentTarget) _nodeState = NodeState.FAILURE;
        else
        {
            float distance = Vector3.Distance(nodeContext.manager.transform.position, nodeContext.currentTarget.position);
            _nodeState = distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
        }
        return _nodeState;
    }
}
