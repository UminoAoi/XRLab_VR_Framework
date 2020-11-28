using System.Collections;
using Assets.SteamVR.Input;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using UnityEngine;

namespace Assets.Scripts.VR
{
    public class HandManager : MonoBehaviour {
        public enum InteractionType {
            Touch,
            Point
        }

        private Hand hand;
        private bool isControllerAttached, isLaserAttached;
        public GameObject controller;
        [HideInInspector]
        public SteamVR_LaserPointer laser;
        public SteamVR_Action_Boolean trigger = SteamVR_Input.GetBooleanAction("InteractUI");
        public SteamVR_Action_Boolean swap = SteamVR_Input.GetBooleanAction("GrabGrip");
        // Start is called before the first frame update
        void Start()
        {
            hand = GetComponent<Hand>();
            StartCoroutine(Initialize());
        }

        private IEnumerator Initialize() {
            laser = GetComponent<SteamVR_LaserPointer>();
            yield return new WaitForSeconds(0.1f);
            if (hand.handType == SteamVR_Input_Sources.RightHand) {
                AttachController();
                hand.otherHand.GetComponent<HandManager>().DetachController();
                AttachLaser();
            } else {
                DetachController();
                hand.otherHand.GetComponent<HandManager>().AttachController();
                DetachLaser();
            }
        }

        public void AlignLaser() {
            if (isLaserAttached)
                laser.AlignLaser(isControllerAttached);
        }

        public void AttachLaser() {
            isLaserAttached = true;
            laser.active = true;
            AlignLaser();
        }

        public void DetachLaser() {
            isLaserAttached = false;
            laser.active = false;
        }

        public void SwitchHandWithLaser() {
            if (isLaserAttached) {
                DetachLaser();
                hand.otherHand.GetComponent<HandManager>().AttachLaser();
            } else {
                AttachLaser();
                hand.otherHand.GetComponent<HandManager>().DetachLaser();
            }
        }

        public void AttachController() {
            isControllerAttached = true;
            hand.isControllerActive = true;
            controller.SetActive(true);
            hand.SetTemporarySkeletonRangeOfMotion(SkeletalMotionRangeChange.WithController);
            AlignLaser();
        }

        public void DetachController() {
            isControllerAttached = false;
            hand.isControllerActive = false;
            controller.SetActive(false);
            hand.SetTemporarySkeletonRangeOfMotion(SkeletalMotionRangeChange.WithoutController);
            AlignLaser();
        }

        public void SwitchHandWithController() {
            if (isControllerAttached) {
                DetachController();
                hand.otherHand.GetComponent<HandManager>().AttachController();
            } else {
                AttachController();
                hand.otherHand.GetComponent<HandManager>().DetachController();
            }
        }

        void Update()
        {
            if (swap.GetStateDown(hand.handType) && !isLaserAttached && !GetComponent<PickUpManager>().grabbing) {
                SwitchHandWithController();
                SwitchHandWithLaser();
            }
        }

    }
}
