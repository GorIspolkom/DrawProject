using UnityEngine;
using System;

namespace Assets.Scrypts.Entity
{
    [Serializable]
    public class Node
    {
        public Vector2 StartPoint { get; private set; }
        public Vector2 EndPoint { get; private set; }
        public Node NextNode { get; private set; }

        public Node(Vector2 StartPoint, Vector2 EndPoint, Node NextNode = null)
        {
            this.StartPoint = StartPoint;
            this.EndPoint = EndPoint;
            this.NextNode = NextNode;
        }

        public void SetNextNode(Node NextNode)
        {
            this.NextNode = NextNode;
        }
    }
}
