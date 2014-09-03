using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishLine : MonoBehaviour
{
    public int lapCount = 1;

    private Dictionary<Car, int> lapsProgress;
    private List<Car> topPlaces;

    private void Start()
    {
        lapsProgress = new Dictionary<Car, int>();
        topPlaces = new List<Car>();
    }

    public bool CompleteLap(Car car)
    {
        bool allLaps = false;

        if (!lapsProgress.ContainsKey(car))
        {
            lapsProgress[car] = 1;
        }
        else
        {
            ++lapsProgress[car];
        }

        if (lapsProgress[car] == lapCount)
        {
            topPlaces.Add(car);
            allLaps = true;
        }

        return allLaps;
    }

    public int getLap(Car car)
    {
        if (!lapsProgress.ContainsKey(car))
        {
            return 0;
        }
        else
        {
            return lapsProgress[car];
        }
    }

    public int getTopPlace(Car car)
    {
        int pos = topPlaces.FindIndex(p => p == car);
        return pos;
    }

}
