﻿using UnityEngine;
using UnityEngine.UI;
using Selftris.Tetris.Engine;
using Selftris.Tetris.Engine.Logics;

namespace Selftris.Tetris.Unity
{
    public class BoardRenderer : Logic
    {
        public BoardRenderer(Color emptyColor, Color occupiedColor, Color activeColor, RawImage renderBoard)
        {
            this.emptyColor = emptyColor;
            this.occupiedColor = occupiedColor;
            this.activeColor = activeColor;
            this.renderBoard = renderBoard;
        }

        private const int displayHeight = 20;
        private Color[] colorOccupancy = new Color[displayHeight * 10];     // flattened board
        private Color emptyColor;
        private Color occupiedColor;
        private Color activeColor;   // color for the current piece
        private RawImage renderBoard;

        public override void Update(float dt)
        {
            Render();
            // Show it on the RawImage
            Texture2D renderTexture = new Texture2D(10, displayHeight);
            renderTexture.SetPixels(colorOccupancy);
            renderTexture.filterMode = FilterMode.Point;
            renderTexture.Apply(false);
            renderBoard.texture = renderTexture;
        }

        public void Render()
        {
            Board board = (Board) player.GetLogic("board");

            // Update colorOccupancy vector
            for (int i = 0; i < displayHeight; i++)      // dont render block out of the board
            {
                for (int j = 0; j < board.occupancy[0].Length; j++)      // occupated and empty box coloring
                {
                    colorOccupancy[i * 10 + j] = board.occupancy[i][j] >= 0 ? occupiedColor : emptyColor;
                }
            }

            Vector2Int[] checkBlock = PiecesManager.QueryPiece(board.curPieceID).occupationTable[board.curPieceRot];
            for (int i = 0; i < checkBlock.Length; i++)
            {
                Vector2Int vecBlock = checkBlock[i] + board.curPiecePos;
                if (vecBlock.y >= displayHeight) { continue; }      // dont render block out of the board
                colorOccupancy[vecBlock.y * 10 + vecBlock.x] = activeColor;
            }
        }
    }
}