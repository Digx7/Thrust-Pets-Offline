// using System;
// using System.Collections;
// using System.IO;
// using System.Reflection;
// using UnityEditor;
// using UnityEditor.Events;
// using UnityEngine;
// using UnityEngine.Events;

// [CustomPropertyDrawer(typeof(ConnectChannelButtonAttribute))]
// public class ConnectChannelButtonDrawer : PropertyDrawer
// {
//     private const float ButtonHeight = 20f;
//     private const float Padding = 2f;

//     public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//     {
//         return EditorGUI.GetPropertyHeight(property, label, true) + Padding + ButtonHeight;
//     }

//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         var attr = (ConnectChannelButtonAttribute)attribute;

//         float eventHeight = EditorGUI.GetPropertyHeight(property, label, true);
//         Rect eventRect   = new Rect(position.x, position.y,                          position.width, eventHeight);
//         Rect buttonRect  = new Rect(position.x, position.y + eventHeight + Padding,  position.width, ButtonHeight);

//         EditorGUI.PropertyField(eventRect, property, label, true);

//         // Resolve the UnityEvent<T> type argument so we can validate Raise() exists
//         Type[] genericArgs = GetUnityEventTypeArguments(fieldInfo.FieldType);
//         Type argType = genericArgs.Length > 0 ? genericArgs[0] : null;
//         MethodInfo raiseMethod = argType == null
//             ? attr.ChannelType.GetMethod("Raise", Type.EmptyTypes)
//             : attr.ChannelType.GetMethod("Raise", new[] { argType });

//         bool canConnect = raiseMethod != null;

//         using (new EditorGUI.DisabledScope(!canConnect))
//         {
//             string buttonLabel = canConnect
//                 ? $"Create & Connect {attr.ChannelType.Name}"
//                 : $"No Raise({argType?.Name ?? "void"}) found on {attr.ChannelType.Name}";

//             if (GUI.Button(buttonRect, buttonLabel))
//                 CreateAndConnect(property, attr, argType, raiseMethod);
//         }
//     }

//     // -----------------------------------------------------------------------

//     private void CreateAndConnect(SerializedProperty property, ConnectChannelButtonAttribute attr,
//                                   Type argType, MethodInfo raiseMethod)
//     {
//         if (!AssetDatabase.IsValidFolder(attr.FolderPath))
//         {
//             Debug.LogError($"ConnectChannelButton: Folder does not exist: {attr.FolderPath}");
//             return;
//         }

//         // Create the channel asset, named after the property
//         var channel = ScriptableObject.CreateInstance(attr.ChannelType);
//         string rawName     = property.name.TrimStart('_');
//         string propertyName = rawName.Length > 0 ? char.ToUpper(rawName[0]) + rawName.Substring(1) : rawName;
//         string assetPath   = AssetDatabase.GenerateUniqueAssetPath(
//             Path.Combine(attr.FolderPath, $"{attr.FileNamePrefix}{propertyName}.asset"));

//         AssetDatabase.CreateAsset(channel, assetPath);
//         AssetDatabase.SaveAssets();

//         // Retrieve the actual UnityEvent instance from the target object via reflection
//         var unityEvent = GetTargetObjectOfProperty(property) as UnityEvent;
//         if (unityEvent == null)
//         {
//             Debug.LogError("ConnectChannelButton: Could not retrieve UnityEvent from property.");
//             AssetDatabase.DeleteAsset(assetPath);
//             return;
//         }

//         Undo.RecordObject(property.serializedObject.targetObject, "Create & Connect Channel");

//         AddPersistentListener(unityEvent, channel, argType, raiseMethod);

//         property.serializedObject.ApplyModifiedProperties();
//         EditorUtility.SetDirty(property.serializedObject.targetObject);
//         AssetDatabase.SaveAssets();
//     }

//     // Dispatches to the correctly typed UnityEventTools overload
//     private static void AddPersistentListener(UnityEvent evt, ScriptableObject channel,
//                                                Type argType, MethodInfo raiseMethod)
//     {
//         if (argType == null)
//         {
//             UnityEventTools.AddPersistentListener(evt, (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), channel, raiseMethod));
//         }
//         else if (argType == typeof(int))
//         {
//             UnityEventTools.AddPersistentListener(evt, (UnityAction<int>)Delegate.CreateDelegate(typeof(UnityAction<int>), channel, raiseMethod) );
//         }
//         else if (argType == typeof(float))
//         {
//             UnityEventTools.AddPersistentListener(evt, (UnityAction<float>)Delegate.CreateDelegate(typeof(UnityAction<float>), channel, raiseMethod) );
//         }
//         else if (argType == typeof(bool))
//         {
//             UnityEventTools.AddPersistentListener(evt, (UnityAction<bool>)Delegate.CreateDelegate(typeof(UnityAction<bool>), channel, raiseMethod) );
//         }
//         else if (argType == typeof(string))
//         {
//             UnityEventTools.AddPersistentListener(evt, (UnityAction<string>)Delegate.CreateDelegate(typeof(UnityAction<string>), channel, raiseMethod) );
//         }
//         else if (typeof(UnityEngine.Object).IsAssignableFrom(argType))
//         {
//             // AddObjectPersistentListener<T> handles ScriptableObject, Component, etc.
//             var method = typeof(UnityEventTools)
//                 .GetMethod("AddObjectPersistentListener", BindingFlags.Public | BindingFlags.Static)
//                 ?.MakeGenericMethod(argType);
//             if (method == null) { Debug.LogError($"ConnectChannelButton: AddObjectPersistentListener<{argType.Name}> not found."); return; }
//             var del = Delegate.CreateDelegate(typeof(UnityAction<>).MakeGenericType(argType), channel, raiseMethod);
//             method.Invoke(null, new object[] { evt, del });
//         }
//         else
//         {
//             Debug.LogError($"ConnectChannelButton: Unsupported type {argType.Name}. Supported: void, int, float, bool, string, UnityEngine.Object subclasses.");
//         }
//     }

//     // Walk the generic hierarchy of a UnityEvent type to extract its type parameters
//     private static Type[] GetUnityEventTypeArguments(Type eventType)
//     {
//         for (var t = eventType; t != null; t = t.BaseType)
//         {
//             if (!t.IsGenericType) continue;
//             var def = t.GetGenericTypeDefinition();
//             if (def == typeof(UnityEvent<>)   || def == typeof(UnityEvent<,>) ||
//                 def == typeof(UnityEvent<,,>) || def == typeof(UnityEvent<,,,>))
//                 return t.GetGenericArguments();
//         }
//         return Array.Empty<Type>();
//     }

//     // Reflection helpers to get the in-memory object behind a SerializedProperty
//     private static object GetTargetObjectOfProperty(SerializedProperty prop)
//     {
//         var path = prop.propertyPath.Replace(".Array.data[", "[");
//         object obj = prop.serializedObject.targetObject;
//         foreach (string element in path.Split('.'))
//         {
//             if (element.Contains("["))
//             {
//                 int bracketOpen  = element.IndexOf('[');
//                 int bracketClose = element.IndexOf(']');
//                 string fieldName = element.Substring(0, bracketOpen);
//                 int    index     = int.Parse(element.Substring(bracketOpen + 1, bracketClose - bracketOpen - 1));
//                 obj = GetValueFromArray(obj, fieldName, index);
//             }
//             else
//             {
//                 obj = GetValueFromObject(obj, element);
//             }
//             if (obj == null) return null;
//         }
//         return obj;
//     }

//     private static object GetValueFromObject(object source, string name)
//     {
//         for (var type = source?.GetType(); type != null; type = type.BaseType)
//         {
//             var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
//             if (f != null) return f.GetValue(source);
//         }
//         return null;
//     }

//     private static object GetValueFromArray(object source, string name, int index)
//     {
//         var list = GetValueFromObject(source, name) as IEnumerable;
//         if (list == null) return null;
//         var e = list.GetEnumerator();
//         for (int i = 0; i <= index; i++)
//             if (!e.MoveNext()) return null;
//         return e.Current;
//     }
// }