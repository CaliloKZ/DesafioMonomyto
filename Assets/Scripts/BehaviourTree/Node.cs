using System.Collections;
using System.Collections.Generic;

public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}
public class Node
{
    protected NodeState _state;

    public Node parent;
    protected List<Node> children = new List<Node>();

    private Dictionary<string, object> m_dataContext = new Dictionary<string, object>();

    public Node()
    {
        parent = null;
    }
    public Node(List<Node> children)
    {
        foreach(Node child in children)
        {
            Attach(child);
        }
    }

    private void Attach(Node node)
    {
        node.parent = this;
        children.Add(node);
    }

    public virtual NodeState Evaluate() => NodeState.FAILURE;

    public void SetData(string key, object value)
    {
        m_dataContext[key] = value;
    }

    public object GetData(string key)
    {
        object value = null;

        if(m_dataContext.TryGetValue(key, out value))
            return value;

        Node node = parent;
        while (node != null)
        {
            value = node.GetData(key);
            if (value != null)
                return value;
            node = node.parent;
        }
        return null;
    }

    public bool ClearData(string key)
    {
        if (m_dataContext.ContainsKey(key))
        {
            m_dataContext.Remove(key);
            return true;
        }

        Node node = parent;
        while(node != null)
        {
            bool cleared = node.ClearData(key);
            if (cleared)
                return true;

            node = node.parent;
        }
        return false;
    }
}
