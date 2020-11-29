using System.Collections;
using Assets.Scripts.ColorChanging;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Room
{
    public class DoorTeleport : SimpleTeleport
    {
        [HideInInspector]
        public bool isToNextRoom;

        [SerializeField]
        protected Animator animator;

        [SerializeField]
        protected string animationName;



        private void Start()
        {
            highlightChanger = GetComponent<HighlightChanger>();
            levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
        }

        public override void ChangeRoom()
        {
            if (IsAllowed())
            {
                animator.Play(animationName);
                StartCoroutine(OnAnimationCompleted());
            }
        }

        public IEnumerator OnAnimationCompleted()
        {
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;
            float time = 0;

            for (int i = 0; i < ac.animationClips.Length; i++)               
            {
                if (ac.animationClips[i].name == animationName)        
                    time = ac.animationClips[i].length;
            }

            yield return new WaitForSeconds(time);

            if(string.IsNullOrEmpty(sceneName))
                levelChanger.ChangeLevel(isToNextRoom);
            else
                levelChanger.ChangeLevel(sceneName);
        }



#if UNITY_EDITOR
        [CustomEditor(typeof(DoorTeleport))]
        public class DoorTeleport_Editor : Editor {
            public override void OnInspectorGUI() {
                DrawDefaultInspector(); 
                DoorTeleport script = (DoorTeleport)target;
                if (script.sceneName.Equals(""))
                    script.isToNextRoom = EditorGUILayout.Toggle("Go to the next room ",script.isToNextRoom);
            }
        }
#endif






    }
}
