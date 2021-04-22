using System.Collections.Generic;
using System.Linq;

namespace ElveesTaskClassLibrary
{
    public class MetroGraph<T>
    {
        private Dictionary<T, Node<T>> _graph;
        private int Stations { get; }
        private int Connections { get; }

        public MetroGraph(int stations, int connections)
        {
            _graph = new Dictionary<T, Node<T>>();
            Stations = stations;
            Connections = connections;
        }

        public void AddNode(T data)
        {
            _graph.TryGetValue(data, out Node<T> node);
            if (node == null)
            {
                node = new Node<T>(data);
                _graph.Add(data, node);
            }
        }

        public void AddConnection(T a, T b)
        {
            _graph.TryGetValue(a, out Node<T> nodeA);
            _graph.TryGetValue(b, out Node<T> nodeB);

            if (nodeA == null)
            {
                AddNode(a);
                _graph.TryGetValue(a, out nodeA);
            }

            if (nodeB == null)
            {
                AddNode(b);
                _graph.TryGetValue(b, out nodeB);
            }

            nodeA.ConnectWith(nodeB);
            nodeB.ConnectWith(nodeA);
        }

        public List<T> GetCloseSequence()
        {
            var visitedNodes = new HashSet<T>();
            var sequence = new List<T>();

            Node<T> BuildSubTree(Node<T> startNode)
            {
                var root = new Node<T>(startNode.Data);
                visitedNodes.Add(root.Data);

                foreach (var node in startNode.ConnectedNodes)
                {
                    if (!visitedNodes.Contains(node.Data))
                        root.ConnectWith(BuildSubTree(node));
                }

                sequence.Add(root.Data);

                return root;
            }

            var root = _graph.Values.ElementAt(0);
            BuildSubTree(root);

            return sequence;
        }

        /*public List<T> GetCloseSequence()
        {
            var visitedNodes = new HashSet<Node<T>>();
            var sequence = new List<T>();

            var nodes = new Stack<(Node<T>, int)>();

            var root = _graph.Values.ElementAt(0);
            nodes.Push((root, 0));
            
            while (nodes.Count > 0)
            {
                var currentNode = nodes.Peek().Item1;
                var currentBranch = nodes.Peek().Item2;

                visitedNodes.Add(currentNode);

                if (currentNode.ConnectedNodes.Count - 1 >= currentBranch &&
                    !visitedNodes.Contains(currentNode.ConnectedNodes[currentBranch]))
                {
                    nodes.Push((currentNode.ConnectedNodes[currentBranch], 0));
                }
                else
                {
                    sequence.Add(nodes.Pop().Item1.Data);
                    if (nodes.Count > 0)
                    {
                        var node = nodes.Pop();
                        node.Item2 += 1;
                        nodes.Push(node);
                    }
                }
            }

            return sequence;
        }*/
    }
}
