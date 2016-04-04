using UnityEngine;
using System.Collections;

public class Tile {
	
	public enum TileType {Empty, Floor};

	TileType type = TileType.Empty;

	public TileType Type {
		get {
			return type;
		}
		set {
			type = value;
		}
	}

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;
	int x;

	public int X {
		get {
			return x;
		}
	}

	int y;

	public int Y {
		get {
			return y;
		}
	}

	public Tile(World world, int x, int y){
		this.world = world;
		this.x = x;
		this.y = y;
	}
}
