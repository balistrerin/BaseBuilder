using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(AutomaticVerticleSize))]
public class AutomaticVerticleSizeEditor : Editor {

	public override void OnInspectorGUI(){

		DrawDefaultInspector ();

		if (GUILayout.Button ("Recalc Size")) {

			AutomaticVerticleSize myScript = ((AutomaticVerticleSize)target);
			myScript.AdjustSize ();
		}
	}
}
