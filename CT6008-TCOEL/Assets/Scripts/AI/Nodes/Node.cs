////////////////////////////////////////////////////////////
// File: <Node.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <Base class for the nodes, all of the other nodes inherit from this class>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node
{
	protected NodeState _nodeState;

	public NodeState nodeState { get { return _nodeState; } } // get the current state of our node

	public abstract NodeState Evaluate();
}

public enum NodeState
{
	RUNNING,
	SUCCESS,
	FAILURE,
}
