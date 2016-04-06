using UnityEngine;
using System.Collections. Generic;

public class MouseController : MonoBehaviour {

	public GameObject circleCursorPrefab;

	Vector3 lastFramePosition;
	Vector3 currFramePosition;
	Vector3 dragStartPosition;

	List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start () {
		dragPreviewGameObjects = new List<GameObject>();

		SimplePool.Preload (circleCursorPrefab, 100);
	}
	
	// Update is called once per frame
	void Update () {
		currFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		currFramePosition.z = 0;

		//UpdateCursor ();
		UpdateDragging ();
		UpdateCameraMovement ();

		lastFramePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		lastFramePosition.z = 0;
	}

//	void UpdateCursor(){
//
//		Tile tileUnderMouse = WorldController.Instance.GetTileAtWorldCoord (currFramePosition);
//		if (tileUnderMouse != null) {
//			circleCursor.SetActive (true);
//			Vector3 cursorPosition = new Vector3 (tileUnderMouse.X, tileUnderMouse.Y, 0);
//			circleCursor.transform.position = cursorPosition;
//		} else {
//			circleCursor.SetActive (false);
//		}
//
//
//	}

	void UpdateDragging(){
		
		// Start Drag
		if (Input.GetMouseButtonDown (0)) {
			dragStartPosition = currFramePosition;
		}

		int start_x =   Mathf.FloorToInt (dragStartPosition.x);
		int end_x   =   Mathf.FloorToInt (currFramePosition.x);
		int start_y =   Mathf.FloorToInt (dragStartPosition.y);
		int end_y   =   Mathf.FloorToInt (currFramePosition.y);

		if (end_x < start_x) {
			int temp = end_x;
			end_x = start_x;
			start_x = temp;
		}

		if (end_y < start_y) {
			int temp = end_y;
			end_y = start_y;
			start_y = temp;
		}

		while (dragPreviewGameObjects.Count > 0) {
			GameObject go = dragPreviewGameObjects [0];
			dragPreviewGameObjects.RemoveAt (0);
			SimplePool.Despawn (go);
		}

		if (Input.GetMouseButton (0)) {

			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = WorldController.Instance.World.GetTileAt (x, y);
					if (t != null) {
						GameObject go = SimplePool.Spawn (circleCursorPrefab, new Vector3 (x, y, 0), Quaternion.identity);
						go.transform.SetParent (this.transform, true);
						dragPreviewGameObjects.Add (go);
					}
				}
			}
		}
			
		// End Drag
		if (Input.GetMouseButtonUp (0)) {

			for (int x = start_x; x <= end_x; x++) {
				for (int y = start_y; y <= end_y; y++) {
					Tile t = WorldController.Instance.World.GetTileAt (x, y);
					if (t != null) {
						t.Type = Tile.TileType.Floor;
					}
				}
			}
		}
	}

	void UpdateCameraMovement(){

		if (Input.GetMouseButton (1) || Input.GetMouseButton (2)) {

			Vector3 diff = lastFramePosition - currFramePosition;
			Camera.main.transform.Translate (diff);
		}
			
		Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis ("Mouse ScrollWheel") ;

		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, 3f, 25f);
	}

}

