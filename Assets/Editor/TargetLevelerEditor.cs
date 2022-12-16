using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(JumpPad))]
public class TargetLevelerEditor : Editor
{

	public override void OnInspectorGUI()
	{
		JumpPad jumpPad = (JumpPad)target;

		if (GUILayout.Button("Level"))
		{
			jumpPad.levelTarget();
		}
	}
}