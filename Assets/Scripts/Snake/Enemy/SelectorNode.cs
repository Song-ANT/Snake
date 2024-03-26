using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : INode
{
    List<INode> _childs;

    public SelectorNode(List<INode> childs)
    {
        _childs = childs;
    }

    public INode.ENodeState Evaluate()
    {
        if (_childs == null)
            return INode.ENodeState.EFailure;

        foreach (var child in _childs)
        {
            switch (child.Evaluate())
            {
                case INode.ENodeState.ERunning:
                    return INode.ENodeState.ERunning;
                case INode.ENodeState.ESuccess:
                    return INode.ENodeState.ESuccess;
            }
        }

        return INode.ENodeState.EFailure;
    }
}
