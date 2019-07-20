using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SoftObject))]
public class SoftObjectInspector : Editor
{
	SerializedProperty distanceProp;
	SerializedProperty frequencyProp;
	SerializedProperty dampingRationProp;
	SerializedProperty massProp;
	SerializedProperty angularDragProp;
	SerializedProperty linearDragProp;

	private float distanceVal;
	private float frequencyVal;
	private float dampingRationVal;
	private float massVal;
	private float angularDragVal;
	private float linearDragVal;

	void OnEnable()
	{
		distanceProp = serializedObject.FindProperty("Distance");
		frequencyProp = serializedObject.FindProperty("Frequency");
		dampingRationProp = serializedObject.FindProperty("DampingRation");
		massProp = serializedObject.FindProperty("Mass");
		angularDragProp = serializedObject.FindProperty("AngularDrag");
		linearDragProp = serializedObject.FindProperty("LinearDrag");

		SetValues();
	}

	private void SetValues()
	{
		distanceVal = distanceProp.floatValue;
		frequencyVal = frequencyProp.floatValue;
		dampingRationVal = dampingRationProp.floatValue;
		massVal = massProp.floatValue;
		angularDragVal = angularDragProp.floatValue;
		linearDragVal = linearDragProp.floatValue;
	}

	private bool CheckForUpdate()
	{
		if (distanceVal != distanceProp.floatValue || frequencyVal != frequencyProp.floatValue || dampingRationVal != dampingRationProp.floatValue ||
			massVal != massProp.floatValue || angularDragVal != angularDragProp.floatValue || linearDragVal != linearDragProp.floatValue)
		{
			SetValues();
			return true;
		}
		return false;
	}

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		EditorGUILayout.PropertyField(distanceProp, new GUIContent("Distance"));
		EditorGUILayout.PropertyField(frequencyProp, new GUIContent("Frequency"));
		EditorGUILayout.PropertyField(dampingRationProp, new GUIContent("DampingRatio"));
		EditorGUILayout.PropertyField(massProp, new GUIContent("Mass"));
		EditorGUILayout.PropertyField(angularDragProp, new GUIContent("AngularDrag"));
		EditorGUILayout.PropertyField(linearDragProp, new GUIContent("LinearDrag"));

		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Update") || CheckForUpdate())
		{
			foreach (var target in targets)
			{
				var softObject = (SoftObject)target;
				softObject.ForceUpdate();
			}
		}
	}
}
