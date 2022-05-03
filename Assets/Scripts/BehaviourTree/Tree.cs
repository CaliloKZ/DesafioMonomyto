using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{
    private Node m_root = null;

    protected void Start()
    {
        m_root = SetupTree();
    }

    private void Update()
    {
        if (m_root != null)
            m_root.Evaluate();
    }

    protected abstract Node SetupTree();
}
