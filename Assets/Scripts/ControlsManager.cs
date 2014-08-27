using UnityEngine;
using System.Collections;

public class ControlsManager : MonoBehaviour {
    [System.Serializable]
    public class PlayerControls
    {
        public KeyCode Up, Down, Left, Right;
    }

    public PlayerControls[] Controls;
}
