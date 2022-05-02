using UnityEngine;

public class AmmoCollectable : MonoBehaviour
{
    [field: SerializeField] public Weapons ammoType { get; private set; }
    [field: SerializeField] public int ammoAmount { get; private set; }

}
