using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : INode
{
    List<INode> _childs;

    public SequenceNode(List<INode> childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_childs == null || _childs.Count == 0)
            return INode.ENodeState.EFailure;

        foreach (var child in _childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.ERunning:
                    return INode.ENodeState.ERunning;
                case INode.ENodeState.ESuccess:
                    continue;
                case INode.ENodeState.EFailure:
                    return INode.ENodeState.EFailure;
            }
        }

        return INode.ENodeState.ESuccess;
    }
}
