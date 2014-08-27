using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    [Range(1, 4)]
    public int nuberOfPlayers;
    public Transform carPrefab;
    public Vector3[] playerPositions;

	void Start () {
        CreatePlayers();
	}

    void CreatePlayers()
    {
        for (int i = 0; i < nuberOfPlayers; i++)
        {
            var a = Instantiate(carPrefab, playerPositions[i], Quaternion.identity) as Transform;
            var controller = a.GetComponent<BubbleController>();
            controller.suffix = "P" + (i + 1).ToString() + "_";
        }
    }
}
