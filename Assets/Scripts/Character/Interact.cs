using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interact : Interactable
{

    private void Awake() {
    }


    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        Destroy(this);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
    }

    internal void HideText() {
        // text.gameObject.SetActive(false);
    }
}