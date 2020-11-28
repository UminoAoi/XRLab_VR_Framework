using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FindItem : Interactable
{
    TextMeshPro text;
    HotPotManager manager;

    private void Awake() {
        manager = FindObjectOfType<HotPotManager>();
        text = GetComponentInChildren<TextMeshPro>();
    }


    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        manager.ObjectInteraction();
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
    }

    internal void HideText() {
        text.gameObject.SetActive(false);
    }
}
