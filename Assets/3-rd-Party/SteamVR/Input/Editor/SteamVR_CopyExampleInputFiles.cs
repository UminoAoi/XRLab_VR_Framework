using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.SteamVR.Input.Editor
{
    public class SteamVR_CopyExampleInputFiles : UnityEditor.Editor
    {
        public const string steamVRInputExampleJSONCopiedKey = "SteamVR_Input_CopiedExamples";

        public const string exampleJSONFolderParent = "Input";
        public const string exampleJSONFolderName = "ExampleJSON";

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnReloadScripts()
        {
            SteamVR_Input.CheckOldLocation();
            CopyFiles();
        }

        public static void CopyFiles(bool force = false)
        {
            bool hasCopied = EditorPrefs.GetBool(steamVRInputExampleJSONCopiedKey, false);
            if (hasCopied == false || force == true)
            {
                string actionsFilePath = SteamVR_Input.GetActionsFilePath();
                bool exists = File.Exists(actionsFilePath);
                if (exists == false)
                {
                    string steamVRFolder = Scripts.SteamVR.GetSteamVRFolderPath();
                    string exampleLocation = Path.Combine(steamVRFolder, exampleJSONFolderParent);
                    string exampleFolderPath = Path.Combine(exampleLocation, exampleJSONFolderName);

                    string streamingAssetsPath = SteamVR_Input.GetActionsFileFolder();

                    string[] files = Directory.GetFiles(exampleFolderPath, "*.json");
                    foreach (string file in files)
                    {
                        string filename = Path.GetFileName(file);

                        string newPath = Path.Combine(streamingAssetsPath, filename);

                        try
                        {
                            File.Copy(file, newPath, false);
                            Debug.Log("<b>[SteamVR]</b> Copied example input JSON to path: " + newPath);
                        }
                        catch
                        {
                            Debug.LogError("<b>[SteamVR]</b> Could not copy file: " + file + " to path: " + newPath);
                        }
                    }

                    EditorPrefs.SetBool(steamVRInputExampleJSONCopiedKey, true);
                }
            }
        }
    }
}