using UnityEngine;
using System.Collections;
using System;

public class InstalledObject  {

	public Tile tile {
		get; protected set;
	}

	public string objectType {

		get; protected set;
	}


	float movementCost;

	int width;
	int height;

	Action<InstalledObject> cbOnChanged;

	protected InstalledObject(){

	}
	 
	static public InstalledObject CreatePrototype(string objectType, float movementCost, int width, int height){
		InstalledObject obj = new InstalledObject ();
		obj.objectType = objectType;
		obj.movementCost = movementCost;
		obj.width = width;
		obj.height = height;

		return obj;
	}

	static public InstalledObject PlaceInstance(InstalledObject proto, Tile tile){
		InstalledObject obj = new InstalledObject ();
		obj.objectType = proto.objectType;
		obj.movementCost = proto.movementCost;
		obj.width = proto.width;
		obj.height = proto.height;

		obj.tile = tile;

		if (tile.PlaceObject (obj) == false) {
			return null;
		}

		return obj;
	}

	public void RegisterOnChangedCallback(Action<InstalledObject> callbackFunc){
		cbOnChanged += callbackFunc;
	}

	public void UnregisterOnChangedCallback(Action<InstalledObject> callbackFunc){
		cbOnChanged -= callbackFunc;
	}
}
