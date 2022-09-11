using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameRuleTracker : MonoBehaviour
{
    public bool canShoot = true;
    public GameObject ballPrefab;
    public Vector3 ballStartPos;
    private bool desertEnvironment = true;
    private bool desertEnvironmentcolor = true;
    private GameObject[] growableObjects;
    private GameObject[] colorableObjects;
    public float score = 0f;
    private float crossesHit = 0f;
    public float balls = 3f;
    public float lives = 3f;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ballText;
    public GameObject RestartButton;
    public GameObject crossPrefab;
    public GameObject cross1Prefab;
    public GameObject cross2Prefab;
    public GameObject cross3Prefab;
    public GameObject[] crosses;
    private AudioSource harp;
    //public TMPro text

    public void RestartGame()
    {
        if(!desertEnvironment)
        {
            environmentChange();
        }
        setScore(true, 0);
        setBalls(true, lives);
        crossesHit = 0f;
        canShoot = true;
        crosses = GameObject.FindGameObjectsWithTag("cross");
        foreach(GameObject go in crosses)
        {
            go.SetActive(false);
        }
        Instantiate(crossPrefab, ballStartPos, Quaternion.identity);
        Instantiate(cross1Prefab, ballStartPos, Quaternion.identity);
        Instantiate(cross2Prefab, ballStartPos, Quaternion.identity);
        Instantiate(cross3Prefab, ballStartPos, Quaternion.identity);
        RestartButton.SetActive(false);
    }



    public void EndGame()
    {
        RestartButton.SetActive(true);
    }

    public void hitACross()
    {
        crossesHit++;
        if(crossesHit >= 4f)
        {
            environmentChange();
        }
    }
    private void Start()
    {
        //get all growable and colorable objects
        growableObjects = GameObject.FindGameObjectsWithTag("Grow");
        colorableObjects = GameObject.FindGameObjectsWithTag("Color");
        harp = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(canShoot)
        {
            Instantiate(ballPrefab, ballStartPos, Quaternion.identity);
            canShoot = false;            
        }
    }
    public void environmentChange()
    {
        if (desertEnvironment)
        {
            //intantiate 4 new balls in cross location and add 1000 points + play harp
            setScore(false, 1000);
            Instantiate(ballPrefab, new Vector3(-5.65f, ballStartPos.y, 0.36f), Quaternion.identity);
            Instantiate(ballPrefab, new Vector3(5.2f, ballStartPos.y, 4.2f), Quaternion.identity);
            Instantiate(ballPrefab, new Vector3(-6.77f, ballStartPos.y, 12.09f), Quaternion.identity);
            Instantiate(ballPrefab, new Vector3(6.8f, ballStartPos.y, 21.2f), Quaternion.identity);
            harp.Play();
        }
        StartCoroutine(growObjects());
        StartCoroutine(colorObjects());
    }
    public void setScore(bool set, float amount)//is set is true, set the score, else add
    {
        if(set)
        {
            score = amount;
            scoreText.SetText("Score: " + score.ToString());

        }else
        {
            if(!desertEnvironment)
            {
                amount *= 2;
            }
            score += amount;
            scoreText.SetText("Score: " + score.ToString());
        }
    }

    public void setBalls(bool set, float amount)//is set is true, set the balls, else sub 1
    {
        if (set)
        {
            balls = amount;
            ballText.SetText("Balls Left: " + balls.ToString());

        }
        else
        {
            balls -= amount;
            ballText.SetText("Balls Left: " + balls.ToString());
        }
        if(balls <= 0)
        {
            EndGame();
        }
    }

    private IEnumerator growObjects()
    {
        foreach(GameObject gameObject in growableObjects)//call on one item to grow per frame for performance
        {
            treeGrow grow = gameObject.GetComponent<treeGrow>();
            if(grow == null)
            {
                Debug.Log("It's " + gameObject.name);
            }else
            {
                grow.ChangeGrowthState(desertEnvironment);
            }
            yield return null;
        }
        if(desertEnvironment)//update the state of the environment 
        {
            desertEnvironment = false;
        }
        else
        {
            desertEnvironment = true;
        }
    }
    private IEnumerator colorObjects()
    {
        foreach(GameObject gameObject in colorableObjects)
        {
            colorSwitch color = gameObject.GetComponent<colorSwitch>();
            color.ChangeColorState(desertEnvironmentcolor);
            yield return null;
        }
        if (desertEnvironmentcolor)//update the state of the environment 
        {
            desertEnvironmentcolor = false;
        }
        else
        {
            desertEnvironmentcolor = true;
        }
    }
}
