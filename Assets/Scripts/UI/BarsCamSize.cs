using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsCamSize : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 biggestScale;
    public Camera cam;
    public float maxCamsize, camsize;
    float maxPos = 5.92f;
    float newPos;
    public float actualsize;
    // Update is called once per frame
    private void Start()
    {
        cam = Camera.main;
        actualsize = 1.777778f / ((float)Screen.currentResolution.width / (float)Screen.currentResolution.height);
        maxCamsize *= actualsize;
    }
    void Update()
    {
        camsize = cam.orthographicSize;
        transform.localScale = new Vector3(biggestScale.x/ maxCamsize * camsize, biggestScale.y / maxCamsize * camsize, biggestScale.z);
        newPos = maxPos / maxCamsize * camsize;
        transform.localPosition = new Vector3(transform.localPosition.x, newPos, transform.localPosition.z);
    }
}
