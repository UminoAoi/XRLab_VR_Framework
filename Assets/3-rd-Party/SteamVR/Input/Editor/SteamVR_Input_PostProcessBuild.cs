﻿using System.IO;
using Assets.SteamVR.Scripts;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Assets.SteamVR.Input.Editor
{
    public class SteamVR_Input_PostProcessBuild
    {
        [PostProcessBuild(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            
        }

        private static void UpdateAppKey(string newFilePath, string executableName)
        {
            if (File.Exists(newFilePath))
            {
                string jsonText = System.IO.File.ReadAllText(newFilePath);

                string findString = "\"app_key\" : \"";
                int stringStart = jsonText.IndexOf(findString);

                if (stringStart == -1)
                {
                    findString = findString.Replace(" ", "");
                    stringStart = jsonText.IndexOf(findString);

                    if (stringStart == -1)
                        return; //no app key
                }

                stringStart += findString.Length;
                int stringEnd = jsonText.IndexOf("\"", stringStart);

                int stringLength = stringEnd - stringStart;

                string currentAppKey = jsonText.Substring(stringStart, stringLength);

                if (string.Equals(currentAppKey, SteamVR_Settings.instance.editorAppKey, System.StringComparison.CurrentCultureIgnoreCase) == false)
                {
                    jsonText = jsonText.Replace(currentAppKey, SteamVR_Settings.instance.editorAppKey);

                    FileInfo file = new FileInfo(newFilePath);
                    file.IsReadOnly = false;

                    File.WriteAllText(newFilePath, jsonText);
                }
            }
        }
    }
}