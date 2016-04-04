using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {
	
	public Sprite floorSprite;

	World world;

	// Use this for initialization
	void Start () {
		world = new World();
		world.RandomizeTiles();

		//Create GameObject for each of our tiles, so they show visually.
		for (int x = 0; x < world.Width; x++) {
			for (int y = 0; y < world.Height; y++) {
				Tile tile_data = world.GetTileAt (x, y);

				GameObject tile_go = new GameObject ();
				tile_go.name = "Tile_" + x + "_" + y;
				tile_go.transform.position = new Vector3 (tile_data.X, tile_data.Y, 0);

				SpriteRenderer tile_sr = tile_go.AddComponent<SpriteRenderer> ();

				if (tile_data.Type == Tile.TileType.Floor) {
					tile_sr.sprite = floorSprite;
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
