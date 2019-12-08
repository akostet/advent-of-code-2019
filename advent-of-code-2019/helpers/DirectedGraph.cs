using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace advent_of_code_2019.helpers
{
    public class Graph<T>
    {
        public Graph()
        {
            Nodes = new List<Node<T>>();
        }

        public List<Node<T>> Nodes { get; set; }

        public int Count { get { return Nodes.Count; } }

        public IEnumerable<Node<T>> UnlinkedNodes()
        {
            return Nodes.Where(x => x.IncomingLinks.Count == 0);
        }

        public bool Contains(T value)
        {
            return FindByValue(value) != null;
        }

        public void AddNode(T value)
        {
            Nodes.Add(new Node<T>(value));
        }

        public void AddNode(Node<T> node)
        {
            Nodes.Add(node);
        }

        public void RemoveNode(Node<T> node)
        {
            Nodes.Remove(node);

            foreach (var n in node.OutgoingLinks)
            {
                n.ToNode.RemoveLink(node);
            }
        }

        public void AddLink(Node<T> from, Node<T> to)
        {
            from.AddOutgoingLink(to);
            to.AddIncomingLink(from);
        }

        public void AddLink(T fromValue, T toValue)
        {
            var fromNode = GetOrAddNode(fromValue);
            var toNode = GetOrAddNode(toValue);

            fromNode.AddOutgoingLink(toNode);
            toNode.AddIncomingLink(fromNode);
        }

        public Node<T> GetOrAddNode(T value)
        {
            var node = FindByValue(value);
            if (node == null)
            {
                node = new Node<T>(value);
                AddNode(node);
            }

            return node;
        }

        public Node<T> FindByValue(T value)
        {
            return Nodes.FirstOrDefault(x => x.Value.Equals(value));
        }
    }

    [DebuggerDisplay("Value={Value}, IncomingLinks={IncomingLinks.Count}, OutgoingLinks={OutgoingLinks.Count}")]
    public class Node<T>
    {
        public Node(T value) : this()
        {
            Value = value;
        }

        public Node()
        {
            OutgoingLinks = new List<Link<T>>();
            IncomingLinks = new List<Link<T>>();
        }

        public T Value { get; set; }
        public List<Link<T>> OutgoingLinks { get; set; }
        public List<Link<T>> IncomingLinks { get; set; }

        public void AddOutgoingLink(Node<T> toNode)
        {
            Link<T> l = new Link<T>(this, toNode);
            OutgoingLinks.Add(l);
        }

        public void AddIncomingLink(Node<T> node)
        {
            Link<T> l = new Link<T>(this, node);
            IncomingLinks.Add(l);
        }

        public void RemoveLink(Node<T> node)
        {
            OutgoingLinks.RemoveAll(x => x.ToNode.Equals(node));
            IncomingLinks.RemoveAll(x => x.ToNode.Equals(node));
        }
    }

    [DebuggerDisplay("From={FromNode}, To={ToNode}")]
    public class Link<T>
    {
        public Link(Node<T> from, Node<T> to)
        {
            FromNode = from;
            ToNode = to;
        }

        public Node<T> FromNode { get; set; }
        public Node<T> ToNode { get; set; }
    }
}
