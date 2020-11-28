using Assets.Scripts._1MiniGame;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : Interactable {

    [SerializeField]
    private ItemType itemType;
    MiniGame1Manager manager;

    private void Start() {
        manager = FindObjectOfType<MiniGame1Manager>();
    }

    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        manager.OnInteract(itemType);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
        throw new System.NotImplementedException();
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
        throw new System.NotImplementedException();
    }

}
