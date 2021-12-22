using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Movement : MonoBehaviour
{
    public InstanceSaver database1, database2;
    public Vector3 offset;
    public Camera cam;
    public float cameraX;
    public float cameraUp;
    public float cameraMove;
    public float speed;
    private GameObject player1;
    private GameObject player2;
    public int playerToFollow;
    public float parametric;
    private PlayerGeneralMoveController playerInfo1;
    private PlayerGeneralMoveController playerInfo2;
    Vector3 rot;
    public float rotProd;
    public float cameraMidPoint;
    public float player1width, player2width;
    private BoxCollider2D boxco1, boxco2;
    public bool enableCamMove;
    public float targety;
    public static int dummyCount, bigDummyCount;
    public static bool slowly;
    public static bool customPos;
    public static float variable;
    public float normalisedPlayer1Pos, normalisedPlayer2Pos;
    public BoxCollider2D box1, box2;
    public float previousPos;
    bool prevPosBool;
    public float camSpeed;
    public float standardHeight;
    public float heightInUse, averageDifference, suavity;
    public float debugDistance1, debugDistance2;
    public bool isPlayerSettled;
    public static bool specialCameraMove1, specialCameraMove2;
    public float numberFloored1, numberFloored2;
    public float cameraSizing;
    public static bool cameraBeingAltered;
    public float actualsize;
    float higher;

	void Start()
    {
        actualsize = 1.777778f / ((float)Screen.currentResolution.width / (float)Screen.currentResolution.height);
        player1 = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("PlayerTwo");
        playerInfo1 = player1.GetComponent<PlayerGeneralMoveController>();
        playerInfo2 = player2.GetComponent<PlayerGeneralMoveController>();
        boxco1 = player1.GetComponent<BoxCollider2D>();
        boxco2 = player2.GetComponent<BoxCollider2D>();
        cameraUp = offset.y;
        player1width = boxco1.size.x / 2;
        player2width = boxco2.size.x / 2;

        debugDistance1 = Mathf.Abs(player1.transform.position.y - transform.position.y);
        debugDistance2 = Mathf.Abs(player2.transform.position.y - transform.position.y);
        averageDifference = Mathf.Abs(player1.transform.position.y - player2.transform.position.y);
        isPlayerSettled = true;
    } //4.89 a //6.626357 //0.41f

    void FixedUpdate()
    {
        IsOnRegion();
        DetectRotation();
        CameraOnHeight();
    }
    private void IsOnRegion()
    {
        if (player1.transform.position.x > player2.transform.position.x)
        {
            normalisedPlayer1Pos = (player1width) + player1.transform.position.x;
            normalisedPlayer2Pos = (-player2width) + player2.transform.position.x;
        }
        else if (player1.transform.position.x <= player2.transform.position.x)
        {
            normalisedPlayer1Pos = (-player1width) + player1.transform.position.x;
            normalisedPlayer2Pos = (player2width) + player2.transform.position.x;
        }

        cameraMidPoint = Mathf.Clamp((normalisedPlayer1Pos + normalisedPlayer2Pos) / 2, -11.69f, 11.69f); //18.5f

        if (Mathf.Abs(normalisedPlayer1Pos - normalisedPlayer2Pos) <= 18.5f)
        {
            enableCamMove = true;
        }
        else
        {
            enableCamMove = false;
        }
        if (Mathf.Abs(normalisedPlayer1Pos - normalisedPlayer2Pos) >= 15.836f)
        {
            variable = Mathf.Clamp(Mathf.Abs(normalisedPlayer1Pos - normalisedPlayer2Pos), 0, 18.5f);
            if (!customPos)
            {
                cameraSizing = 6.6f / 18.5f * variable;
                if (!cameraBeingAltered)
                {
                    cam.orthographicSize = cameraSizing * actualsize;
                }
                box1.size = new Vector2((6.626357f / variable) * 15.836f, 0.7051506f);
                box2.size = new Vector2((6.626357f / variable) * 15.836f, 0.7051506f);
            }
        }
        else
        {
            variable = 15.836f;
            if (!cameraBeingAltered)
            {
                cameraSizing = 6.6f / 18.5f * 15.836f;
                if (true)
                {
                    cam.orthographicSize = cameraSizing * actualsize;
                }
                box1.size = new Vector2(6.626357f, 0.7051506f);
                box2.size = new Vector2(6.626357f, 0.7051506f);
            }
        }
    }
    private void LateUpdate()
    {
        if (enableCamMove)
        {
            if (Mathf.Abs(previousPos - cameraMidPoint) >= 0.5f && !prevPosBool)
            {

                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, cameraMidPoint, camSpeed * Time.deltaTime), cameraUp, offset.z);

                if(transform.position.x >= cameraMidPoint || transform.position.x == -11.69f || transform.position.x == 11.69f)
                {
                    prevPosBool = true;
                }
            }
            else
            {
                prevPosBool = false;
                transform.position = new Vector3(cameraMidPoint, cameraUp, offset.z);
                previousPos = transform.position.x;
            }
        }
        else if(!enableCamMove)
        {
            prevPosBool = false;
            transform.position = new Vector3(transform.position.x, cameraUp, offset.z);
        }
    }
    void CameraOnHeight()
    {
        numberFloored1 = Mathf.Floor(Mathf.Abs(player1.transform.position.y - player2.transform.position.y) * 100) / 100;
        numberFloored2 = Mathf.Floor(averageDifference * 100) / 100;
        if (specialCameraMove1 && numberFloored1 != numberFloored2 && isPlayerSettled || specialCameraMove2 && numberFloored1 != numberFloored2 && isPlayerSettled)
        {
            cameraUp = Mathf.Clamp(player1.transform.position.y >= player2.transform.position.y ? Mathf.Lerp(cameraUp, database1.player1.transform.position.y + 3f, 0.8f) : Mathf.Lerp(cameraUp, database1.player2.transform.position.y + 3f, 0.8f), -2.98f, 10f);
        }
        else if (!specialCameraMove1 && !specialCameraMove2 && numberFloored1 != numberFloored2 && isPlayerSettled)
        {
            cameraUp = Mathf.Clamp(player1.transform.position.y >= player2.transform.position.y ? Mathf.Lerp(cameraUp, database1.player1.transform.position.y, 0.2f) : Mathf.Lerp(cameraUp, database1.player2.transform.position.y, 0.2f), -2.98f, 2f);
        }
    }
    void DetectRotation()
    {
        rot = player1.transform.forward;

        if (normalisedPlayer1Pos > normalisedPlayer2Pos && rot.z == 1 && playerInfo1.rotating == false)
        {
            playerInfo1.Turn();
            playerInfo2.Turn();
        }
        if (normalisedPlayer2Pos > normalisedPlayer1Pos && rot.z == -1 && playerInfo1.rotating == false)
        {
             playerInfo1.Turn();
             playerInfo2.Turn();
        }
    }
}
