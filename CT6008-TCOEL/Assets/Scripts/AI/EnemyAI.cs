////////////////////////////////////////////////////////////
// File: <EnemyAi.cs>
// Author: <Morgan Ellis>
// Date Created: <9/11/2020>
// Brief: <This is the main file for the guard that controls the behvaiour tree and therefor the actions on the guards>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Node topNode;

    public LayerMask platformLayerMask;

    private void Awake()
    {
    }

    private void Start()
    {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree()
    {
        PatrolNode patrolNode = new PatrolNode(this, platformLayerMask);

        Sequence chaseSequence = new Sequence(new List<Node> { });
        Selector patrolSelector = new Selector(new List<Node> { patrolNode});
        Sequence searchSequence = new Sequence(new List<Node> { });

        topNode = new Selector(new List<Node> { patrolSelector });
    }

    private void Update()
    {
        topNode.Evaluate();
    }
}

