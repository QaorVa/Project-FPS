using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadDoor : Interactable
{
    [SerializeField] private GameObject door;
    private bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Interact()
    {
        isOpen = !isOpen;
        door.GetComponent<Animator>().SetBool("isOpen", isOpen);
    }
}
