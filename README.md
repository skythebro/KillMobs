# Killmobs

## Installation (Manual)

* Install [BepInEx](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html)
* Install [Bloodstone](https://v-rising.thunderstore.io/package/deca/Bloodstone) into (VRising server folder)/BepInEx/plugins
* Install [VampireCommandFramework](https://v-rising.thunderstore.io/package/deca/VampireCommandFramework/) into (VRising server folder)/BepInEx/plugins
* Extract [MobKiller.dll](https://thunderstore.io/package/download/Skies/SpiderKiller/1.2.0/) into (VRising server folder)/BepInEx/plugins

Features:
- Kills mobs using a command within a certain range of the player.

Admin command (requires [VampireCommandFramework](https://v-rising.thunderstore.io/package/deca/VampireCommandFramework/)):
- Use the `.killmobs [name] [range]` command to kill mobs in range around player,`name` is prefabName: so name that starts with CHAR_ . `range` is 25f by default which is 5 tiles.

### Troubleshooting
- Make sure you install the mod on the server. If you are in a singleplayer world use [ServerLaunchFix](https://v-rising.thunderstore.io/package/Mythic/ServerLaunchFix/)
- Check your BepInEx logs on the server to make sure the latest version of both KillMobs, VampireCommandFramework and Bloodstone were loaded.

### Support
- Ask in the V Rising Mod Community [discord](https://vrisingmods.com/discord)

### Contributors
- skythebro/skyKDG: `@realskye` on Discord
- V Rising Mod Community discord for helpful resources to mod this game and code inspiration.