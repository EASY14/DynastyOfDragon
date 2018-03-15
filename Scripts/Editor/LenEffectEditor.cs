using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DepthOV))]
[ExecuteInEditMode]
public class LenEffectEditor : Editor
{
    private SerializedProperty LenCircleD;//镜头容许弥散圆直径
    private SerializedProperty LensApertureF;//光圈值F

    private SerializedProperty isFixedFocal;
    private SerializedProperty FixedFocusDistance;
    private SerializedProperty FocusDistance;
    
    private SerializedProperty blurMode;
    private SerializedProperty blurSize;

    private SerializedProperty downSample;
    private SerializedProperty iterations;
    void OnEnable()
    {
        
        LenCircleD = serializedObject.FindProperty("LenCircleD");
        LensApertureF = serializedObject.FindProperty("LensApertureF");
        isFixedFocal = serializedObject.FindProperty("isFixedFocal");
        FixedFocusDistance = serializedObject.FindProperty("FixedFocusDistance");
        FocusDistance = serializedObject.FindProperty("FocusDistance");

        blurMode = serializedObject.FindProperty("blurMode");

        blurSize = serializedObject.FindProperty("blurSize");
        downSample = serializedObject.FindProperty("downSample");
        iterations = serializedObject.FindProperty("iterations");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(blurMode, new GUIContent("模糊方式"));

        GUILayout.Label("镜头调节:", EditorStyles.largeLabel);      
        EditorGUILayout.PropertyField(LenCircleD, new GUIContent("容许弥散圆直径O"));
        EditorGUILayout.PropertyField(LensApertureF, new GUIContent("光圈值F"));
        EditorGUILayout.PropertyField(isFixedFocal, new GUIContent("是否选择定焦镜头"));
        if (isFixedFocal.boolValue==true)
        {
            EditorGUILayout.PropertyField(FixedFocusDistance, new GUIContent("常见焦距f"));
        }
        else
        {
            EditorGUILayout.PropertyField(FocusDistance, new GUIContent("手动调焦焦距f"));
        }

        GUILayout.Label("模糊调节：", EditorStyles.largeLabel);
        EditorGUILayout.PropertyField(blurSize, new GUIContent("模糊程度"));
        EditorGUILayout.PropertyField(downSample, new GUIContent("降采样"));
        EditorGUILayout.PropertyField(iterations, new GUIContent("迭代次数"));
        serializedObject.ApplyModifiedProperties();
    }

    
}
