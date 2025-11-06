using Photon.Realtime;
using System.Collections.Generic;
using Photon.Client;

public abstract class QuantumNetworkCallbacks : IConnectionCallbacks,
	IMatchmakingCallbacks,
	IInRoomCallbacks
{
	public virtual void OnConnected() {}
	public virtual void OnConnectedToMaster() {}
	public virtual void OnCreatedRoom() {}
	public virtual void OnCreateRoomFailed(short returnCode, string message) {}
	public virtual void OnCustomAuthenticationFailed(string debugMessage) {}
	public virtual void OnCustomAuthenticationResponse(Dictionary<string, object> data) {}
	public virtual void OnDisconnected(DisconnectCause cause) {}
	public virtual void OnFriendListUpdate(List<FriendInfo> friendList) {}
	public virtual void OnJoinedRoom() {}
	public virtual void OnJoinRandomFailed(short returnCode, string message) {}
	public virtual void OnJoinRoomFailed(short returnCode, string message) {}
	public virtual void OnLeftRoom() {}
	public virtual void OnMasterClientSwitched(Player newMasterClient) {}
	public virtual void OnPlayerEnteredRoom(Player newPlayer) {}
	public virtual void OnPlayerLeftRoom(Player otherPlayer) {}
	public virtual void OnPlayerPropertiesUpdate(Player targetPlayer, PhotonHashtable changedProps) {}
	public virtual void OnRegionListReceived(RegionHandler regionHandler) {}
	public virtual void OnRoomPropertiesUpdate(PhotonHashtable propertiesThatChanged) {}
}
