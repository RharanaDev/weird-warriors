using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameOptions
{
    public static bool player1Controller, player2Controller;
    public static int display1, display2; // 0 = PLAYSTATION, 1 = XBOX, 2 = NINTENDO
    public static List<int> chosenChars = new List<int> {1,2,3,4,5,6,7,8,10,15};
}
