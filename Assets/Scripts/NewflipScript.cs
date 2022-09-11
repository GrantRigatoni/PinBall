using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewflipScript : MonoBehaviour
{
    public float restPosition = 0f;
    public float pressedPosition = 45f;
    public float hitStrength = 10000f;
    public float flipperDamper = 150f;
    AudioSource audio;
    HingeJoint hinge;
    public string inputName;
    bool hit = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        hinge = GetComponent<HingeJoint>();
        hinge.useSpring = true;
    }

    // Update is called once per frame
    void Update()
    {
        JointSpring spring = new JointSpring();
        spring.spring = hitStrength;
        spring.damper = flipperDamper;

        if(Input.GetAxis(inputName) == 1)
        {
            spring.targetPosition = pressedPosition;
            Debug.Log("Pressed");
            if (!hit)
            {
                hit = true;
                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            hit = false;
            spring.targetPosition = restPosition;
        }
        hinge.spring = spring;
        hinge.useLimits = true;
    }
}
