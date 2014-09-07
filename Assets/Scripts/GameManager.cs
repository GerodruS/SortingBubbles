using UnityEngine;
using System.Collections;
using Eppy;

public class GameManager : MonoBehaviour {

    [Range(1, 4)]
    public int numberOfPlayers = 1;
    public Transform carPrefab;
    public Vector3[] playerPositions;
    public Material[] carMaterials;
	void Start () {
        CreatePlayers();
	}

    void CreatePlayers()
    {
        for (int i = 0; i < numberOfPlayers; i++)
        {
            CreatePlayer(i);
        }
    }

    public GameObject CreatePlayer(int i)
    {
        var cameraRectangles = GetCameraRects();

        //Debug.Log("Create " + i);
        var car = Instantiate(carPrefab, playerPositions[i], Quaternion.identity) as Transform;
        car.rigidbody2D.isKinematic = true;
        var controller = car.GetComponent<BubbleController>();
        controller.suffixes.Add(new Tuple<string, float>("P" + (i + 1).ToString() + "_", 1.0f));

        var camera = car.GetComponentInChildren<Camera>();
        camera.name = "P" + (i + 1).ToString() + "_Camera";
        camera.rect = cameraRectangles[i];

        var meshRenderer = car.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material = carMaterials[i];

        return car.gameObject;
    }

    Rect[] GetCameraRects()
    {
        var rects = new Rect[numberOfPlayers];
        if (numberOfPlayers < 4)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                rects[i] = new Rect((float)i / numberOfPlayers, 0, 1.0f / numberOfPlayers, 1);
            }
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    rects[i * 2 + j] = new Rect(i * 0.5f, j * 0.5f, 0.5f, 0.5f);
                }
            }
        }
        return rects;
    }
}
