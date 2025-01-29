using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerMovementSpeed = 7f;
    float playerRotationSpeed = 90f;
    bool fistFighting;
    Animator animator;
    Rigidbody rb;
    public AudioSource walkingSound;
    public AudioSource punchingSound;
    public AudioSource kickingSound;
    public Gun gun;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        walkingSound = GetComponent<AudioSource>();
        punchingSound = GetComponent<AudioSource>();
        kickingSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bool Moving = Input.GetButton("Horizontal") || Input.GetButton("Vertical");
        animator.SetInteger("isMoving", 0);

        if (Input.GetButton("Horizontal"))
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                rb.transform.Rotate(new Vector3(0, 1, 0) * playerRotationSpeed * Time.deltaTime);
                animator.SetInteger("isMoving", 3);
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                rb.transform.Rotate(new Vector3(0, -1, 0) * playerRotationSpeed * Time.deltaTime);
                animator.SetInteger("isMoving", 2);
            }
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }

        if (Input.GetButton("Vertical"))
        {
            if(Input.GetAxis("Vertical") > 0) 
            {
                rb.transform.Translate(new Vector3(0, 0, 1) * playerMovementSpeed * Time.deltaTime);
                animator.SetInteger("isMoving", 4);
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                rb.transform.Translate(new Vector3(0, 0, -1) * playerMovementSpeed * Time.deltaTime);
                animator.SetInteger("isMoving", 1);
            }
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }

        if (Moving)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isKick");
            if (fistFighting)
            {
                if (!kickingSound.isPlaying)
                {
                    kickingSound.Play();
                    fistFighting = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetTrigger("isPunch");
            if (fistFighting)
            {
                punchingSound.Play();
                fistFighting = false;
            }
        }

        else
        {
            animator.SetBool("isShooting", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Cube")
        {
            fistFighting = true;
            Debug.Log("Fist Fighting");
        }

        Debug.Log("Collision with: " + collision.gameObject.name);
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        fistFighting = false;
    //    }
    //}
}
