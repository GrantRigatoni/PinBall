using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnCollection : MonoBehaviour
{
    public GameObject explosion;
    public ParticleSystem part;
    public GameObject child;
    public GameObject gameRuleTrack;
    public GameRuleTracker tracker;
    public AudioSource audioSource;
    private void Start()
    {
        gameRuleTrack = GameObject.Find("GameRuleTracker");
        tracker = gameRuleTrack.GetComponent<GameRuleTracker>();
        part = explosion.GetComponent<ParticleSystem>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag ("Ball"))
        {
            Debug.Log("here");
            part.Play();
            Renderer mr = gameObject.GetComponent<Renderer>();
            mr.forceRenderingOff = true;
            mr = child.GetComponent<Renderer>();
            mr.forceRenderingOff = true;
            GetComponent<Collider>().enabled = false;
            tracker.hitACross();
            audioSource.Play();
            Invoke("kill", 1.0f);
        }
    }

    private void kill()
    {
        part.Pause();
        explosion.SetActive(false);
        gameObject.SetActive(false);
    }
}
