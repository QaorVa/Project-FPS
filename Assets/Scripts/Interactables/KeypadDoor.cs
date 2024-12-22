using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoor : Interactable
{
    [SerializeField] private GameObject door;
    [SerializeField] private int usageCount = 1;
    private bool isOpen = false;
    private Collider interactCollider;
    // Start is called before the first frame update
    void Start()
    {
        interactCollider = gameObject.GetComponent<Collider>();
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
        isOpen = false;
    }

    public void OpenDoor()
    {

        door.GetComponent<Animator>().Play("Opened");
        isOpen = true;
        
    }
}
