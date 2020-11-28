using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Room
{
    [RequireComponent(typeof(Animator))]
    public class LevelChanger : MonoBehaviour
    {
        /// <summary>
        /// fadeIn/fadeOut animation on canvas
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Scene to load after fade out complited
        /// </summary>
        private string sceneName;

        public void Start()
        {
            animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Method turn on fade out animation
        /// </summary>
        /// <param name="sceneName"></param>
        public void ChangeLevel(string sceneName)
        {
            this.sceneName = sceneName;
            animator.SetTrigger("FadeOut");
        }

        /// <summary>
        /// Method turn on fade out animation
        /// </summary>
        /// <param name="isToNextRoom"></param>
        public void ChangeLevel(bool isToNextRoom)
        {
            string currentRoom = GetCurrentRoom();
            string nextRoom = GetNextRoom(currentRoom, isToNextRoom);

            sceneName = nextRoom;

            animator.SetTrigger("FadeOut");
        }

        private string GetNextRoom(string currentRoomNumber, bool isToNextRoom)
        {
            int currentRoomInt = int.Parse(currentRoomNumber);
            int nextRoomInt = isToNextRoom ? currentRoomInt + 1 : currentRoomInt - 1;
            string nextRoomString = nextRoomInt + "";
            if (nextRoomInt < 10)
            {
                nextRoomString = nextRoomString.Insert(0, "0");
                nextRoomString = nextRoomString.Insert(0, "0");
            }
            if (nextRoomInt >= 10 && nextRoomInt < 100)
            {
                nextRoomString = nextRoomString.Insert(0, "0");
            }
            nextRoomString = nextRoomString.Insert(0, "R_");

            if (IsSceneExists(nextRoomString))
                return nextRoomString;

            return "R_000";
        }

        private bool IsSceneExists(string nextRoom)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            List<string> scenes = new List<string>();
            for (int i = 0; i < sceneCount; i++)
            {
                scenes.Add(System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i)));
            }
            return scenes.Contains(nextRoom);
        }

        private string GetCurrentRoom()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var splited = currentScene.Split('_');
            return splited[1];
        }

        /// <summary>
        /// Method is called after fade out animation
        /// </summary>
        public void OnFadeComplited()
        {
            try
            {
                SceneManager.LoadScene(sceneName);
            }
            catch (Exception e)
            {
                SceneManager.LoadScene("R_000");
            }
        }


    }
}
