using System.Collections;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject OpenDoorText;         // UI Text prompt to open the door
    private Animation anim;                 // Animation component reference
    [SerializeField]AudioSource openDoorSound;
    [SerializeField] AudioSource closeDoorSound;
    private bool playerIsNearBy = false;    // Flag to check if player is nearby
    private bool doorIsOpen = false;        // Flag to check if the door is open

    private void Start()
    {
        openDoorSound = GetComponent<AudioSource>();
        closeDoorSound = GetComponent<AudioSource>();
        openDoorSound.pitch = 2.0f;
        // Attempt to fetch the Animation component from the same GameObject
        anim = GetComponentInChildren<Animation>();
        if (anim == null)
        {
            Debug.LogError("No Animation component found on the GameObject.");
        }

        if (OpenDoorText != null)
        {
            OpenDoorText.SetActive(false); // Ensure the prompt is hidden initially
        }
        else
        {
            Debug.LogError("OpenDoorText GameObject is not assigned.");
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "PLAYER")
        {
            if (OpenDoorText != null)
            {
                OpenDoorText.SetActive(true);
            }
            playerIsNearBy = true;
        }
    }

    private void Update()
    {
        if (playerIsNearBy && Input.GetKeyDown(KeyCode.E))
        {
            if (anim != null && !doorIsOpen)
            {
                PlayAnimationByIndex(0); // Open door animation
                if (!openDoorSound.isPlaying)
                {
                    openDoorSound.Play();
                }
                doorIsOpen = true;

                if (OpenDoorText != null)
                {
                    OpenDoorText.SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "PLAYER")
        {
            if (OpenDoorText != null)
            {
                OpenDoorText.SetActive(false);
            }

            if (doorIsOpen && anim != null)
            {
                PlayAnimationByIndex(1); // Close door animation
                if (!closeDoorSound.isPlaying)
                {
                    closeDoorSound.Play();
                }
                doorIsOpen = false;
            }

            playerIsNearBy = false;
        }
    }

    public void PlayAnimationByIndex(int index)
    {
        if (anim == null)
        {
            Debug.LogError("Animation component is not attached to the GameObject.");
            return;
        }

        int i = 0;
        foreach (AnimationState state in anim)
        {
            if (i == index)
            {
                anim.Play(state.name);
                return;
            }
            i++;
        }

        Debug.LogError($"Invalid animation index: {index}. Ensure the animation exists.");
    }
}
