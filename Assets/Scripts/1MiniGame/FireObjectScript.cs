using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._1MiniGame;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using UnityEngine;

public class FireObjectScript : Interactable
{
    private ItemType fireItemType = ItemType.Feuer;

    public override void OnInteraction(HandManager handManager, PointerEventArgs args)
    {
        MiniGame1Manager.Instance.OnInteract(fireItemType);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) { }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) { }
}
