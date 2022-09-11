using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorSwitch : MonoBehaviour
{
    public Color startColor = new Color(0.69f, 0.353f, 0.149f);
    public Color endColor = new Color(0.243f, 0.545f, 0.165f);
    float Time = 0f;
    private const int FramesCount = 100;
    private const float AnimationTimeSeconds = 2;
    private float _deltaTime = AnimationTimeSeconds / FramesCount;
    private const float finishedTime = 1f;

    public void ChangeColorState(bool changeToLiving)
    {
        StartCoroutine(ChangeColor(changeToLiving));
    }
    private IEnumerator ChangeColor(bool changeToLiving)//true makes things live, false makes things die issue - I think the bug is that the lerp isn't set correctly?
    {
        while (changeToLiving)
        {
            Time += _deltaTime;
            if (Time > finishedTime)
            {
                Time = 0f;
                GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, finishedTime);
                break;
            }
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, Time);
            yield return new WaitForSeconds(_deltaTime);
        }
        while(!changeToLiving)
        {
            Time += _deltaTime;
            if (Time > finishedTime)
            {
                Time = 0f;
                GetComponent<Renderer>().material.color = Color.Lerp(endColor, startColor, finishedTime);
                break;
            }
            GetComponent<Renderer>().material.color = Color.Lerp(endColor, startColor, Time);
            yield return new WaitForSeconds(_deltaTime);
        }
    }
}