using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    Node topBehaviourNode;
    NodeFactory nodeFactory;
    [SerializeField] NodeContext nodeContext;

    public void Init(TankManager tankManager)
    {
        nodeContext = new NodeContext()
        {
            manager = tankManager,
        };
        nodeFactory = new NodeFactory(ref nodeContext);
        //nodeContext.agent.updatePosition = false;
        ConstructBehaviourTree();
    }

    private void ConstructBehaviourTree()
    {
        FindTargetNode findTargetNode = nodeFactory.FindTargetNode(20f) as FindTargetNode;

        RangeNode fireRangeNode = nodeFactory.RangeNode(12f) as RangeNode;
        DeviationNode fireDeviationNode = nodeFactory.DeviationNode(2f) as DeviationNode;
        ShootNode fireActionNode = nodeFactory.ShootNode(1f) as ShootNode;
        Sequence fireActionGroupNode = nodeFactory.Sequence(new List<Node>() { fireRangeNode, fireDeviationNode, fireActionNode }) as Sequence;

        RangeNode aimRangeNode = nodeFactory.RangeNode(12f) as RangeNode;
        AimAtTargetNode aimAtTargetNode = nodeFactory.AimAtTargetNode() as AimAtTargetNode;
        Sequence aimActionGroupNode = nodeFactory.Sequence(new List<Node>() { aimRangeNode, aimAtTargetNode }) as Sequence;

        RangeNode chaseRangeNode = nodeFactory.RangeNode(4f) as RangeNode;
        Inverter chaseRangeInverted = nodeFactory.Inverter(chaseRangeNode) as Inverter;
        ChaseNode chaseNode = nodeFactory.ChaseNode() as ChaseNode;
        Sequence chaseActionGroupNode = nodeFactory.Sequence(new List<Node>() { chaseRangeInverted, chaseNode }) as Sequence;

        Selector actionSelectionNode = nodeFactory.Selector(new List<Node>() { fireActionGroupNode, aimActionGroupNode, chaseActionGroupNode }) as Selector;

        topBehaviourNode = nodeFactory.Sequence(new List<Node>() { findTargetNode, actionSelectionNode });
    }

    //public void Tick()
    //{
    //    topBehaviourNode.Evaluate();
    //}

    private void Update()
    {
        topBehaviourNode.Evaluate();
    }
}
