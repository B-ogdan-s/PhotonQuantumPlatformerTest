using Photon.Realtime;
using UnityEngine;

public class QuantumClientUpdater : MonoBehaviour
{
    public RealtimeClient Client;

	void Update()
    {
        if (Client != null)
            Client.Service();
    }
}
