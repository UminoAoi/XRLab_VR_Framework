using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : Interactable
{
    public Animator animator;
    public GameObject blocker;


    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        animator.SetTrigger("open");
        blocker.SetActive(false);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
    }

}
