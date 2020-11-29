using System.Collections;
using Assets.SteamVR.Input;
using Assets.SteamVR.InteractionSystem.Core.Scripts;
using Assets.SteamVR.InteractionSystem.Hints.Scripts;
using UnityEngine;

namespace Assets.Scripts.VR
{
    public class ControllerHints : MonoBehaviour {

        public bool cycling; // if you want the scripts to turn on one by one
        private bool cyclingStarted = false;
        public Hint[] hints;
        private float initialTimer = 0.5f;

        [System.Serializable]
        public struct Hint {
            public string action;
            public bool active, timed; // active needed for enabling the hint, timed for being on timer
            [HideInInspector]
            public bool started, stopped;
            public float timer; // needed both for cycling and regular hint usage
            public string text;
            [HideInInspector]
            public ISteamVR_Action_In act;
        }
        private void Start() {
            for (int i = 0; i < hints.Length; i++) {
                hints[i].stopped = true;
                hints[i].act = null;
                for (int actionIndex = 0; actionIndex < SteamVR_Input.actionsIn.Length; actionIndex++) {
                    ISteamVR_Action_In action = SteamVR_Input.actionsIn[actionIndex];
                    if (action.GetShortName().Equals(hints[i].action))
                        hints[i].act = action;
                }
            }
        }

        public void StartHint(int i) {
            hints[i].started = true;
            hints[i].stopped = false;
            if (hints[i].text != null && !hints[i].text.Equals("") && hints[i].act != null) {
                ControllerButtonHints.ShowTextHint(GetComponent<Hand>(), hints[i].act, hints[i].text);
            } else {
                ControllerButtonHints.ShowButtonHint(GetComponent<Hand>(), hints[i].act);
            }
        }

        public void StopHint(int i) {
            hints[i].stopped = true;
            hints[i].started = false;
            if (hints[i].text != null && !hints[i].text.Equals("")) {
                ControllerButtonHints.HideTextHint(GetComponent<Hand>(), hints[i].act);
            } else {
                ControllerButtonHints.HideButtonHint(GetComponent<Hand>(), hints[i].act);
            }
        }

        private IEnumerator Cycle() {
            for (int i = 0; i < hints.Length; i++) {
                if (hints[i].active) {
                    StartHint(i);
                    yield return new WaitForSeconds(hints[i].timer);
                    hints[i].active = false;
                    StopHint(i);
                }
            }
            cycling = false;
            cyclingStarted = false;
        }

        private void Update() {
            if (initialTimer > 0)
                initialTimer -= Time.deltaTime;
            else {
                if(cycling && !cyclingStarted) {
                    cyclingStarted = true;
                    StartCoroutine(Cycle());
                } else if(!cycling) {
                    for (int i = 0; i < hints.Length; i++) {
                        if (!hints[i].active) {
                            if (!hints[i].stopped)
                                StopHint(i);
                        } else {
                            if (!hints[i].started)
                                StartHint(i);
                            else if (hints[i].timed) {
                                if (hints[i].timer > 0)
                                    hints[i].timer -= Time.deltaTime;
                                else if (!hints[i].stopped)
                                    hints[i].active = false;
                            }
                        }
                    }
                }
            }
        }

    }
}
