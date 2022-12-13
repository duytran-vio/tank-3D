using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
    protected NodeState _nodeState;

    public NodeState State => _nodeState;

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    RUNNING, SUCCESS, FAILURE
}
