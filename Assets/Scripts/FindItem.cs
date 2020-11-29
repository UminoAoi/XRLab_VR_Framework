using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindItem : Interactable
{
    HotPotManager manager;

    private void Awake() {
        manager = FindObjectOfType<HotPotManager>();
    }


    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        manager.ObjectInteraction();
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
    }

}
