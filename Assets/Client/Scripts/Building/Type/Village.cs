using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : AbstractBuilding
{
    public Village(Vector3Int position) : base(position) { }

    public override BuildingType BuildingType => BuildingType.Village;
}
