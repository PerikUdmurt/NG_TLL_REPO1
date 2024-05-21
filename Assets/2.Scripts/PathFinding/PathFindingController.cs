using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFindingController : MonoBehaviour
{
    public NodeController _startNode;
    public NodeController _endNode;
    [Header("Available Nodes")]
    public bool wallNode;
    public bool airNode;
    public bool waterNode;

    private HashSet<NodeController> openlist = new HashSet<NodeController>();
    private HashSet<NodeController> closedlist = new HashSet<NodeController>();
    private HashSet<NodeController> finalPath = new HashSet<NodeController>();

    [ContextMenu("FindThePath")]
    public void Test()
    {
        FindThePath(_startNode, _endNode);
    }

    public HashSet<NodeController> FindThePath(NodeController startNode, NodeController endNode)
    {
        if (startNode == null) { Debug.Log("Отсутствует начальная нода"); return null; }
        if (endNode == null) { Debug.Log("Отсутствует конечная нода"); return null; }

        if (startNode != null) 
        {
            openlist.Clear();
            closedlist.Clear();
            finalPath.Clear();
            openlist.Add(startNode); 
        }
        else
        {
            Debug.Log("не задана стартовая нода");
        } 
        
        var currentNode = openlist.First();

        while (openlist.Count > 0)
        {
            var shortestPath = float.MaxValue;
            foreach (var item in openlist)
            {
                if (AvailableNodeCheck(item))
                {
                    if (shortestPath > item.f)
                    {
                        shortestPath = item.f;
                        currentNode = item;
                    }
                }
            }

            if (currentNode == endNode)
            {
                Debug.Log("Найден путь");
                openlist.Clear();
                var node = endNode;
                while (node.parent != null)
                {
                    finalPath.Add(node);
                    
                    node = node.parent;
                }
                finalPath.Reverse();

                Debug.Log(finalPath.Count);
                return finalPath;
            }

            openlist.Remove(currentNode);
            closedlist.Add(currentNode);

            foreach (var child in currentNode.chains)
            {
                if (!AvailableNodeCheck(child.endNode))
                    continue;
                
                if (closedlist.Contains(child.endNode))
                    continue;

                child.endNode.parent = currentNode;
                var temp_G = currentNode.g + Vector3.Distance(currentNode.transform.position, child.endNode.transform.position);
                if (!openlist.Contains(child.endNode) || temp_G < child.endNode.g)
                {
                    child.endNode.g = temp_G;
                    child.endNode.h = Vector3.Distance(child.endNode.transform.position, endNode.transform.position);
                    child.endNode.f = child.endNode.g + child.endNode.h;
                }
                foreach (var openNode in openlist)
                    if (child.endNode == openNode && child.endNode.g > openNode.g && !AvailableNodeCheck(child.endNode))
                        continue;

                openlist.Add(child.endNode);
            }

        }
        Debug.Log("Путь не найден");
        return null;

    }
    
    private bool AvailableNodeCheck(NodeController node)
    {
        if (!wallNode)
        {
            if (node.type == NodeController.typeOfNode.Wall)
            {
                return false;
            }
        }
        if (!airNode)
        {
            if (node.type == NodeController.typeOfNode.Air)
            {
                return false;
            }
        }
        if (!waterNode)
        {
            if (node.type == NodeController.typeOfNode.Water)
            {
                return false;
            }
        }
        return true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (NodeController node in finalPath)
        {
            Gizmos.DrawSphere(node.gameObject.transform.position, 0.1f);
        }
    }
}



