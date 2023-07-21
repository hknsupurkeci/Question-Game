using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animscri : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    float x = 0;
    // Update is called once per frame
    void Update()
    {
        //x+=Time.deltaTime;
        //if (x > 2)
        //    anim.SetTrigger("hover");
        //Debug.Log(x);
    }
}
