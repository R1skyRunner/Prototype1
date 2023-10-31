using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Ragdoll))]
public class RagdollEditor : Editor
{
    Ragdoll ragdoll;

    Collider[] colliders;
    Rigidbody[] rigidBodies;
    CharacterJoint[] characterJoints;


    SerializedProperty TargetColliders;
    SerializedProperty TargetRigidBodies;
    SerializedProperty TargetJoints;

    private void OnEnable()
    {
        ragdoll = (Ragdoll)target;

        colliders = ragdoll.GetComponentsInChildren<Collider>();
        rigidBodies = ragdoll.GetComponentsInChildren<Rigidbody>();
        characterJoints = ragdoll.GetComponentsInChildren<CharacterJoint>();

        TargetColliders = serializedObject.FindProperty("Colliders");
        TargetRigidBodies = serializedObject.FindProperty("rigidBodies");
        TargetJoints = serializedObject.FindProperty("joints");

    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        TargetColliders.arraySize = colliders.Length;
        TargetRigidBodies.arraySize = rigidBodies.Length;
        TargetJoints.arraySize = characterJoints.Length;

        EditorGUILayout.LabelField("Set Ragdoll");
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(20f));

        Buttons();

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    private void Buttons()
    {
        if (GUILayout.Button("Get Colliders"))
        {
            GetColliders();
        }

        if (GUILayout.Button("Remove Everything"))
        {
            RemoveEverything();
        }

        if (GUILayout.Button("Remove Colliders"))
        {
            RemoveColliders();
        }

        if (GUILayout.Button("Get Everything"))
        {
            GetEverything();
        }

        if (GUILayout.Button("Remove Components"))
        {
            RemoveComponents();
        }

        if (GUILayout.Button("Add Components"))
        {
            AddComponents();
        }

        if (GUILayout.Button("Enable Components"))
        {
            EnableComponents(true);
        }

        if (GUILayout.Button("Disable Components"))
        {
            EnableComponents(false);
        }

       
    }

    #region // Add Components
    public void GetEverything()
    {
        for (int i = 0; i < rigidBodies.Length; i++)
        {
            TargetRigidBodies.GetArrayElementAtIndex(i).objectReferenceValue = rigidBodies[i];
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            TargetColliders.GetArrayElementAtIndex(i).objectReferenceValue = colliders[i];
        }

        for (int i = 0; i < characterJoints.Length; i++)
        {
            TargetJoints.GetArrayElementAtIndex(i).objectReferenceValue = characterJoints[i];
        }

    }

    public void GetColliders()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            TargetColliders.GetArrayElementAtIndex(i).objectReferenceValue = colliders[i];
        }
    }

    public void AddComponents()
    {
        if (ragdoll.Colliders[0] == null) return;

        foreach (Collider collider in ragdoll.Colliders)
        {
            collider.gameObject.AddComponent<ChildObjectTakeDamage>();
        }
    } 

    public void EnableComponents(bool state)
    {
        if (ragdoll.Colliders[0] == null) return;

        foreach (Collider collider in ragdoll.Colliders)
        {
            collider.gameObject.GetComponent<ChildObjectTakeDamage>().enabled = state;
        }
    }

    #endregion

    #region // Remove Components
    public void RemoveEverything()
    {
        TargetRigidBodies.ClearArray();
        TargetColliders.ClearArray();
        TargetJoints.ClearArray();

    }

    public void RemoveColliders()
    {
        TargetColliders.ClearArray();
    }

    public void RemoveComponents()
    {
        if (ragdoll.Colliders[0] == null) return;

        foreach (Collider collider in ragdoll.Colliders)
        {
            DestroyImmediate(collider.gameObject.GetComponent<ChildObjectTakeDamage>());
        }
    }


    #endregion

}
