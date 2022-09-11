using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class plungerScript : MonoBehaviour
{
    private float power;
    float minPower = 0f;
    public float maxPower = 1500f;
    public Slider powerSlider;
    List<Rigidbody> ballList;
    public TextMeshProUGUI launchText;
    bool ballReady;
    // Start is called before the first frame update
    void Start()
    {
        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;
        ballList = new List<Rigidbody>();
        launchText.SetText("");//gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {      
        if(ballReady)
        {
            powerSlider.gameObject.SetActive(true);
        }
        else
        {
            powerSlider.gameObject.SetActive(false);
        }
        powerSlider.value = power;
        if(ballList.Count > 0)
        {
            ballReady = true;
            if(Input.GetKey(KeyCode.Space))
            {
                if(power <= maxPower)
                {
                    power += 200 * Time.deltaTime;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                foreach(Rigidbody r in ballList)
                {
                    r.AddForce(power * Vector3.forward);
                }
            }
        }else
        {
            ballReady = false;
            power = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Add(other.gameObject.GetComponent<Rigidbody>());
            launchText.SetText("Hold Space To Launch Ball");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ballList.Remove(other.gameObject.GetComponent<Rigidbody>());
            power = 0f;
            launchText.SetText("");
        }
    }
}
