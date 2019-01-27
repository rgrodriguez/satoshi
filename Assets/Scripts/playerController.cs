using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerController : MonoBehaviour {

    public float speed;

    private Rigidbody2D rb2d;
    private bool isWakingUp = true;
    private bool isRustling = false;
    private float rustleTimer = 0.0f;
    private float referenceX = 0.0f;
    private float struggleRating = 0.0f;

    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveCombined = Math.Min(Math.Abs(moveHorizontal) + Math.Abs(moveVertical), 1.0f);

        if (isWakingUp)
        {
            if (struggleRating > 3.0f)
            {
                isWakingUp = false;
                isRustling = false;
            }

            if (moveCombined > 0.0f && isRustling == false)
            {
                // Start rustle
                isRustling = true;

                rustleTimer = 0.0f;
                
                referenceX = transform.position.x;

                struggleRating += 0.1f;
            }
        }
        else
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }

        if (isRustling)
        {
            rustleTimer += Time.deltaTime;

            float rustleOffset = EaseCubicInOut(rustleTimer, -0.02f, 0.04f, 0.1f);

            if (rustleTimer > 0.1f)
            {
                isRustling = false;
                transform.position = new Vector3(referenceX, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(referenceX + rustleOffset, transform.position.y, transform.position.z);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Changable") || other.gameObject.CompareTag("frontDoor"))
        {
            other.gameObject.SetActive(false);
        }
        else
        {
            other.gameObject.SetActive(true);
        }
    }

    float EaseCubicInOut(float t, float b, float c, float d)
    {
        t /= d / 2.0f;
        if (t < 1.0f) return c / 2 * t * t * t + b;
        t -= 2.0f;
        return c / 2 * (t * t * t + 2.0f) + b;
    }
}
