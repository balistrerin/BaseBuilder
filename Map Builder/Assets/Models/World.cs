using UnityEngine;
using System.Collections;

public class World {

	Tile[,] tiles;
	int width;
	int height;

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

	public Tile GetTileAt(int x, int y){
		if (x > width || x < 0) {
			Debug.LogError ("Tile (" + x + "," + y + ") is out of range.");
			return null;
		}
		return tiles [x, y];

	}

}
