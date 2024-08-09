using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace AndreiScripts
{
    public class BoardPacket : BasePacket
    {
        public BoardState currentBoard = new BoardState();

        void Start()
        {

        }

        void Update()
        {

        }

        public BoardPacket()
        {
            currentBoard = new BoardState();
        }

        public BoardPacket(BoardState boardState, PlayerData playerData) :
            base(playerData, PacketType.BoardPacket)
        {
            BoardState newBoard = new BoardState();
            newBoard.boardObjects = new List<BoardObject>();
            for (int i = 0; i < boardState.boardObjects.Count; i++)
            {
                currentBoard.boardObjects.Add(boardState.boardObjects[i]);
            }
        }



        public byte[] Serialize()
        {
            BeginSerialize();

            for (int i = 0; i < currentBoard.boardObjects.Count; i++)
            {
                bw.Write(currentBoard.boardObjects[i].cardName);
            }

            return EndSerialize();
        }

        public BoardPacket Deserialize(byte[] buffer, int bufferOffset)
        {
            base.Deserialize(buffer, bufferOffset);

            this.currentBoard = new BoardState();

            BoardPacket packet = new BoardPacket();

            for (int i = 0; i < 4; i++)
            {
                this.currentBoard.boardObjects.Add(new BoardObject());
            }

            for (int i = 0; i < 4; i++)
            {
                this.currentBoard.boardObjects[i].cardName = br.ReadString();
            }

            return this;
        }

    }
}
