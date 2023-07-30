using System;
using System.Collections.Generic;
using Bloodstone.API;
using ProjectM;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using VampireCommandFramework;

namespace SpiderKiller;

internal static class MobUtil
{
    private static NativeArray<Entity> GetMobs()
    {
        var mobQuery = VWorld.Server.EntityManager.CreateEntityQuery(new EntityQueryDesc()
        {
            All = new[]
            {
                ComponentType.ReadOnly<LocalToWorld>(),
                ComponentType.ReadOnly<Team>()
            },
            None = new[] { ComponentType.ReadOnly<Dead>(), ComponentType.ReadOnly<DestroyTag>() }
        });

        return mobQuery.ToEntityArray(Allocator.Temp);
    }

    internal static List<Entity> ClosestMobs(ChatCommandContext ctx, float radius, string name = "None")
    {
        try
        {
            var e = ctx.Event.SenderCharacterEntity;
            var mobs = GetMobs();
            var results = new List<Entity>();
            var origin = VWorld.Server.EntityManager.GetComponentData<LocalToWorld>(e).Position;
            var prefabCollectionSystem = VWorld.Server.GetExistingSystem<PrefabCollectionSystem>();
            PrefabGUID mobGUID;
            mobGUID = !name.Equals("None") ? prefabCollectionSystem.NameToPrefabGuidDictionary[name] : new PrefabGUID();
            foreach (var mob in mobs)
            {
                var position = VWorld.Server.EntityManager.GetComponentData<LocalToWorld>(mob).Position;
                var distance = UnityEngine.Vector3.Distance(origin, position); // wait really?
                var em = VWorld.Server.EntityManager;
                var getGuid = em.GetComponentDataFromEntity<PrefabGUID>();
                if (name.Equals("None") && distance < radius)
                {
                    results.Add(mob);
                }
                else if (distance < radius && getGuid[mob] == mobGUID)
                {
                    results.Add(mob);
                }
            }

            return results;
        }
        catch (Exception)
        {
            return null;
        }
    }
}