using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PathFindingController))]
public class DefaultFollowState : AIState
{
    private PathFindingController _pf;
    private NodeController _characterNode;
    public NodeController targetNode;
    private HashSet<NodeController> _currentPath;
    public float timeOfResearch;
    
    private void Awake()
    {
        _pf = GetComponent<PathFindingController>();
        _characterNode = GetComponent<NodeController>();
    }
    public override void Init()
    {
        GetPath();
        StartCoroutine(GetPathAgain(timeOfResearch));
    }
    public override void Play()
    {

    }

    private void GetPath() 
    {
        HashSet<NodeController> newPath = _pf.FindThePath(_characterNode, targetNode);
        if (newPath != null)
        {
            _currentPath = newPath;
        }
    }

    private IEnumerator GetPathAgain(float time)
    {
        while (AI.GetCurrentState() == this)
        {
            yield return new WaitForSeconds(time);
            GetPath();
        }
        yield return null;
    }
}
