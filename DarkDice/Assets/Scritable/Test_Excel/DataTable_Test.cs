using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DataTable_Test : ScriptableObject
{
	//public List<EntityType> 정의; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> ID생성표; // Replace 'EntityType' to an actual type that is serializable.
	public List<stageEntity> stage_Data; // Replace 'EntityType' to an actual type that is serializable.
	public List<monsterEntity> monster_Data; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> 몬스터; // Replace 'EntityType' to an actual type that is serializable.
	//public List<EntityType> item_Data; // Replace 'EntityType' to an actual type that is serializable.
}
