using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtahBombScript : MonoBehaviour
{
    public InstanceSaver dataBase;
    public FightersData fighter;
    public Vector3 target, originalPos;
    public Transform myFather;
    public float parameter, parameter2;
    bool doneAlready;
    public float expandVel;
    bool changeRoute;
    public Camera cam;
    public float secs;
    bool coroutineStop;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {

        if (fighter.isDoingCombo2)
        {
            AssignTarget();
            if(myFather.position != target && !changeRoute)
            {
                myFather.position = Vector3.MoveTowards(myFather.position, target, parameter * Time.deltaTime);
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 6.6f * cam.GetComponent<CameraMove>().actualsize, expandVel * Time.deltaTime);
            }
            else if (myFather.position == target && !changeRoute)
            {
                if (!coroutineStop)
                {
                    StartCoroutine("ChangeRoute");
                    coroutineStop = true;
                }
            }
            else if(changeRoute && myFather.position != originalPos)
            {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, cam.GetComponent<CameraMove>().cameraSizing * cam.GetComponent<CameraMove>().actualsize, expandVel * Time.deltaTime);
                myFather.position = Vector3.MoveTowards(myFather.position, originalPos, expandVel * Time.deltaTime);
            }
            else if (changeRoute && myFather.position == originalPos)
            {
                CameraMove.cameraBeingAltered = false;
                fighter.isDoingCombo2 = false;
                doneAlready = false;
                changeRoute = false;
                dataBase.myRb.isKinematic = false;
                coroutineStop = false;
            }
        }
    }
    void AssignTarget()
    {
        if (!doneAlready)
        {
            dataBase.myRb.isKinematic = true;
            CameraMove.cameraBeingAltered = true;
            doneAlready = true;
            target = myFather.position + (Vector3.up * 3.3f);
            originalPos = myFather.position;
        }
    }
    IEnumerator ChangeRoute()
    {
        yield return new WaitForSeconds(1.2f);
        changeRoute = true;
    }
}
