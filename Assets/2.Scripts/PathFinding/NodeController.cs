using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeController : MonoBehaviour
{
    public List<Chain> chains;

    public enum typeOfNode
    {
        Platform, EdgeOfPlatform, Wall, Air, Water, Character, OneWayPlatform
    }
    public typeOfNode type;

    [HideInInspector]
    public NodeController parent;
    [HideInInspector]
    public float g;
    [HideInInspector]
    public float h;
    [HideInInspector]
    public float f;
    [HideInInspector]
    public float weight;


    public void AddNode(NodeController node, Chain.typeOfLink linkType)
    {
       chains.Add(new Chain { startNode = this, endNode = node, linkType = linkType });
    }
    
    public void RemoveNode(NodeController node)
    {
        chains.Remove(new Chain { endNode = node });
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NodeController>())
        {
            NodeController colNode = collision.gameObject.GetComponent<NodeController>();
            AddNode(colNode, Chain.typeOfLink.Simple);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<NodeController>())
        {
            NodeController colNode = (collision.gameObject.GetComponent<NodeController>());
            RemoveNode(colNode);
        }
    }

    
}
