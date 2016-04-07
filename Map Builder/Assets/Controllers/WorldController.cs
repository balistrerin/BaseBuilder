using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class WorldController : MonoBehaviour {

	public static WorldController Instance{ get; protected set;}
	public Sprite floorSprite;
	public Sprite wallSprite;

	Dictionary<Tile,GameObject> tileGameObjectMap;

	Dictionary<InstalledObject,GameObject> installedObjectGameObjectMap;

	public World World { get; protected set;}

	// Use this for initialization
	void Start () {
		if (Instance != null) {
			Debug.LogError("There should nver be two world controllers.");
		}

		Instance = this;

		World = new World();

		World.RegisterInstalledObjectCreated (OnInstalledObjectCreated);

		tileGameObjectMap = new Dictionary<Tile, GameObject> ();
		installedObjectGameObjectMap = new Dictionary<InstalledObject,GameObject> ();


		//Create GameObject for each of our tiles, so they show visually.
		for (int x = 0; x < World.Width; x++) {
			for (int y = 0; y < World.Height; y++) {
				Tile tile_data = World.GetTileAt (x, y);

				GameObject tile_go = new GameObject ();

				tileGameObjectMap.Add (tile_data, tile_go);

				tile_go.name = "Tile_" + x + "_" + y;
				tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, 0);
				tile_go.transform.SetParent (this.transform, true);

				tile_go.AddComponent<SpriteRenderer> ();

				tile_data.RegisterTileTypeChangedCallback(OnTileTypeChanged);

			}
		}
		World.RandomizeTiles();
	}
	void Update(){
		
	}

	void DestroyAllTileGameObjects(){

		while (tileGameObjectMap.Count > 0) {
			Tile tile_data = tileGameObjectMap.Keys.First ();
			GameObject tile_go = tileGameObjectMap [tile_data];

			tileGameObjectMap.Remove (tile_data);
			tile_data.UnregisterTileTypeChangedCallback (OnTileTypeChanged);
			Destroy (tile_go);
		}
	}

	void OnTileTypeChanged(Tile tile_data){

		if (tileGameObjectMap.ContainsKey (tile_data) == false) {
			Debug.LogError ("tileGameObjectMap doesnt contain the tile_data -- did you forget to add the tile to the dictionayr or forget to unregister a callback?  ");
			return;
		}
		GameObject tile_go = tileGameObjectMap [tile_data];

		if (tile_go == null) {
			Debug.LogError ("tileGameObjectMap's returned GameObject is null -- did you forget to add the tile to the dictionayr or forget to unregister a callback?  ");
			return;
		}

		if (tile_data.Type == Tile.TileType.Floor) {
			tile_go.GetComponent<SpriteRenderer> ().sprite = floorSprite;

		} else if(tile_data.Type == Tile.TileType.Empty){
			tile_go.GetComponent<SpriteRenderer> ().sprite = null;

		}else{
			Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
		}

	}

	public Tile GetTileAtWorldCoord(Vector3 coord){

		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y);

		return World.GetTileAt (x, y);

	}

	public void OnInstalledObjectCreated(InstalledObject obj){

		GameObject obj_go = new GameObject ();

		installedObjectGameObjectMap.Add (obj, obj_go);

		obj_go.name = obj.objectType + "_" + obj.tile.X + "_" + obj.tile.Y;
		obj_go.transform.position = new Vector3 (obj.tile.X, obj.tile.Y, 0);
		obj_go.transform.SetParent (this.transform, true);

		obj_go.AddComponent<SpriteRenderer> ().sprite = wallSprite;

		obj.RegisterOnChangedCallback (OnInstalledObjectChanged);

	}

	void OnInstalledObjectChanged(InstalledObject obj){
		Debug.LogError("OnInstalledObjectChanged -- NOT IMPLEMENTED");
	}

}
