using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedUI : MonoBehaviour
{
    public Animator anim;
    public int num;
    public MatchControlScript match;
    public InstanceSaver dataBase;
    // Start is called before the first frame update
    void ReturnToLife()
    {
        match.ControlDisable(true);
    }
    public void NumerUp()
    {
        num++;
        anim.SetInteger("Rounds", num);
    }
}
