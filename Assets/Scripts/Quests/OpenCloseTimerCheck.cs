using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseTimerCheck : MonoBehaviour
{
    public GameObject trigger; // The trigger GameObject
    public GameObject timer; // The timer GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the timer is initially inactive
        if (timer != null)
        {
            timer.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the trigger GameObject is destroyed
        if (trigger == null)
        {
            // Activate the timer GameObject
            if (timer != null)
            {
                timer.SetActive(true);
                timer.GetComponent<Timer>().StartCountdown(); // Bắt đầu đếm ngược khi kích hoạt
            }
        }
    }
}
