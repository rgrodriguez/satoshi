using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wakingUpController : MonoBehaviour
{
    private const float VolumeStep = 0.01f;
    private const float VolumeDecayFactor = 0.05f;
    private const float VolumeDecayIntervalInS = 0.03f;
    private const float MinimumVolume = 0.15f;

    private AudioSource audioData;
    private float timer = 0.0f;
    private float volume = MinimumVolume;
    private float lastMoveHorizontal = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
        Debug.Log("started music");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        // Build up the volume if the input occurs repeatedly

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Increase the volume a set amount every time movement axis value increases
        if (moveHorizontal > lastMoveHorizontal)
        {
            volume += VolumeStep;

            Debug.Log($"increased volume to {volume}");
        }

        // Decrease the volume over time
        if (timer > VolumeDecayIntervalInS)
        {
            // Reset the timer
            timer = 0.0f;

            // Reduce the volume by one step
            volume *= 1.0f - VolumeDecayFactor;

            // Keep volume at minimum value
            if (volume < MinimumVolume)
            {
                volume = MinimumVolume;
            }

            Debug.Log($"decreased volume to {volume}");
        }

        // Save the last value for movement axis
        lastMoveHorizontal = moveHorizontal;

        // Set scale the internal volume to an exponential scale
        audioData.volume = EaseCubicInOut(volume, 0, 1, 1);
    }

    float EaseCubicInOut(float t, float b, float c, float d)
    {
        t /= d / 2.0f;
        if (t < 1.0f) return c / 2 * t * t * t + b;
        t -= 2.0f;
        return c / 2 * (t * t * t + 2.0f) + b;
    }
}
