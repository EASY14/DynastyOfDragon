using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CharacterBehaviour))]   
[ExecuteInEditMode]
public class CharacterMovementEditor : Editor {

    private SerializedProperty maxhp;
    private SerializedProperty maxmp;
    private SerializedProperty mpRate;
    private SerializedProperty attackSpeed;
    private SerializedProperty moveSpeed;
    private SerializedProperty attackDamage;

    private SerializedProperty showEffect;
    private SerializedProperty resumeHPEffect;
    private SerializedProperty resumeMPEffect;

    private SerializedProperty bloodChangeEvent;
    private SerializedProperty isShowBloodChange;

    private SerializedProperty energyChangeEvent;
    private SerializedProperty isShowEnergyChange;

    private SerializedProperty updatePlayData;
    private SerializedProperty isShowUpdatePlayerData;

    private SerializedProperty isShowRange;
    private SerializedProperty currentSkillID;
    private SerializedProperty skills;

    //private int num = 5;
    void OnEnable ()
    {
        maxhp = serializedObject.FindProperty("maxhp");
        maxmp = serializedObject.FindProperty("maxmp");
        mpRate = serializedObject.FindProperty("mpRate");
        attackSpeed = serializedObject.FindProperty("attackSpeed");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        attackDamage = serializedObject.FindProperty("attackDamage");

        showEffect = serializedObject.FindProperty("showEffect");
        resumeHPEffect = serializedObject.FindProperty("resumeHPEffect");
        resumeMPEffect = serializedObject.FindProperty("resumeMPEffect");

        bloodChangeEvent = serializedObject.FindProperty("bloodChangeEvent");
        isShowBloodChange = serializedObject.FindProperty("isShowBloodChange");

        energyChangeEvent = serializedObject.FindProperty("energyChangeEvent");
        isShowEnergyChange = serializedObject.FindProperty("isShowEnergyChange");

        updatePlayData = serializedObject.FindProperty("updatePlayData");
        isShowUpdatePlayerData = serializedObject.FindProperty("isShowUpdatePlayerData");

        isShowRange = serializedObject.FindProperty("isShowRange");
        currentSkillID = serializedObject.FindProperty("currentSkillID");
        skills = serializedObject.FindProperty("skills");
    }

	public override void OnInspectorGUI()
    {
        serializedObject.Update();

        CharacterBehaviour cm = target as CharacterBehaviour;


        EditorGUILayout.PropertyField(maxhp, new GUIContent("max hp"));
        EditorGUILayout.PropertyField(maxmp, new GUIContent("max mp"));

        EditorGUILayout.PropertyField(mpRate, new GUIContent("mp rate"));

        EditorGUILayout.PropertyField(attackSpeed, new GUIContent("attack Speed"));
        EditorGUILayout.PropertyField(attackDamage, new GUIContent("attack damage"));
        
        EditorGUILayout.PropertyField(moveSpeed, new GUIContent("move speed"));

        EditorGUILayout.PropertyField(showEffect, new GUIContent("show effect"));
        EditorGUILayout.PropertyField(resumeHPEffect, new GUIContent("resumeHP effect"));
        EditorGUILayout.PropertyField(resumeMPEffect, new GUIContent("resumeMP effect"));

        GUILayout.Space(5);

        EditorGUILayout.PropertyField(isShowBloodChange);
        if (isShowBloodChange.boolValue)
        {
            EditorGUILayout.LabelField("parameter ,float maxhp,float currenthp", GUILayout.Height(20));
            EditorGUILayout.PropertyField(bloodChangeEvent);
        }

        EditorGUILayout.PropertyField(isShowEnergyChange);
        if (isShowEnergyChange.boolValue)
        {
            EditorGUILayout.LabelField("parameter ,float maxmp,float currentmp", GUILayout.Height(20));
            EditorGUILayout.PropertyField(energyChangeEvent);
        }

        EditorGUILayout.PropertyField(isShowUpdatePlayerData);
        if (isShowUpdatePlayerData.boolValue)
        {
            EditorGUILayout.LabelField("parameter ,attactDamage,attactSpeed,MoveSpeed,mpRate", GUILayout.Height(20));
            EditorGUILayout.PropertyField(updatePlayData);
        }


        EditorGUILayout.PropertyField(isShowRange);
        EditorGUILayout.PropertyField(currentSkillID);
        EditorGUILayout.PropertyField(skills, true);

        serializedObject.ApplyModifiedProperties();

    }
}
