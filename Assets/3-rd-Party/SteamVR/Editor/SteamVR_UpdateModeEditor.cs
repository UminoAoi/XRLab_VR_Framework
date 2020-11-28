using Assets.SteamVR.Input;
using UnityEditor;
using UnityEngine;

namespace Assets.SteamVR.Editor
{
    [CustomPropertyDrawer(typeof(SteamVR_UpdateModes))]
    public class SteamVR_UpdateModesEditor : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField(_position, _label, _property.intValue, _property.enumNames);
        }
    }
}