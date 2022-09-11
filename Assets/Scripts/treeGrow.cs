using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeGrow : MonoBehaviour//This program assumes we start at the InitScale(if haveItemShrinkOnGrow is true) or TargetScale(if haveItemShrinkOnGrow is false)
{
    private float _currentScale = InitScale;
    public float TargetScale = 2.0f;
    private const float InitScale = 0f;
    private const int FramesCount = 100;
    private const float AnimationTimeSeconds = 2;
    private float _deltaTime = AnimationTimeSeconds / FramesCount;
    private float _dx; //= (TargetScale - InitScale) / FramesCount;
    private GameObject thingToGrow;
    private Transform thingToGrowTransform;
    public bool haveItemShrinkOnGrow = false;//set to true if you want the item to disappear during the grow stage
    private IEnumerator Grow(bool changeToLiving)//true makes things live, false makes things die
    {
        if(haveItemShrinkOnGrow)
        {
            if(changeToLiving)
            {
                changeToLiving = false;
            }else
            {
                changeToLiving = true;
            }
        }
        while (changeToLiving)
        {
            _currentScale += _dx;
            if (_currentScale > TargetScale)
            {
                _currentScale = TargetScale;
                break;
            }
            thingToGrowTransform.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(_deltaTime);
        }
        
        while (!changeToLiving)
        {
            _currentScale -= _dx;
            if (_currentScale < InitScale)
            {
                _currentScale = InitScale;
                break;
            }
            thingToGrowTransform.localScale = Vector3.one * _currentScale;
            yield return new WaitForSeconds(_deltaTime);
        }
    }
    public void ChangeGrowthState(bool changeToLiving)
    {
        StartCoroutine(Grow(changeToLiving));
    }

    private void Start()
    {
        if(haveItemShrinkOnGrow)
        {
            _currentScale = TargetScale;
        }
        _dx = (TargetScale - InitScale) / FramesCount;
        thingToGrow = gameObject;
        thingToGrowTransform = gameObject.transform;
        //StartCoroutine(Grow(true));
    }
}
