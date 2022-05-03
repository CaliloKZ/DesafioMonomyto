using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : Tree
{
    public EnemyShoot enemyShoot { get; private set; }

    [field: SerializeField] public Transform[] waypoints { get; private set; }
    [field: SerializeField] public float moveSpeed { get; private set; }
    [field: SerializeField] public float fovRange { get; private set; }

    [field: SerializeField] public float minRange { get; private set; }
    [field: SerializeField] public float chaseRange { get; private set; }

    private void Awake() => enemyShoot = GetComponent<EnemyShoot>();

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckPlayerInFOVRange(this),
                new TaskChaseAndShoot(this),
            }),
            new TaskPatrol(this),
        });

        return root;
    }

}
