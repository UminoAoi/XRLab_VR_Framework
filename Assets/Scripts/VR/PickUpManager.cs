using System.Collections;
using Assets.Scripts.VR.Interactable;
using Assets.SteamVR.Input;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

namespace Assets.Scripts.VR
{
    public class PickUpManager : MonoBehaviour {

        public SteamVR_Action_Boolean grabAction = null;
        [HideInInspector]
        public bool grabbing = false;

        private Grabbable currentItem = null;
        private GameObject[] contactGrabbables;

        private float timer = 0;
        private int layer;

        public float maxTouchDistance, maxLaserDistance;
        public float releaseVelocityTimeOffset = -0.011f;


        private bool lerping = false;
        public GrabType grabType;
        public float lerpSpeed;

        private void Start() {
            contactGrabbables = GameObject.FindGameObjectsWithTag("Grabbable");
        }


        private void Update() {
            if (lerping) {
                currentItem.transform.position = Vector3.MoveTowards(currentItem.transform.position, transform.position, Time.deltaTime * lerpSpeed);
                if (Vector3.Distance(transform.position, currentItem.transform.position) < 0.1f) {
                    lerping = false;
                    currentItem.grabber = this;
                    currentItem.GetComponent<Rigidbody>().useGravity = true;
                }
            } else if (grabAction.GetStateUp(GetComponent<Hand>().handType) && currentItem != null && timer <= 0) {
                Drop();
            } else if (grabAction.GetStateDown(GetComponent<Hand>().handType)) {
                PickUp();
            }
            if (timer > 0)
                timer -= Time.deltaTime;
        }

        public void SnapGrab() {
            currentItem.transform.position = transform.position;
            currentItem.grabber = this;
            currentItem.transform.localScale = currentItem.scaleInHand;
        }
        public void FancySnapGrab() {
            currentItem.scalingUpToFull = false;
            currentItem.scalingDown = true;
            currentItem.tempGrabber = this;
        }
        public void LerpGrab() {
            lerping = true;
            currentItem.scalingUpToHand = true;
            currentItem.GetComponent<Rigidbody>().useGravity = false;
        }

        public void PickUp(Grabbable gr) {
            layer = gr.gameObject.layer;
            gr.gameObject.layer = 9;
            currentItem = gr;
            if (!currentItem || currentItem.beingGrabbed || (currentItem.transform.position - transform.position).magnitude > maxLaserDistance)
                return;

            timer = 0.2f;
            currentItem.beingGrabbed = true;

            switch(grabType) {
                case GrabType.LerpGrab:
                    LerpGrab();
                    break;
                case GrabType.SnapGrab:
                    SnapGrab();
                    break;
                case GrabType.FancySnapGrab:
                    FancySnapGrab();
                    break;
            }

            gr.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            gr.transform.localRotation = Quaternion.identity;
            currentItem.GetComponent<Rigidbody>().isKinematic = true;

            grabbing = true;
            GetComponent<SteamVR_LaserPointer>().active = false;

            Collider collider = currentItem.GetComponent<Collider>();
            collider.isTrigger = true;
       
        }

        public void PickUp() {
            Grabbable gr = GetNearest();
            if (!gr || (gr.transform.position - transform.position).magnitude > maxTouchDistance)
                return;
            PickUp(gr);
        }

        private IEnumerator LayerChange(Grabbable gr) {
            yield return new WaitForSeconds(0.5f);
            gr.gameObject.layer = layer;
        }

        public void Drop() {

            if (!currentItem || currentItem.grabber != this)
                return;
            Hand hand = GetComponent<Hand>();
            hand.skeleton.BlendToSkeleton();
            //Apply velocity
            Rigidbody target = currentItem.GetComponent<Rigidbody>();
            target.isKinematic = false;
            target.velocity = hand.GetTrackedObjectVelocity(releaseVelocityTimeOffset);
            target.angularVelocity = hand.GetTrackedObjectAngularVelocity(releaseVelocityTimeOffset);
            target.velocity.Scale(new Vector3(1.5f, 1.5f, 1.5f));

            Collider collider = currentItem.GetComponent<Collider>();
            StartCoroutine(LayerChange(currentItem));
            if(currentItem.rescale)
                currentItem.scalingUpToFull = true;
            collider.isTrigger = false;

            currentItem.grabber = null;
            currentItem.beingGrabbed = false;
            AfterDropping();

            currentItem = null;
            grabbing = false;
        }

        public void AfterDropping() {
            GetComponent<SteamVR_LaserPointer>().active = !GetComponent<Hand>().otherHand.GetComponent<SteamVR_LaserPointer>().active;
        }

        private Grabbable GetNearest() {
            Grabbable nearest = null;
            float minDistance = float.MaxValue;
            float distance;
            foreach (GameObject interaction in contactGrabbables) {
                if (interaction != null) {
                    distance = (interaction.transform.position - transform.position).sqrMagnitude;
                    if (distance < minDistance) {
                        nearest = interaction.GetComponent<Grabbable>();
                        minDistance = distance;
                    }
                }


            }
            return nearest;
        }

        public enum GrabType {
            LerpGrab, SnapGrab, FancySnapGrab
        }

    }
}
