//======= Copyright (c) Valve Corporation, All rights reserved. ===============

using System.Collections;
using Assets.SteamVR.Input;
using UnityEngine;

namespace Assets.Scripts.VR
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        Transform previousContact = null;


        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.Euler(45, 0, 0);

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update() {
            if (!isActive) {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            if (!active) {
                holder.SetActive(false);
            } else {

                holder.SetActive(true);
                float dist = 100f;
                float x = pointer.transform.position.x - transform.position.x;
                float y = pointer.transform.position.y - transform.position.y;
                float z = pointer.transform.position.z - transform.position.z;
                Vector3 goal = new Vector3(x, y, z);
                Ray raycast = new Ray(transform.position, goal);
                RaycastHit hit;
                bool bHit = Physics.Raycast(raycast, out hit);

                if (previousContact && previousContact != hit.transform) {
                    PointerEventArgs args = new PointerEventArgs();
                    args.fromInputSource = pose.inputSource;
                    args.distance = 0f;
                    args.flags = 0;
                    args.target = previousContact;
                    //OnPointerOut(args);
                    OnPointOut(args);
                    previousContact = null;
                }
                if (bHit && previousContact != hit.transform) {
                    PointerEventArgs argsIn = new PointerEventArgs();
                    argsIn.fromInputSource = pose.inputSource;
                    argsIn.distance = hit.distance;
                    argsIn.flags = 0;
                    argsIn.target = hit.transform;
                    //OnPointerIn(argsIn);
                    OnPointIn(argsIn);
                    previousContact = hit.transform;
                }
                if (!bHit) {
                    previousContact = null;
                }
                if (bHit && hit.distance < 100f) {
                    dist = hit.distance;
                }

                if (bHit && interactWithUI.GetStateUp(pose.inputSource)) {
                    PointerEventArgs argsClick = new PointerEventArgs();
                    argsClick.fromInputSource = pose.inputSource;
                    argsClick.distance = hit.distance;
                    argsClick.flags = 0;
                    argsClick.target = hit.transform;
                    OnInteracting(argsClick);
                    //OnPointerClick(argsClick);
                }

                if (interactWithUI != null && interactWithUI.GetState(pose.inputSource)) {
                    pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                    pointer.GetComponent<MeshRenderer>().material.color = clickColor;
                } else {
                    pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                    pointer.GetComponent<MeshRenderer>().material.color = color;
                }
                pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
            }
        }
        public void AlignLaser(bool withController) {
            if (withController)
            {
                if (SteamVR.Scripts.SteamVR.instance.hmd_ModelNumber.Equals("Oculus Quest"))
                    holder.transform.localPosition = new Vector3(0f, 0f, -0.045f);
                else
                    holder.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
                holder.transform.localPosition = new Vector3(-0.035f, -0.05f, -0.01f);
        }

        public void OnPointIn(PointerEventArgs args) {
            Interactable.Interactable interactable = args.target.GetComponent<Interactable.Interactable>();
            if(interactable != null) {
                interactable.OnPointerEnter(GetComponent<HandManager>(), args);
            }
        }
        public void OnPointOut(PointerEventArgs args) {
            Interactable.Interactable interactable = args.target.GetComponent<Interactable.Interactable>();
            if (interactable != null) {
                interactable.OnPointerExit(GetComponent<HandManager>(), args);
            }
        }

        public void OnInteracting(PointerEventArgs args) {
            Interactable.Interactable interactable = args.target.GetComponent<Interactable.Interactable>();
            if (interactable != null) {
                interactable.OnInteraction(GetComponent<HandManager>(), args);
            }
        }
    }


    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}