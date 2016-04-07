using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class AutomaticVerticleSize : MonoBehaviour {

	public float childHeight = 35f;
	public float childWidth = 150f;
	// Use this for initialization
	void Start () {
		AdjustSize ();
	}

	public void AdjustSize(){

		Vector2 size = this.GetComponent<RectTransform> ().sizeDelta;
		size.x = this.transform.childCount * childWidth;
		size.y = this.transform.childCount * childHeight;
		this.GetComponent<RectTransform> ().sizeDelta = size;
	}
}
