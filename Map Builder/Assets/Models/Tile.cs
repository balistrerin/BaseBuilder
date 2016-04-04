using UnityEngine;
using System.Collections;
using System;

public class Tile {
	
	public enum TileType {Empty, Floor};

	private TileType _type = TileType.Empty;

	public TileType Type {
		get {return _type;}
		set {
			TileType oldType = _type;
			_type = value;

			if (cbTileTypeChanged != null && oldType != _type)
				cbTileTypeChanged (this);
		}
	}

	LooseObject looseObject;
	InstalledObject installedObject;

	World world;

	public int X {get; protected set; }
	public int Y { get; protected set;}

	Action<Tile> cbTileTypeChanged;

	public Tile(World world, int x, int y){
		this.world = world;
		this.X = x;
		this.Y = y;
	}

	public void RegisterTileTypeChangedCallback(Action<Tile> callback){
		cbTileTypeChanged += callback;
	}

	public void UnregisterTileTypeChangedCallback(Action<Tile> callback){
		cbTileTypeChanged -= callback;
	}
}
