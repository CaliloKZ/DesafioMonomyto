using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomItemDrop
{
    public static GameObject GetRandomDrop(List<Drops> dropList)
    {
        float _maxValue = 0;
        foreach (Drops drop in dropList)
        {
            _maxValue += drop.dropRate;
        }
        float _random = Random.Range(0, _maxValue);

        float sumValue = 0;
        for (int i = 0; i < dropList.Count; i++)
        {
            sumValue += dropList[i].dropRate;
            if (_random <= sumValue)
            {
                return dropList[i].itemToDrop;
            }
        }
        return null;
    }
}
