using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPortal : Interactable
{
    private PlayerUI playerUI;

    private void Start()
    {
        playerUI = FindObjectOfType<PlayerUI>();
    }

    protected override void Interact()
    {
        playerUI.ShowWinUI();
    }
}
