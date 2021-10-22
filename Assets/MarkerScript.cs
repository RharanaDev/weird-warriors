using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarkerScript : MonoBehaviour
{
    public int markerNumber;
    public bool isMyControllerOn;
    public string horizontal, vertical, punch, kick;
    int vectX, vectY;
    public Transform[] transforms;
    public int position, newPos;
    public int predictPos;
    public float cursorProgressX, cursorProgressY, timeCursor;
    public bool virginity;
    int previousPosX, previousPosY;
    public bool isActivated = true;
    public Preview prev;
    public Renderer rend;
    public MarkerScript otherMarker;
    public Animator anim;
    public DatabaseMenu data;
    public static int playersReady;
    bool ready;
    public AudioSource sound;
    public string[] sceneNames;

    public int predict;
    public static List<int> takenPositions = new List<int>();
    // Update is called once per frame
    private void Awake()
    {
        switch (markerNumber)
        {
            case 1:
                isMyControllerOn = GameOptions.player1Controller;
                position = 10;
                break;
            case 2:
                isMyControllerOn = GameOptions.player2Controller;
                position = 11;
                break;
        }
    }
	private void OnEnable()
	{
        switch (markerNumber)
        {
            case 1:
                isMyControllerOn = GameOptions.player1Controller;
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController1";
                    vertical = "VerticalController1";
                    punch = "PunchController1";
                    kick = "KickController1";
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal1";
                    vertical = "Vertical1";
                    punch = "Punch1";
                    kick = "Kick1";
                }
                break;

            case 2:
                isMyControllerOn = GameOptions.player2Controller;
                if (isMyControllerOn)
                {
                    horizontal = "HorizontalController2";
                    vertical = "VerticalController2";
                    punch = "PunchController2";
                    kick = "KickController2";
                }
                else if (!isMyControllerOn)
                {
                    horizontal = "Horizontal2";
                    vertical = "Vertical2";
                    punch = "Punch2";
                    kick = "Kick2";
                }
                break;
        }
    }
	void Start()
    {
        virginity = true;
        takenPositions = new List<int>();
        playersReady = 0;
    }
    void Update()
    {
        previousPosX = vectX;
        previousPosY = vectY;
        vectX = Mathf.FloorToInt(Input.GetAxis(horizontal));
        vectY = Mathf.FloorToInt(Input.GetAxis(vertical));

        if (isActivated)
        {
            if (virginity && vectX != 0 && vectY == 0)
            {
                virginity = false;
                MoveOnX(0);
            }
            else if (virginity && vectY != 0 && vectX == 0)
            {
                virginity = false;
                MoveOnY(0);
            }

            else if (vectX != 0 && cursorProgressX < timeCursor && vectY == 0 && !virginity)
            {
                cursorProgressX += Time.deltaTime;
            }
            else if (vectY != 0 && cursorProgressY < timeCursor && vectX == 0 && !virginity)
            {
                cursorProgressY += Time.deltaTime;
            }
            else
            {
                cursorProgressX = 0;
                cursorProgressY = 0;
                virginity = true;
            }
            if (cursorProgressX >= timeCursor)
            {
                MoveOnX(1);
            }
            else if (cursorProgressY >= timeCursor)
            {
                MoveOnY(1);
            }
        }
        OnPunch();
    }
    void OnPunch()
    {
        if (Input.GetButtonDown(punch))
        {
            if (GameOptions.chosenChars.Contains(position))
            {
                data.StartCoroutine(data.Fail(position-1));
            }
            else if (!ready)
            {
                ready = true;
                playersReady++;
                prev.ChangeState();

                if (playersReady == 2)
                {
                    StartCoroutine("BeginGame");
                }
            }
        }
    }
    IEnumerator BeginGame()
    {
        int n;
        n = Random.Range(0,3);

        yield return new WaitForSecondsRealtime(6);
        SceneManager.LoadScene(sceneNames[n]);
    }
    void MoveOnX(int mode)
    {
        int PosXused = 0;

        switch (mode)
        {
            case 0:
                PosXused = vectX;
                break;
            case 1:
                PosXused = previousPosX;
                break;
        }

        if (PosXused < 0)
        {
            if (position == 1)
            {
                predict = 16;
                position = 16;
            }
            else
            {
                predict = position - 1;
                if (CheckForPredict(predict))
                {

                    position--;
                }
                else
                {
                    if (predict == 1)
                    {
                        position = 16;
                    }
                    else
                    {
                        position -= 2;
                    }
                }
            }
        }
        else if (PosXused > 0)
        {
            if (position == 16)
            {
                predict = 1;
                if (CheckForPredict(predict))
                {
                position = 1;
                }
                else
                {
                position = 2;
                }
            }
            else
            {
                predict = position + 1;
                if (CheckForPredict(predict))
                {
                position++;
                }
                else
                {
                position += 2;
                }
            }
        }
        transform.position = transforms[position - 1].position;
        sound.Play();
        cursorProgressX = 0;
        prev.ChangeSprite(position - 1);
        CheckIfMorao();
    }
    void MoveOnY(int mode)
    {
        int PosYused = 0;

        switch (mode)
        {
            case 0:
                PosYused = vectY;
                break;
            case 1:
                PosYused = previousPosY;
                break;
        }

        if (PosYused < 0)
        {
            if (position >= 13)
            {
                predict = position - 12;
                if (CheckForPredict(predict))
                {
                    position -= 12;
                }
                else
                {
                    position -= 8;
                }
            }
            else
            {
                predict = position + 4;
                if (CheckForPredict(predict))
                {
                    position += 4;
                }
                else
                {
                    if(predict >= 13)
                    {
                        position -= 8;
                    }
                    else
                    {
                        position += 8;
                    }
                }
            }
        }
        else if (PosYused > 0)
        {
            if (position <= 4)
            {
                predict = position + 12;
                if (CheckForPredict(predict))
                {
                    position += 12;
                }
                else
                {
                    position += 8;
                }
            }
            else
            {
                predict = position - 4;
                if (CheckForPredict(predict))
                {
                    position -= 4;
                }
                else
                {
                    if (predict <= 4)
                    {
                        position += 8;
                    }
                    else
                    {
                        position -= 8;
                    }
                }
            }
        }
        transform.position = transforms[position - 1].position;
        sound.Play();
        cursorProgressY = 0;
        prev.ChangeSprite(position - 1);
        CheckIfMorao();
    }
    public void CheckIfMorao()
    {
        if(otherMarker.position == position)
        {
            anim.SetBool("Morao", true);
            otherMarker.anim.SetBool("Morao", true);
        }
        else
        {
            anim.SetBool("Morao", false);
            otherMarker.anim.SetBool("Morao", false);
        }
    }

    bool CheckForPredict(int prediction)
    {
        if (takenPositions.Contains(prediction))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
