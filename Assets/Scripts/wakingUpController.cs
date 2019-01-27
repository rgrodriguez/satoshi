using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class wakingUpController : MonoBehaviour
{
    private const float VolumeScale = 10.0f;
    private const float VolumeStep = 0.02f;
    private const float VolumeDecayFactor = 0.05f;
    private const float VolumeDecayIntervalInS = 0.03f;
    private const float GirlStationaryVolume = 0.07f;
    private const float GirlStrugglingVolume = GirlStationaryVolume * 1.9f;
    private const float GirlStrugglingMaximumVolume = GirlStationaryVolume * 2.9f;
    private const float DisplayIntervalInS = 0.1f;
    private const float DurationToStayInStrugglingStateInS = 1.0f;

    private AudioSource audioData;
    private float timer = 0.0f;
    private float displayTimer = 0.0f;
    private float lastInputChangeTimer = 0.0f;
    private float volume = GirlStationaryVolume;
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
        lastInputChangeTimer -= Time.deltaTime;
        displayTimer += Time.deltaTime;

        // Build up the volume if the input occurs repeatedly

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveCombined = Math.Min(Math.Abs(moveHorizontal) + Math.Abs(moveVertical), 1.0f);

        // Increase the volume a set amount every time movement axis value increases
        if (moveCombined > lastMoveHorizontal)
        {
            volume += VolumeStep;

            // Set the countdown until the volume goes back to stationary level
            lastInputChangeTimer = DurationToStayInStrugglingStateInS;

            Debug.Log($"increased volume to {volume}");
        }

        // If girl is not struggling
        if (lastInputChangeTimer <= 0.0f && timer > VolumeDecayIntervalInS)
        {
            // Reset the timer
            timer = 0.0f;

            // Decrease the volume over time
            volume *= 1.0f - VolumeDecayFactor;
        }

        // Apply minimum volume

        float minimumVolume;

        // Decide which minimum volume value to use
        if (lastInputChangeTimer <= 0.0f)
        {
            minimumVolume = GirlStationaryVolume;
        }
        else
        {
            minimumVolume = GirlStrugglingVolume;
        }

        // Keep volume at the chosen minimum value
        if (volume < minimumVolume)
        {
            volume = minimumVolume;
        }

        // Cap the volume
        if (volume > GirlStrugglingMaximumVolume)
        {
            volume = GirlStrugglingMaximumVolume;
        }

        // Display volume for debug
        if (displayTimer > DisplayIntervalInS)
        {
            displayTimer = 0.0f;

            Debug.Log($"volume = {volume}");
        }

        // Save the last value for movement axis
        lastMoveHorizontal = moveCombined;

        // Set scale the internal volume to an exponential scale
        audioData.volume = EaseCubicInOut(volume, 0, 1, 1) * VolumeScale;
    }

    float EaseCubicInOut(float t, float b, float c, float d)
    {
        t /= d / 2.0f;
        if (t < 1.0f) return c / 2 * t * t * t + b;
        t -= 2.0f;
        return c / 2 * (t * t * t + 2.0f) + b;
    }
}
