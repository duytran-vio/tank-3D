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

    public Node ChaseNode(float stoppingDistance)
    {
        return new ChaseNode(stoppingDistance, ref _context);
    }

    public Node FindTargetNode(float searchRadius)
    {
        return new FindTargetNode(searchRadius, ref _context);
    }

    public Node RangeNode(float range)
    {
        return new RangeNode(range, ref _context);
    }

    public Node AimAtTargetNode()
    {
        return new AimAtTargetNode(_context);
    }

    public Node DeviationNode(float maximumAllowedDeviation)
    {
        return new DeviationNode(_context, maximumAllowedDeviation);
    }

    public Node ShootNode(float fireAfterTime)
    {
        return new ShootNode(fireAfterTime, _context);
    }
}

[System.Serializable]
public class NodeContext
{
    public NavMeshAgent agent;
    public TankManager manager;
    public Transform currentTarget;
    public float timeSinceLastFire;
}
