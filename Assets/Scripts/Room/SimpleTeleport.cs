using Assets.Scripts.ColorChanging;
using Assets.Scripts.VR;
using Assets.Scripts.VR.Interactable;
using UnityEngine;

namespace Assets.Scripts.Room
{
    [RequireComponent(typeof(HighlightChanger))]
    public class SimpleTeleport : Interactable
    {
        public int teleportId;
        /// <summary>
        /// Scene to teleport
        /// </summary>
        public string sceneName;
        public bool isEnabled;

        protected LevelChanger levelChanger;

        protected HighlightChanger highlightChanger;


        private void Start()
        {
            highlightChanger = GetComponent<HighlightChanger>();
            levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        }

        /// <summary>
        /// Method is called after laser click on object
        /// </summary>
        public virtual void ChangeRoom()
        {

            if (IsAllowed())
            {
                levelChanger.ChangeLevel(sceneName);
            }
        }


        public override void OnInteraction(HandManager handManager, PointerEventArgs args)
        {
            ChangeRoom();
        }

        public override void OnPointerEnter(HandManager handManager, PointerEventArgs args)
        {
            highlightChanger.DoHighlight();
        }

        public override void OnPointerExit(HandManager handManager, PointerEventArgs args)
        {
            highlightChanger.MakeDefaultState();
        }

        public virtual bool IsAllowed()
        {
            return isEnabled;
        }
    }
}
