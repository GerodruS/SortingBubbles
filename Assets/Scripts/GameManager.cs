using UnityEngine;
using System.Collections;

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
        var cameraRectangles = GetCameraRects();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            var car = Instantiate(carPrefab, playerPositions[i], Quaternion.identity) as Transform;
            var controller = car.GetComponent<BubbleController>();
            controller.suffix = "P" + (i + 1).ToString() + "_";

            var camera = car.GetComponentInChildren<Camera>();
            camera.rect = cameraRectangles[i];

            var meshRenderer = car.GetComponentInChildren<MeshRenderer>();
            meshRenderer.material = carMaterials[i];
        }
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
