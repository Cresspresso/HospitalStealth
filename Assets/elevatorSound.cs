using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorSound : MonoBehaviour
{
    public AudioClip audioClip;

    private Animator doorAnimator;
    private AudioSource audioSource;

    private bool played = false;

    // Start is called before the first frame update
    void Awake()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("L_DoorElevator"))
        {
            if (!played)
            {
                audioSource.PlayOneShot(audioClip);
                played = true;
            }
        }
    }
}
