using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;

public class PhotonRoom : MonoBehaviourPunCallbacks
{
    private float countdownTime = 6f; // Thời gian đếm ngược
    private bool roomCreated = false;
    private List<RoomInfo> roomList = new List<RoomInfo>();

    void Start()
    {
        // Kết nối đến Photon server sử dụng các cài đặt mặc định
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        // Tham gia vào Lobby khi kết nối thành công đến Master Server
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        // Bắt đầu đếm ngược 10 giây sau khi tham gia vào Lobby
        StartCoroutine(CountdownAndCreateOrJoinRoom());
    }

    IEnumerator CountdownAndCreateOrJoinRoom()
    {
        while (countdownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countdownTime--;
        }

        // Kiểm tra hoặc tạo phòng sau khi đếm ngược kết thúc
        CheckOrCreateRoom();
    }

    void CheckOrCreateRoom()
    {
        if (roomCreated)
            return;

        roomCreated = true;

        // Tìm phòng có nhiều người chơi nhất từ danh sách phòng cập nhật
        RoomInfo bestRoom = null;
        int maxPlayers = 0;

        if (roomList.Count > 0)
        {
            foreach (RoomInfo room in roomList)
            {
                if (room.PlayerCount > maxPlayers && room.PlayerCount < room.MaxPlayers)
                {
                    bestRoom = room;
                    maxPlayers = room.PlayerCount;
                }
            }
        }

        if (bestRoom != null)
        {
            // Tham gia phòng có nhiều người chơi nhất
            PhotonNetwork.JoinRoom(bestRoom.Name);
        }
        else
        {
            // Nếu không có phòng phù hợp hoặc danh sách phòng rỗng, tạo phòng mới với tên ngẫu nhiên
            string roomName = Random.Range(1000, 9999).ToString();
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 });
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        // Khi tham gia phòng thành công, chuyển sang scene chơi chính thức
        PhotonNetwork.LoadLevel("waiting room");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log("Create Room Failed: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Join Room Failed: " + message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        this.roomList = roomList;
    }
}

