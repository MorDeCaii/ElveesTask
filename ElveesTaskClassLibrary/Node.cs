using System.Collections.Generic;

namespace ElveesTaskClassLibrary
{
    internal class Node<T>
    {
        public T Data { get; }
        public List<Node<T>> ConnectedNodes { get; set; }

        public Node(T data)
        {
            Data = data;
            ConnectedNodes = new List<Node<T>>();
        }

        public void ConnectWith( Node<T> node)
        {
            ConnectedNodes.Add(node);
        }
    }
}
