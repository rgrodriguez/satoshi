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

    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            if (this.gameObject.GetComponent<SpriteRenderer>().sprite == rightWalk) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = rightWalkAlt;
             }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = rightWalk;
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
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
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
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
