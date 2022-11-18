using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MushroomGenerator))]
public class TreeGeneratorEditor : Editor
{

	public override void OnInspectorGUI()
	{
		MushroomGenerator mushroomGenerator = (MushroomGenerator)target;

		if (DrawDefaultInspector())
		{
			if (mushroomGenerator.autoUpdate)
			{
				mushroomGenerator.generate();
			}
		}

		if (GUILayout.Button("Generate"))
		{
			mushroomGenerator.generate();
		}
	}
}