using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballMovement : MonoBehaviour
{
    public GameObject GameRuleTrack;
    GameRuleTracker tracker;
    public Rigidbody ballRB;
    public Transform trans;

    private void Awake()
    {
        GameRuleTrack = GameObject.Find("GameRuleTracker");
        tracker = GameRuleTrack.GetComponent<GameRuleTracker>();
    }
    private void Start()
    {
        Debug.Log("ball Spawned");
    }
    void Update()
    {
        if (trans.position.z < -12f)
        {
            if(GameObject.FindGameObjectsWithTag("Ball").Length == 1)
            {
                if (tracker.balls != 1)
                {
                    tracker.canShoot = true;
                }
                tracker.setBalls(false, 1); 
            }
            gameObject.SetActive(false);
        }
    }
}
