using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public bool shake;
    public float strength;
    public CameraMove camScript;
    public float shakeDur;
    public static bool isShaking;
    float valueCrazy, valueCrazy2;
    public BoxCollider2D constraint1, constraint2, constraint3;
    Vector3 supposedPos;
    bool shakeBool;
    float clampY, clampX;
    public Vector3 originalPos;
    public float offsetY, offsetX;

    public IEnumerator Shake(float time)
    {
        originalPos = transform.position;
        Deactivate();
        shakeBool = true;
        yield return new WaitForSecondsRealtime(time);
        shakeBool = false;
        Reactivate();
        isShaking = false;
        camScript.enabled = true;
    }
    public void Deactivate()
    {
        constraint1.enabled = false;
        constraint2.enabled = false;
        constraint3.enabled = false;
    }
    public void Reactivate()
    {
        constraint1.enabled = true;
        constraint2.enabled = true;
        constraint3.enabled = true;
    }
    public void Update()
    {
        if(shakeBool)
        {
            supposedPos = transform.position + new Vector3(Random.insideUnitSphere.x * strength, Random.insideUnitSphere.y * strength, 0);
            valueCrazy = Mathf.Clamp(supposedPos.y, originalPos.y - offsetY, originalPos.y + offsetY);
            valueCrazy2 = Mathf.Clamp(supposedPos.x, originalPos.x - offsetX, originalPos.x + offsetX);
            transform.position = new Vector3(valueCrazy2, valueCrazy, supposedPos.z);
            camScript.enabled = false;
        }
    }
}
