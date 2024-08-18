public class PlayersStatesPacket : BasePacket
{
    //public 
    public PlayerState[] playerStates = new PlayerState[2];
    
    
    public PlayersStatesPacket() { }
    
    public PlayersStatesPacket(PlayerData playerData, PlayerState[] playerStates): base(PacketType.PlayersStates, playerData)
    {
        //this.playerStates = playerStates;
        for (int i = 0; i < 2; i++)
        {
            if (this.playerStates[i] == null)
            {
                this.playerStates[i] = new PlayerState();
            }

            this.playerStates[i].currentHp = playerStates[i].currentHp;
            this.playerStates[i].currentMana = playerStates[i].currentMana;
            this.playerStates[i].playerName = playerStates[i].playerName;
        }
    }
    
    public new byte[] Serialize()
    {
        base.Serialize();
        for(int i = 0; i < this.playerStates.Length; i++)
        {
            binaryWriter.Write(playerStates[i].currentHp);
            binaryWriter.Write(playerStates[i].currentMana);
            binaryWriter.Write(playerStates[i].playerName);
        }

        return serializeMemoryStream.ToArray();
    }

    public new PlayersStatesPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        for(int i = 0; i < 2;i++)
        {

            if (playerStates[i] == null)
            {
                playerStates[i] = new PlayerState();
            }

            playerStates[i].currentHp = binaryReader.ReadInt32();
            playerStates[i].currentMana = binaryReader.ReadInt32();
            playerStates[i].playerName = binaryReader.ReadString();
        }

        return this;
    }
}
