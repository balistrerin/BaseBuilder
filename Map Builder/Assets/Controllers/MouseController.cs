﻿using UnityEngine;
using System.Collections. Generic;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour {


	bool buildModeIsObjects = false;
	public GameObject circleCursorPrefab;

	Tile.TileType buildModeTile = Tile.TileType.Floor;
	string buildModeObjectType;

	Vector3 lastFramePosition;
	Vector3 currFramePosition;
	Vector3 dragStartPosition;

	List<GameObject> dragPreviewGameObjects;

	// Use this for initialization
	void Start () {
		dragPreviewGameObjects = new List<GameObject>();

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
		
	void UpdateDragging(){

		if (EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

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
						if (buildModeIsObjects == true) {

							WorldController.Instance.World.PlaceInstalledObject (buildModeObjectType, t);




						} else {

							t.Type = buildModeTile;
						}
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

	public void SetMode_BuildFloor(){

		buildModeTile = Tile.TileType.Floor;
		buildModeIsObjects = false;
	}

	public void SetMode_Bulldoze(){

		buildModeTile = Tile.TileType.Empty;
		buildModeIsObjects = false;
	}

	public void SetMode_BuildInstalledObject(string objectType){
		
		buildModeIsObjects = true;
		buildModeObjectType = objectType;
	}
}

