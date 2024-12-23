using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoor : Interactable
{
    [SerializeField] private GameObject door;
    [SerializeField] private int usageCount = 1;
    private bool isOpen = false;
    private Collider interactCollider;

    private AudioSource audioSource;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip interactSound;
    // Start is called before the first frame update
    void Start()
    {
        interactCollider = gameObject.GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(usageCount <= 0)
        {
            interactCollider.enabled = false;
        }
    }

    protected override void Interact()
    {
        if(usageCount <= 0)
        {
            return;
        }
        PlaySound(interactSound);

        usageCount--;

        if(isOpen == false)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
        
    }

    public void CloseDoor()
    {
        door.GetComponent<Animator>().Play("Closed");
        PlaySound(doorCloseSound);
        isOpen = false;
    }

    public void OpenDoor()
    {

        door.GetComponent<Animator>().Play("Opened");
        PlaySound(doorOpenSound);
        isOpen = true;
        
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
