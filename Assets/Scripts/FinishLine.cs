using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FinishLine : MonoBehaviour
{
    public int lapCount = 1;

    private Dictionary<Car, int> _lapsProgress;
    private List<Car> _topPlaces;
    private int _leftToFinish;

    private void Start()
    {
        _lapsProgress = new Dictionary<Car, int>();
        _topPlaces = new List<Car>();

        GameManager manager = GameObject.Find("Managers").GetComponent<GameManager>();
        _leftToFinish = manager.numberOfPlayers;
    }

    public bool CompleteLap(Car car)
    {
        bool allLaps = false;

        if (!_lapsProgress.ContainsKey(car))
        {
            _lapsProgress[car] = 1;
        }
        else
        {
            ++_lapsProgress[car];
        }

        string suffixesString = string.Empty;
        List<string> suffixesList = car.GetComponent<BubbleController>().suffixes;
        for (int i = 0, count = suffixesList.Count; i < count; ++i)
        {
            suffixesString += suffixesList[i] + " ";
        }

        if (_lapsProgress[car] == lapCount)
        {
            _topPlaces.Add(car);
            allLaps = true;
                        
            Debug.Log(suffixesString + "finished at " + (_topPlaces.FindIndex(p => p == car) + 1) + " place!");

            --_leftToFinish;
            if (0 == _leftToFinish)
            {
                Debug.Log("All cars finished! Race is over!");
            }
        }
        else
        {
            Debug.Log(suffixesString + "finished " + _lapsProgress[car] + " lap!");
        }

        return allLaps;
    }

    public int getLap(Car car)
    {
        if (!_lapsProgress.ContainsKey(car))
        {
            return 0;
        }
        else
        {
            return _lapsProgress[car];
        }
    }

    public int getTopPlace(Car car)
    {
        int pos = _topPlaces.FindIndex(p => p == car);
        return pos;
    }

}
