using UnityEngine;

namespace Selftris.Tetris.Engine
{
    public static class PiecesManager
    {
        private static Piece[] pieces;

        public static Piece QueryPiece(int ID)
        {
            return pieces[ID];
        }

        public static void InitInfo()
        {
            pieces = new Piece[7];
            pieces[0] = O();
            pieces[1] = I();
            pieces[2] = T();
            pieces[3] = S();
            pieces[4] = Z();
            pieces[5] = J();
            pieces[6] = L();
        }

        private static Piece O()
        {
            return new Piece {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[1][] {
                    new Vector2Int[4] { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(1, 1) }
                },
                wallKickTable = new Vector2Int[][] { new Vector2Int[] { } }
            };
        }

        private static Piece I()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 19),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(-1, 0), new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(2, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, -2), new Vector2Int(0, -1), new Vector2Int(0, 0), new Vector2Int(0, 1) },
                    new Vector2Int[4] {  new Vector2Int(2, -1), new Vector2Int(1, -1), new Vector2Int(0, -1), new Vector2Int(-1, -1) },
                    new Vector2Int[4] {  new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, 2) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, -1) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, -2) , new Vector2Int(-2, 1) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, 2) , new Vector2Int(2, -1) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(2, 0), new Vector2Int(-1, 2) , new Vector2Int(2, -1) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-2, 0), new Vector2Int(1, 0), new Vector2Int(-2, -1) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-2, 0), new Vector2Int(1, -2) , new Vector2Int(-2, 1) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(2, 0), new Vector2Int(-1, 0), new Vector2Int(2, 1) , new Vector2Int(-1, -2) }
                }
            };
        }

        private static Piece T()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(0, 1) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
                }
            };
        }

        private static Piece S()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 1), new Vector2Int(0, 1), new Vector2Int(-1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int(0, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, -1), new Vector2Int(1, 0), new Vector2Int(0, 1) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
                }
            };
        }

        private static Piece Z()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 0), new Vector2Int(-1, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(1, 1) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
                }
            };
        }

        private static Piece L()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(-1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1), new Vector2Int(0, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, -1), new Vector2Int(0, 1) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
                }
            };
        }

        private static Piece J()
        {
            return new Piece
            {
                initPos = new Vector2Int(4, 18),
                occupationTable = new Vector2Int[4][] {
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(-1, 1), new Vector2Int(-1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, 1), new Vector2Int(-1, -1), new Vector2Int(0, -1) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(1, -1), new Vector2Int(1, 0) },
                    new Vector2Int[4] {  new Vector2Int(0, 0), new Vector2Int(0, -1), new Vector2Int(1, 1), new Vector2Int(0, 1) }
                },
                wallKickTable = new Vector2Int[8][]
                {
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, -2) , new Vector2Int(1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, 2) , new Vector2Int(-1, 2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(-1, 0), new Vector2Int(-1, 1), new Vector2Int(0, -2) , new Vector2Int(-1, -2) },
                    new Vector2Int[5] {  new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, -1), new Vector2Int(0, 2) , new Vector2Int(1, 2) }
                }
            };
        }
    }
}
