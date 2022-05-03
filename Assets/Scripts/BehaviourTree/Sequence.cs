using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public Sequence() : base () { }
    public Sequence(List<Node> children) : base(children) { }

    public override NodeState Evaluate()
    {
        bool _anyChildIsRunning = false;

        foreach(Node node in children)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    _state = NodeState.FAILURE;
                    return _state;
                case NodeState.SUCCESS:
                    continue;
                case NodeState.RUNNING:
                    _anyChildIsRunning = true;
                    continue;
                default:
                    _state = NodeState.SUCCESS;
                    continue;
            }
        }

        _state = _anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
        return _state;
    }
}
