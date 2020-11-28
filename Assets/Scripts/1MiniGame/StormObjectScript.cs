using System.Collections;
using System.Collections.Generic;
using Assets.Scripts._1MiniGame;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using UnityEngine;

public class StormObjectScript : Interactable
{
    private ItemType stormItemType = ItemType.Sturm;

    public override void OnInteraction(HandManager handManager, PointerEventArgs args)
    {
        MiniGame1Manager.Instance.OnInteract(stormItemType);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) { }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) { }
}
