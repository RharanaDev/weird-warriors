using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    //This Class cuts a string into equal arrays with custom lenght.
    public static string[] CutString(string arg, int eachCutLenght)
    {
        int howMuchParts;
        howMuchParts = arg.Length / eachCutLenght;

        int sumativeNumber = 0;

        if (arg.Length % eachCutLenght != 0)
        {
            howMuchParts++;
        }

        string[] cut = new string[howMuchParts];

        for (int i = 0; i < howMuchParts; i++)
        {
            cut[i] = arg.Substring(0 + sumativeNumber, eachCutLenght);
            sumativeNumber += eachCutLenght;

            if (sumativeNumber + eachCutLenght > arg.Length)
            {
                eachCutLenght = arg.Length - sumativeNumber;
            }
        }
        return cut;
    } 
}
