using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorOpen : MonoBehaviour
{
    public bool enter;
    private AudioSource source;
    int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        enter = false;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
        if (enter && count == 1)
        {
            source.Play();
            count -= 1;
            Debug.Log("HELLO");
        }
    }
}
