using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSelection : MonoBehaviour
{
    [SerializeField] private GameObject m_singleCannon;
    [SerializeField] private GameObject[] m_doubleCannons;
    [SerializeField] private GameObject m_chargeCannon;

    public Gun selectedGun { get; private set; }

    [SerializeField] private List<Guns> m_obtainedGuns = new List<Guns>();

    public void EquipGun(Guns gun)
    {
        if (!m_obtainedGuns.Contains(gun))
            return;

        switch (gun)
        {
            case Guns.Single:
                break;
            case Guns.Double:
                break;
            case Guns.Charge:
                break;
        }
    }
}
