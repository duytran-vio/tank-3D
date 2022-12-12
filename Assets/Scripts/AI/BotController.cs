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
        ConstructBehaviourTree();
    }

    private void ConstructBehaviourTree()
    {
        FindTargetNode findTargetNode = nodeFactory.FindTargetNode(10f) as FindTargetNode;
        ChaseNode chaseNode = nodeFactory.ChaseNode() as ChaseNode;
        Sequence chaseActionGroupNode = nodeFactory.Sequence(new List<Node>() { findTargetNode, chaseNode }) as Sequence;
        topBehaviourNode = nodeFactory.Selector(new List<Node>() { chaseActionGroupNode });
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
