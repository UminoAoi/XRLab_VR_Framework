using Assets.Scripts.ColorChanging;
using UnityEngine;

namespace Assets.Scripts.VR.Interactable
{
    [RequireComponent(typeof(HighlightChanger))]
    public class Grabbable : Interactable {

        [HideInInspector]
        public PickUpManager grabber, tempGrabber;
        public bool rescale = false;
        public Vector3 scaleInHand;
        HighlightChanger highlightChanger;
        [HideInInspector]
        public bool scalingUpToHand = false, scalingUpToFull = false, scalingDown = false, beingGrabbed = false;
        public float scaleSpeed = 0.1f;
        private Vector3 originalScale;
        // Start is called before the first frame update
        void Start() {
            highlightChanger = GetComponent<HighlightChanger>();
            originalScale = transform.localScale;
            if (!rescale)
                scaleInHand = transform.localScale;
        }

        // Update is called once per frame
        void Update() {
            if (grabber != null) {
                transform.position = grabber.transform.position;
                float x = grabber.transform.localRotation.x;
                x += 0.4f;
                transform.localRotation = new Quaternion(x, grabber.transform.localRotation.y, grabber.transform.localRotation.z, grabber.transform.localRotation.w);
            }
            if (scalingUpToHand)
                ScaleUp(true);
            else if (scalingUpToFull)
                ScaleUp(false);
            else if (scalingDown)
                ScaleDown();
        }
        override public void OnPointerEnter(HandManager handManager, PointerEventArgs args) {
            highlightChanger.DoHighlight();
            //transform.localScale = new Vector3(2, 2, 2);
        }
        override public void OnPointerExit(HandManager handManager, PointerEventArgs args) {
            highlightChanger.MakeDefaultState();
            //transform.localScale = new Vector3(1, 1, 1);
        }
        override public void OnInteraction(HandManager handManager, PointerEventArgs args) {
            highlightChanger.MakeDefaultState();
        
            handManager.GetComponent<PickUpManager>().PickUp(this);
        }

        public void ScaleUp(bool toHand) {
            Vector3 upperLimit;
            float currSpeed;
            if (toHand) {
                upperLimit = scaleInHand;
                currSpeed = scaleSpeed;
            } else {
                upperLimit = originalScale;
                currSpeed = scaleSpeed / 3;
            }
            transform.localScale = Vector3.MoveTowards(transform.localScale, upperLimit, currSpeed * (upperLimit - transform.localScale).magnitude);
            if ((transform.localScale - upperLimit).magnitude < 0.05f * upperLimit.magnitude) {
                transform.localScale = upperLimit;
                scalingUpToHand = false;
                scalingUpToFull = false;
            }
        }

        public void ScaleDown() {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, scaleSpeed * transform.localScale.magnitude);
            if (transform.localScale.magnitude < 0.05f * originalScale.magnitude) {
                scalingDown = false;
                grabber = tempGrabber;
                scalingUpToHand = true;
            }
        }

    }
}
