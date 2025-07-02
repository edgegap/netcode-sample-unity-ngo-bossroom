# Boss Room sample on Edgegap
Create a dedicated server on Edgegap for Unity's [Boss Room sample](https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop). This sample does not require any Unity Gaming Services (UGS), Multiplay, or Relays to run. See guide for testing and customization in [Edgegap Learning Center](https://docs.edgegap.com/docs/sample-projects/unity-netcodes/unity-netcode-on-edgegap).

## Preparation
This tutorial assumes you are already familiar with [using the Edgegap plugin](https://docs.edgegap.com/learn/unity-games/getting-started-with-servers).

## Sample Modifications
For this sample to run as dedicated server, we made the following changes:

- In `Assets/Scripts/ConnectionManagement/ConnectionMethod.cs`, the `SetConnectionPayload()` call in the `SetupHostConnectionAsync` function of the `ConnectionMethodIP` class was commented out, since the server is not a player.
- In `Assets/Scripts/ConnectionManagement/ConnectionState/StartingHostState.cs`, we changed `NetworkManager.StartHost()` with `NetworkManager.StartServer()` in the `StartHost` function.
- In `Assets/Scripts/Gameplay/GameplayObjects/Character/ClientCharacter.cs`, we commented out the `OnMovementStatusChanged()` call in the `OnNetworkSpawn` function.
- In `Assets/Scripts/ConnectionManagement/ConnectionState/HostingState.cs`, we commented out the following lines from the `GetConnectStatus` function in order to test the client from the editor.

```
if (connectionPayload.isDebug != Debug.isDebugBuild)
{
    return ConnectStatus.IncompatibleBuildType;
}
```

- We added `Assets/Scripts/EdgegapServerStarter.cs` with an empty gameObject to the `MainMenu` scene to to automatically call `connectionManager.StartHostIp()` on server launch, using `0.0.0.0` as the address, and the value of the `ARBITRIUM_PORT_GAMEPORT_INTERNAL` environment variable as the port. The value used for the player name does not matter in this case.
