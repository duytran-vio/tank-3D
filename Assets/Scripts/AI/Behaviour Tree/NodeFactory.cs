using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NodeFactory
{
    NodeContext _context;

    public NodeFactory(ref NodeContext context)
    {
        _context = context;
    }

    public Node Selector(List<Node> nodes)
    {
        return new Selector(nodes);
    }

    public Node Sequence(List<Node> nodes)
    {
        return new Sequence(nodes);
    }

    public Node Inverter(Node node)
    {
        return new Inverter(node);
    }

    public Node ChaseNode()
    {
        return new ChaseNode(ref _context);
    }

    public Node FindTargetNode(float searchRadius)
    {
        return new FindTargetNode(searchRadius, ref _context);
    }
}

[System.Serializable]
public class NodeContext
{
    public NavMeshAgent agent;
    public TankManager manager;
    public Transform currentTarget;
}
