using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServerSide
{
    public class PacketHandler : MonoBehaviour
    {
        [MessageHandler((ushort)ClientToServerPacket.JoinLobby)]
        private static void JoinLobby(ushort clientID, Message message)
        {
            string name = message.GetString();

            if(name == null)
            {
                FindObjectOfType<NetworkManager>().Server.Send(
                    Message.Create(MessageSendMode.reliable,
                    ServerToClientPacket.SendAlert).AddString("Server: Your name is empty"),
                    clientID);
                return;
            }

            Debug.Log($"Somebody joined the lobby as {name}");
            new ServerPlayer(clientID, name);

            Message lobbyMsg = Message.Create(MessageSendMode.reliable, ServerToClientPacket.LoadLobby);
            FindObjectOfType<NetworkManager>().Server.Send(lobbyMsg, clientID);
        }

        [MessageHandler((ushort)ClientToServerPacket.ChangeReadyStatus)]
        private static void ChangeReadyStatus(ushort clientID, Message message)
        {
            ServerPlayer player = NetworkManager.Find(clientID);
            if(player == null)
            {
                Debug.LogError($"Unathorized ready status change from: {clientID}");
                return;
            }

            player.IsReady = message.GetBool();
        }
    }
}