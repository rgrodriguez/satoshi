using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerController : MonoBehaviour {

    public float speed;
    public AudioSource[] sheetsSounds;
    public AudioSource jumpAndLandSound;

    private Rigidbody2D rb2d;
    private bool isWakingUp = true;
    private bool isRustling = false;
    private bool isJumping = false;
    private float rustleTimer = 0.0f;
    private float jumpingTimer = 0.0f;
    private float referenceX = 0.0f;
    private float referenceY = 0.0f;
    private float struggleRating = 0.0f;

    private const float StruggleThreshold = 3.0f;

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
            if (struggleRating > StruggleThreshold && isJumping == false)
            {
                //isWakingUp = false;
                isRustling = false;

                transform.position = new Vector3(referenceX, transform.position.y, transform.position.z);
                isJumping = true;

                jumpAndLandSound.Play();

                referenceX = transform.position.x;
                referenceY = transform.position.y;
            }

            if (moveCombined > 0.0f && isRustling == false && isJumping == false)
            {
                // Start rustle
                isRustling = true;

                rustleTimer = 0.0f;

                referenceX = transform.position.x;

                struggleRating += 0.1f;

                int index = UnityEngine.Random.Range(0, 4);
                sheetsSounds[index].Play();
            }
        }
        else
        {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            rb2d.AddForce(movement * speed);
        }

        if (isJumping)
        {
            jumpingTimer += Time.deltaTime;

            float duration = 0.775f;
            float p = EaseLinear(jumpingTimer, 0f, 1f, duration);
            float jp = p;// EaseInOutExpo(p, 0f, 1f, 1f);
            float jumpOffset = EaseJump(jp, 0f, 0.5f, 1f);
            float horizontalOffset = EaseLinear(p, 0f, 1f, 1f);

            transform.position = new Vector3(referenceX + horizontalOffset, referenceY + jumpOffset, transform.position.z);

            if (jumpingTimer >= duration)
            {
                isJumping = false;
                isWakingUp = false;
            }
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

    float EaseLinear(float t, float b, float c, float d)
    {
        float p = t / d;
        return b + p * c;
    }

    float EaseJump(float t, float b, float c, float d)
    {
        float p = t / d;
        //p = EaseCubicInOut(p, 0f, 1f, 1f);
        float a = (p - 0.5f) * 2f;
        float r = -(a * a) + 1f;
        return b + r * c;
    }

    float EaseCubicOut(float t, float b, float c, float d)
    {
        t /= d;
        t--;
        return c * (t * t * t + 1) + b;
    }

    float EaseInOutExpo(float t, float b, float c, float d)
    {
        t /= d / 2f;
        if (t < 1f) return c / 2f * (float)(Math.Pow(2f, 10f * (t - 1f))) + b;
        t--;
        return c / 2f * ((float)-Math.Pow(2f, -10f * t) + 2f) + b;
    }
}
