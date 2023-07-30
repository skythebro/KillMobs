using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using SpiderKiller;
using UnityEngine;

namespace MobKiller.VCFCompat
{
    using ProjectM;
    using Unity.Entities;
    using Unity.Transforms;
    using VampireCommandFramework;
    using Bloodstone.API;

    public static partial class Commands
    {
        private static ManualLogSource _log => Plugin.LogInstance;

        static Commands()
        {
            Enabled = IL2CPPChainloader.Instance.Plugins.TryGetValue("gg.deca.VampireCommandFramework", out var info);
            if (Enabled) _log.LogWarning($"VCF Version: {info.Metadata.Version}");
        }

        public static bool Enabled { get; private set; }

        public static void Register() => CommandRegistry.RegisterAll();

        public record Mob(Entity Entity);

        public class MobRemover : CommandArgumentConverter<Mob, ChatCommandContext>
        {
            private const float Radius = 25f;

            public override Mob Parse(ChatCommandContext ctx, string input)
            {
                var mobs = MobUtil.ClosestMobs(ctx, Radius);
                var em = VWorld.Server.EntityManager;
                var getTeam = em.GetComponentDataFromEntity<Team>();

                foreach (var mob in mobs)
                {
                    var team = getTeam[mob];
                    var isUnit = Team.IsInUnitTeam(team);
                    var isNeutral = Team.IsInNeutralTeam(team);
                    if (isNeutral || !isUnit) continue;
                    return new Mob(mob);
                }

                throw ctx.Error($"Could not find a mob within {Radius:F1}");
            }
            
            public class MobCommands
            {
                [Command("killmobs", shortHand: "kmob", adminOnly: true, description: "Kills mobs in range, name is optional.",
                    usage: "Usage: .killmobs [name] [radius] example: (CHAR_Bandit_Bomber)")]
                public void killmobs(ChatCommandContext ctx, string name = "None", float radius = 25f)
                {
                    if (name.Equals("None"))
                    {
                        ctx.Error("You need to specify a mob name!");
                        return;
                    }
                    var mobs = MobUtil.ClosestMobs(ctx, radius, name);

                    foreach (var mob in mobs)
                    {
                        StatChangeUtility.KillEntity(VWorld.Server.EntityManager, mob,
                            ctx.Event.SenderCharacterEntity, Time.time, true);
                    }

                    if (mobs.Count < 1)
                    {
                        ctx.Error("Failed to kill any mobs, are there any in range?");
                    }
                    else
                    {
                        ctx.Reply("Mobs have been killed!");
                    }
                    
                    
                }
                
            }
        }
    }
}