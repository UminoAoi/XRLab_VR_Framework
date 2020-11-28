using Assets.Scripts._1MiniGame;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObjectScript : Interactable
{
    private ItemType lightningItemType = ItemType.Blitz;

    public override void OnInteraction(HandManager handManager, PointerEventArgs args)
    {
        MiniGame1Manager.Instance.OnInteract(lightningItemType);
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) { }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) { }
}
