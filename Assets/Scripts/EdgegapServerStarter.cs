using System;
using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.ConnectionManagement;
using UnityEngine;

namespace Unity.Multiplayer.Samples.BossRoom
{
    public class EdgegapServerStarter : MonoBehaviour
    {
        public string portMapName = "gameport";

        // Start is called before the first frame update
        void Start()
        {
            if (Application.isBatchMode)
            {
                ConnectionManager connectionManager = GameObject.Find("ConnectionManager").GetComponent<ConnectionManager>();
                string internalPortAsStr = Environment.GetEnvironmentVariable($"ARBITRIUM_PORT_{portMapName.ToUpper()}_INTERNAL");

                if (internalPortAsStr == null || !ushort.TryParse(internalPortAsStr, out ushort port))
                {
                    throw new Exception($"Could not find port mapping, make sure your app version port name matches with \"{portMapName}\"");
                }

                connectionManager.StartHostIp("SERVER", "0.0.0.0", port);
            }
        }
    }
}
