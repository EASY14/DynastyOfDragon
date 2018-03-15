using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MonsterBehaviour))]
[ExecuteInEditMode]
public class MonsterBehaviourEditor : Editor {

    private SerializedProperty maxhp;
    private SerializedProperty detectRange;
    private SerializedProperty attackSpeed;
    private SerializedProperty moveSpeed;
    private SerializedProperty attackDamage;
    private SerializedProperty experience;
    private SerializedProperty cyrstal;

    private SerializedProperty dieEffect;
    private SerializedProperty showEffect;

    private SerializedProperty bloodChangeEvent;
    private SerializedProperty showSkillTipsEvent;
    private SerializedProperty hideSkillTipsEvent;

    private SerializedProperty isShowBloodChange;
    private SerializedProperty isShowSkillTips;
    private SerializedProperty isHideSkillTips;
    private SerializedProperty isShowRange;

    private SerializedProperty currentSkillID;
    private SerializedProperty skills;

    //private int num = 5;
    void OnEnable ()
    {
        maxhp = serializedObject.FindProperty("maxhp");
        detectRange = serializedObject.FindProperty("detectRange");
        attackSpeed = serializedObject.FindProperty("attackSpeed");
        moveSpeed = serializedObject.FindProperty("moveSpeed");
        attackDamage = serializedObject.FindProperty("attackDamage");
        experience = serializedObject.FindProperty("experience");
        cyrstal = serializedObject.FindProperty("cyrstal");

        showEffect = serializedObject.FindProperty("showEffect");
        dieEffect = serializedObject.FindProperty("dieEffect");

        bloodChangeEvent = serializedObject.FindProperty("bloodChangeEvent");
        showSkillTipsEvent = serializedObject.FindProperty("showSkillTipsEvent");
        hideSkillTipsEvent = serializedObject.FindProperty("hideSkillTipsEvent");

        isShowBloodChange = serializedObject.FindProperty("isShowBloodChange");
        isShowSkillTips = serializedObject.FindProperty("isShowSkillTips");
        isHideSkillTips = serializedObject.FindProperty("isHideSkillTips");
        isShowRange = serializedObject.FindProperty("isShowRange");

        currentSkillID = serializedObject.FindProperty("currentSkillID");
        skills = serializedObject.FindProperty("skills");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MonsterBehaviour ms = target as MonsterBehaviour;


        EditorGUILayout.PropertyField(maxhp, new GUIContent("max hp"));

        EditorGUILayout.PropertyField(detectRange, new GUIContent("detect range"));

        EditorGUILayout.PropertyField(attackSpeed, new GUIContent("attack Speed"));
        EditorGUILayout.PropertyField(attackDamage, new GUIContent("attack damage"));

        EditorGUILayout.PropertyField(moveSpeed, new GUIContent("move speed"));

        EditorGUILayout.PropertyField(experience, new GUIContent("experience"));
        EditorGUILayout.PropertyField(cyrstal, new GUIContent("crystal"));

        EditorGUILayout.PropertyField(showEffect, new GUIContent("showEffect"));
        EditorGUILayout.PropertyField(dieEffect, new GUIContent("dieEffect"));
        GUILayout.Space(5);

        EditorGUILayout.PropertyField(isShowBloodChange);
        if (isShowBloodChange.boolValue)
        {
            EditorGUILayout.LabelField("parameter ,float bloodPercentage [0.0f,1.0f]", GUILayout.Height(20));
            EditorGUILayout.PropertyField(bloodChangeEvent);
        }

        EditorGUILayout.PropertyField(isShowSkillTips);
        if (isShowSkillTips.boolValue)
        {
            EditorGUILayout.LabelField("parameter ,float skillRange,float skillAngle", GUILayout.Height(20));
            EditorGUILayout.PropertyField(showSkillTipsEvent);
        }

        EditorGUILayout.PropertyField(isHideSkillTips);
        if (isHideSkillTips.boolValue)
        {
            EditorGUILayout.PropertyField(hideSkillTipsEvent);
        }

        EditorGUILayout.PropertyField(isShowRange);
        EditorGUILayout.PropertyField(currentSkillID);
        EditorGUILayout.PropertyField(skills,true);


        serializedObject.ApplyModifiedProperties();
    }
}
