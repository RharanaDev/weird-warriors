using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCameraAdjust : MonoBehaviour
{
    public float actualsize;
    // Start is called before the first frame update
    void Start()
    {
        actualsize = Camera.main.orthographicSize * (1.777778f / ((float)Screen.currentResolution.width / (float)Screen.currentResolution.height));
        Camera.main.orthographicSize = actualsize;
    }
}
