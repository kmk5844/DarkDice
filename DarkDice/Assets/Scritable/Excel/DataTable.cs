using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DataTable : ScriptableObject
{
	//public List<EntityType> description; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> ID_code; // Replace 'EntityType' to an actual type that is serializable.
	public List<stageEntity> stage_Data; // Replace 'EntityType' to an actual type that is serializable.
	public List<monsterEntity> monster_Data; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> item_Data; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> monster_ref; // Replace 'EntityType' to an actual type that is serializable.
}
