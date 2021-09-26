using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtahMissileScript : MonoBehaviour
{
    Vector3 target;
    Transform trans;
    public Animator anim;
    public string targetto;
    public float constant;
    public float velocity;
    // Start is called before the first frame update

    void Start()
    {
        trans = GameObject.FindGameObjectWithTag(targetto).transform;
        target = new Vector3(trans.position.x, constant, trans.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, velocity * Time.deltaTime);
        }
        else
        {
            anim.SetTrigger("Explode");
            Destroy(gameObject, 4);
        }

    }
}
