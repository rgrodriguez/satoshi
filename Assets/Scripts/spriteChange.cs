using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteChange : MonoBehaviour
{
    public Sprite rightWalk;
    public Sprite downWalk;
    public Sprite leftWalk;
    public Sprite upWalk;
    public Sprite rightWalkAlt;
    public Sprite downWalkAlt;
    public Sprite leftWalkAlt;
    public Sprite upWalkAlt;

    float timer = 1f;
    float delay = 1f;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKey(KeyCode.D))) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == rightWalk) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = rightWalkAlt;
             }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = rightWalk;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == downWalk)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = downWalkAlt;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = downWalk;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == leftWalk)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = leftWalkAlt;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = leftWalk;
            }
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == upWalk)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = upWalkAlt;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = upWalk;
            }
        }
    }
}
