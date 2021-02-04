////////////////////////////////////////////////////////////
// File: <Selector.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <A selector will return true as long as one of the child nodes is returning true>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
	protected List<Node> nodes = new List<Node>();

	public Selector(List<Node> nodes)
	{
		this.nodes = nodes; // creates a list of all child nodes in this selector
	}

	public override NodeState Evaluate()
	{
		foreach (var node in nodes)
		{
			switch (node.Evaluate())
			{
				case NodeState.RUNNING:
					Debug.Log("<color=yellow>Action Running: </color>" + node.ToString());
					_nodeState = NodeState.RUNNING;
					return _nodeState;
				case NodeState.SUCCESS:
					Debug.Log("<color=green>Action Completed: </color>" + node.ToString());
					_nodeState = NodeState.SUCCESS; 
					return _nodeState;
				case NodeState.FAILURE:
					Debug.Log("<color=red>Action Failed: </color>" + node.ToString());
					break;
				default:
					break;
			}
		}
		_nodeState = NodeState.FAILURE; // all child nodes must have failed as the did not exit the switch statement
		return _nodeState;
	}
}
