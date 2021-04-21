using UnityEngine;
namespace Selftris.Tetris.Unity
{
    public class BoardRenderer : MonoBehaviour
    {
        Color[] colorOccupancy = new Color[400];     // flattened board
        public Color emptyColor;
        public Color occupiedColor;
        public Color activeColor;   // color for the current piece
        public RawImage renderBoard;

        public void Render()
        {
            // Update colorOccupancy vector
            for (int i = 0; i < 20; i++)      // dont render block out of the board
            {
                for (int j = 0; j < occupancy[0].Length; j++)      // occupated and empty box coloring
                {
                    if (occupancy[i][j])
                    {
                        colorOccupancy[i * 10 + j] = occupiedColor;
                    }
                    else
                    {
                        colorOccupancy[i * 10 + j] = emptyColor;
                    }
                }
            }
            Vector2Int[] checkBlock = curPiece.OccupationTest(curRot);
            for (int i = 0; i < checkBlock.Length; i++)
            {
                Vector2Int vecBlock = checkBlock[i] + curPos;
                if (vecBlock.y > 19) { continue; }      // dont render block out of the board
                colorOccupancy[vecBlock.y * 10 + vecBlock.x] = activeColor;
            }

            // Render it to the RawImage
            Texture2D renderTexture = new Texture2D(10, 20);
            renderTexture.SetPixels(colorOccupancy);
            renderTexture.filterMode = FilterMode.Point;
            renderTexture.Apply(false);
            renderBoard.texture = renderTexture;
        }
    }
}
