// Editor-only, inside an Editor folder
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CreateScriptableObjectButtonAttribute))]
public class CreateScriptableObjectButtonDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attributeData = (CreateScriptableObjectButtonAttribute)attribute;
        var fieldType = fieldInfo.FieldType;

        bool isObjectReference = property.propertyType == SerializedPropertyType.ObjectReference;
        bool isScriptableObjectType = typeof(ScriptableObject).IsAssignableFrom(fieldType);
        bool canCreate = isObjectReference && isScriptableObjectType && !fieldType.IsAbstract;

        float buttonWidth = 60f;
        Rect fieldRect = new Rect(position.x, position.y, position.width - buttonWidth - 4f, position.height);
        Rect buttonRect = new Rect(fieldRect.xMax + 4f, position.y, buttonWidth, position.height);

        EditorGUI.PropertyField(fieldRect, property, label);

        using (new EditorGUI.DisabledScope(!canCreate || property.objectReferenceValue != null))
        {
            if (GUI.Button(buttonRect, "New"))
            {
                CreateAndAssignAsset(property, fieldType, attributeData);
            }
        }
    }

    private void CreateAndAssignAsset(SerializedProperty property, Type fieldType, CreateScriptableObjectButtonAttribute attributeData)
    {
        string folder = attributeData.FolderPath;
        if (!AssetDatabase.IsValidFolder(folder))
        {
            Debug.LogError($"Folder does not exist: {folder}");
            return;
        }

        var asset = ScriptableObject.CreateInstance(fieldType);

        string rawName = property.name.TrimStart('_');
        string propertyName = rawName.Length > 0 ? char.ToUpper(rawName[0]) + rawName.Substring(1) : rawName;
        string fileName = $"{attributeData.FileNamePrefix}{propertyName}.asset";
        string assetPath = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(folder, fileName));

        AssetDatabase.CreateAsset(asset, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        property.objectReferenceValue = asset;
        property.serializedObject.ApplyModifiedProperties();

        // EditorUtility.FocusProjectWindow();
        // Selection.activeObject = asset;
    }
}