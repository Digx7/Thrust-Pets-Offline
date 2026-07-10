using UnityEngine;
using UnityEditor;

namespace Digx7.ThrustPets
{    
    [CustomEditor(typeof(ThrustPetMapData))]
    public class ThrustPetMapDataCustomInspector : Editor 
    {
        
        // TODO: Add your serialized properties here        
        // SerializedProperty _fooProp;

        void OnEnable()
        {
            // TODO: initialize your SerializedProperty's here
            //_fooProp = serializedObject.FindProperty("_foo");
        }

        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();

            ThrustPetMapData myData = target as ThrustPetMapData;

            // TODO (Optional): comment out the 'base.OnInspectorGUI()' line to fully create your own inspector
            // Use the following code as a starting point

            // EditorGUILayout.PropertyField(_fooProp);

            // // Apply changes to the serializedProperty - always do this at the end of OnInspectorGUI.
            // serializedObject.ApplyModifiedProperties();
        }
    }
}