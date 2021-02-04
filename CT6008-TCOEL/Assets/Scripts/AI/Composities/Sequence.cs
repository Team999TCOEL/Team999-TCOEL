////////////////////////////////////////////////////////////
// File: <Sequence.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <A sequence will return rue as ong as every child node returns true>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
	protected List<Node> nodes = new List<Node>();


	public Sequence(List<Node> nodes)
	{
		this.nodes = nodes; // creates a list of all child nodes in this sequence
	}

	public override NodeState Evaluate()
	{
		bool isAnyNodeRunning = false; // checks if any of the nodes is running and if they are the state cannot be broken
		foreach (var node in nodes)
		{
			switch (node.Evaluate())
			{
				case NodeState.RUNNING:
					Debug.Log("<color=yellow>Action Running: </color>" + node.ToString());
					isAnyNodeRunning = true;
					break;
				case NodeState.SUCCESS:
					Debug.Log("<color=green>Action Completed: </color>" + node.ToString());
					break;
				case NodeState.FAILURE:
					Debug.Log("<color=red>Action Failed: </color>" + node.ToString());
					_nodeState = NodeState.FAILURE; // if any of the nodes fail then the state fails
					return _nodeState;
				default:
					break;
			}
		}
		/*for (int i = 0; i < nodes.Count; i++)
		{
			Debug.Log("<color=white>Sequence</color>" + ": " + i + " " + nodes[i] + "--> ");
		}*/
		_nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS; // check if any states are running, if they are the node state is running else the node state is successful
		return _nodeState;
	}
}
