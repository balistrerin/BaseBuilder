using UnityEngine;
using System.Collections;

public class World {

	Tile[,] tiles;
	int width;

	public int Width {
		get {
			return width;
		}
	}

	int height;

	public int Height {
		get {
			return height;
		}
	}

	public World(int width = 100, int height = 100){
		this.width = width;
		this.height = height;

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tiles = new Tile[width, height];
			}
		}

		Debug.Log ("World created with " + (width * height) + " tiles");
	}

	public void RandomizeTiles(){

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (Random.Range (0, 2) == 0) {
					tiles[x, y].Type = Tile.TileType.Empty;
				} else {
					tiles[x, y].Type = Tile.TileType.Floor;
				}
			}
		}
	}

	public Tile GetTileAt(int x, int y){
		if (x > width || x < 0) {
			Debug.LogError ("Tile (" + x + "," + y + ") is out of range.");
			return null;
		}
		return tiles [x, y];

	}

}
