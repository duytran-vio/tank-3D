using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetNode : Node
{
    float searchRadius;
    NodeContext context;

    public FindTargetNode(float searchRadius, ref NodeContext context)
    {
        this.searchRadius = searchRadius;
        this.context = context;
    }

    public override NodeState Evaluate()
    {
        Collider[] listHit = Physics.OverlapSphere(context.manager.transform.position, searchRadius);

        _nodeState = NodeState.FAILURE;
        float currentClosest = float.MaxValue;
        foreach (Collider collider in listHit)
        {
            if (collider.TryGetComponent(out TankManager manager))
            {
                if (manager.tankInfo.id == context.manager.tankInfo.id) continue;

                _nodeState = NodeState.SUCCESS;

                float distance = Vector3.Distance(manager.transform.position, context.manager.transform.position);

                if (!context.currentTarget || currentClosest > distance)
                {
                    context.currentTarget = manager.transform;
                    currentClosest = distance;
                }
            }
        }

        return _nodeState;
    }
}
