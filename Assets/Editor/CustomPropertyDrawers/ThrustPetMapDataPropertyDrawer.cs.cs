using UnityEngine;
using UnityEditor;

namespace Digx7.ThrustPets
{
    // TODO (Optional): Toggle comment if you want to implement a custom Property Drawer for the ThrustPetMapData Scriptable Object

//    [CustomPropertyDrawer(typeof(ThrustPetMap))]
//    public class ThrustPetMapStructPropertyDrawer: PropertyDrawer {
//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) 
//        {
//            base.OnGUI(position, property, label);
//
//            // TODO (Optional): Display custom data here 
//
//            EditorGUI.BeginProperty(position, label, property);
//
//            Rect locationPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
//            EditorGUI.PropertyField(locationPosition, property);
//
//            ThrustPetMapData myTarget = (ThrustPetMapData)property.objectReferenceValue;
//
//            if(myTarget != null)
//            {
//                // TODO (Optional): Display custom data here 
//            }
//            else
//            {
//                GUIStyle errorStyle = new GUIStyle(EditorStyles.label);
//                errorStyle.normal.textColor = Color.red;
//                
//                Rect spriteMissingPosition = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
//                EditorGUI.LabelField(spriteMissingPosition, "Error!  No ThrustPetMapData is set, this will cause a bug", errorStyle);
//
//            }
//
//
//             EditorGUI.EndProperty();
//        }
//
//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            return base.GetPropertyHeight(property, label);
//
//            // TODO (Optional): adjust the height based on data
//
//            // float height = EditorGUIUtility.singleLineHeight * 2;
//
//            //  return height;
//        }
//    }
}