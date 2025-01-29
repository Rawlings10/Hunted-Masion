using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ppMovement : MonoBehaviour
{
    Rigidbody rb;
    float speed = 10f;
    float rotation = 90f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.transform.Rotate(Vector3.up * Time.deltaTime * rotation);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.transform.Rotate(Vector3.down * Time.deltaTime * rotation);
        }
    }
}
