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
            agent = tankManager.GetComponent<NavMeshAgent>()
        };
        nodeFactory = new NodeFactory(ref nodeContext);
        //nodeContext.agent.updatePosition = false;
        ConstructBehaviourTree();
    }

    private void ConstructBehaviourTree()
    {
        FindTargetNode findTargetNode = nodeFactory.FindTargetNode(10f) as FindTargetNode;

        RangeNode fireRangeNode = nodeFactory.RangeNode(5f) as RangeNode;
        DeviationNode fireDeviationNode = nodeFactory.DeviationNode(2f) as DeviationNode;
        ShootNode fireActionNode = nodeFactory.ShootNode(0.5f) as ShootNode;
        Sequence fireActionGroupNode = nodeFactory.Sequence(new List<Node>() { fireRangeNode, fireDeviationNode, fireActionNode }) as Sequence;

        RangeNode aimRangeNode = nodeFactory.RangeNode(6f) as RangeNode;
        AimAtTargetNode aimAtTargetNode = nodeFactory.AimAtTargetNode() as AimAtTargetNode;
        Sequence aimActionGroupNode = nodeFactory.Sequence(new List<Node>() { aimRangeNode, aimAtTargetNode }) as Sequence;

        RangeNode chaseRangeNode = nodeFactory.RangeNode(4f) as RangeNode;
        Inverter rangeNode2Inverted = nodeFactory.Inverter(chaseRangeNode) as Inverter;
        ChaseNode chaseNode = nodeFactory.ChaseNode(4f) as ChaseNode;
        Sequence chaseActionGroupNode = nodeFactory.Sequence(new List<Node>() { rangeNode2Inverted, chaseNode }) as Sequence;

        Selector actionSelectionNode = nodeFactory.Selector(new List<Node>() { fireActionGroupNode, aimActionGroupNode, chaseActionGroupNode }) as Selector;

        topBehaviourNode = nodeFactory.Sequence(new List<Node>() { findTargetNode, actionSelectionNode });
    }

    //public void Tick()
    //{
    //    topBehaviourNode.Evaluate();
    //}

    private void Update()
    {
        nodeContext.manager.tankInfo.movementInput = Vector3.zero;
        topBehaviourNode.Evaluate();
    }
}
