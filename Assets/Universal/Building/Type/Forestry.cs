using ServerSide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forestry : AbstractBuilding, IResourceStorage
{
    public Forestry(ServerPlayer owner, Vector3Int position) : base(owner, position) 
    {
        BuildingStorage = new List<ResourceStorage>
        {
            new ResourceStorage(this, ResourceType.Wood, new Dictionary<int, double>()
            {
                { 1, 5d }
            })
        };
    }

    public override BuildingType BuildingType => BuildingType.Forestry;

    public List<ResourceStorage> Storage => BuildingStorage;

    public List<ResourceStorage> BuildingStorage;

    public override void OnTurnCycleEnded()
    {
        double produce = GetDefinition().ProduceLevel.Find(x => x.level == Level).value;
        produce -= Storage[0].AddSafe(produce);
        if (produce > 0) //Remained some resource
        {
            owner.TryStoreResource(GetDefinition().produceType, produce);
        }
    }
}
