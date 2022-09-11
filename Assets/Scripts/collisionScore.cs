using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionScore : MonoBehaviour
{
    private GameObject gameRuleTrack;
    private GameRuleTracker tracker;
    public float points = 100f;
    AudioSource audioSource;

    private void Start()
    {
        gameRuleTrack = GameObject.Find("GameRuleTracker");
        tracker = gameRuleTrack.GetComponent<GameRuleTracker>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            audioSource.Play();
            tracker.setScore(false, points);
        }
    }
}
