using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain
{
        public NodeController startNode;
        public NodeController endNode;
        public enum typeOfLink
        { Simple, JumpNode, DoubleJump }
        public typeOfLink linkType;
}
