using UnityEngine;
using System.Collections.Generic;
using System;

public class World {

	Tile[,] tiles;

	Dictionary<string, InstalledObject> installedObjectsPrototypes;

	public int Width {get; protected set; }

	public int Height { get; protected set;}

	Action<InstalledObject> cbInstalledObjectCreated;

	public World(int width = 100, int height = 100){
		
		this.Width = width;
		this.Height = height;

		tiles = new Tile[Width,Height];

		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				tiles [x, y] = new Tile (this, x, y);
			}
		}

		Debug.Log ("World created with " + (width * height) + " tiles");

		CreateInstalledObjectPrototypes ();
	}

	void CreateInstalledObjectPrototypes(){

		installedObjectsPrototypes = new Dictionary<string,InstalledObject> ();


		installedObjectsPrototypes.Add ("Wall", 
									InstalledObject.CreatePrototype (
										"Wall",
										0, // Impassable
										1, // Width
										1 // Height
									)
		);
	}

	public void RandomizeTiles(){
		Debug.Log ("RandomizeTiles");
		for (int x = 0; x < Width; x++) {
			for (int y = 0; y < Height; y++) {
				
				if(UnityEngine.Random.Range(0, 2) == 0) {
					tiles[x,y].Type = Tile.TileType.Empty;
				} else {
					tiles[x,y].Type = Tile.TileType.Floor;
				}
			}
		}
	}

	public Tile GetTileAt(int x, int y){
		if (x > Width || x < 0 || y > Height || y < 0) {
			Debug.LogError ("Tile (" + x + "," + y + ") is out of range.");
			return null;
		}
		return tiles [x, y];

	}

	public void PlaceInstalledObject(string objectType, Tile t){

		if (installedObjectsPrototypes.ContainsKey (objectType) == false) {
			Debug.LogError ("installedObjectsPrototypes doesnt contain a proto for key" + objectType);
			return;
		}

		InstalledObject obj = InstalledObject.PlaceInstance (installedObjectsPrototypes [objectType], t);

		if (obj == null) {
			return;
		}


		if (cbInstalledObjectCreated != null) {
			cbInstalledObjectCreated (obj);
		}
	}

	public void RegisterInstalledObjectCreated(Action<InstalledObject> callbackfunc){

		cbInstalledObjectCreated += callbackfunc;
	}

	public void UnregisterInstalledObjectCreated(Action<InstalledObject> callbackfunc){

		cbInstalledObjectCreated -= callbackfunc;
	}


}
