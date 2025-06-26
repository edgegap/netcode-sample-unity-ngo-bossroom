# Boss Room sample on Edgegap
This guide will help you create a dedicated server on Edgegap for Unity's [Boss Room sample](https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop). For this guide, we do **not** require setting up Unity Relays.

## Preparation
This tutorial assumes you are already familiar with [using the Edgegap plugin](https://docs.edgegap.com/learn/unity-games/getting-started-with-servers).

## Testing the sample
- Build, Containerize, then Upload & Create a New App Version for your server using the Edgegap plugin. Keep the default port mapping values when creating the app version.
- Deploy your app version from the Edgegap dashboard. Take note of the external port value, as well as the `ARBITRIUM_PUBLIC_IP` from the container logs.
- Launch the game in the editor. Select `Start with direct IP`, then `Join with IP`. Enter the server's `IP address` and `external port` value, then click on `Join`.
- Select your hero, then click on `Ready` to start the game.

## Editing the sample
For this sample to work as a dedicated server, the following changes needed to be made to the base sample:

- In `Assets/Scripts/ConenctionManagement/ConnectionMethod.cs`, the `SetConnectionPayload()` call in the `SetupHostConnectionAsync` function of the `ConnectionMethodIP` class was commented out, since the server is not a player.
- In `Assets/Scripts/ConenctionManagement/ConnectionState/StartingHostState.cs`, we changed `NetworkManager.StartHost()` with `NetworkManager.StartServer()` in the `StartHost` function.
- In `Assets/Scripts/Gameplay/GameplayObjects/Character/ClientConnection.cs`, we commented out the `OnMovementStatusChanged()` call in the `OnNetworkSpawn` function.
- In `Assets/Scripts/ConenctionManagement/ConnectionState/HostingState.cs`, we commented out the following lines from the `GetConnectStatus` function in order to test the client from the editor.

```
if (connectionPayload.isDebug != Debug.isDebugBuild)
{
    return ConnectStatus.IncompatibleBuildType;
}
```

- We added a `EdgegapServerStarter` gameObject to the `MainMenu` scene with a script that allows the server to automatically call `connectionManager.StartHostIp()` on launch, using `0.0.0.0` as the address, and the value of the `ARBITRIUM_PORT_GAMEPORT_INTERNAL` environment variable as the port. The value used for the player name does not matter in this case.
