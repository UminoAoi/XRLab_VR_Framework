using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;

using UnityEngine;

public class Interact : Interactable
{

    public GameObject GameLogic;
    RoomLogic referenceScript;

    private void Awake() 
    {
        GameLogic = GameObject.FindGameObjectWithTag("Logic");
        referenceScript = GameLogic.GetComponent<RoomLogic>();
    }


    public override void OnInteraction(HandManager handManager, PointerEventArgs args) {
        if(gameObject.GetComponent<Foe>().was_seen != 0)
            Destroy(gameObject);
            referenceScript.counter++;
    }

    public override void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
    }

    public override void OnPointerExit(HandManager handManager, PointerEventArgs args) {
    }

    internal void HideText() {
        // text.gameObject.SetActive(false);
    }
}