namespace Selftris.Tetris.Engine
{
    public static class PiecesManager
    {
        private static Piece[] pieces;

        public static Position[] GetOccupation(int ID, int rot)
        {
            return pieces[ID].occupationTable[rot];
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
                initPos = new Position(4, 18),
                occupationTable = new Position[1][] {
                    new Position[4] { new Position(0, 0), new Position(1, 0), new Position(0, 1), new Position(1, 1) }
                },
                wallKickTable = new Position[][] { new Position[] { } }
            };
        }

        private static Piece I()
        {
            return new Piece
            {
                initPos = new Position(4, 19),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(-1, 0), new Position(0, 0), new Position(1, 0), new Position(2, 0) },
                    new Position[4] {  new Position(0, -2), new Position(0, -1), new Position(0, 0), new Position(0, 1) },
                    new Position[4] {  new Position(2, -1), new Position(1, -1), new Position(0, -1), new Position(-1, -1) },
                    new Position[4] {  new Position(1, -1), new Position(1, 0), new Position(1, 1), new Position(1, 2) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-2, 0), new Position(1, 0), new Position(-2, -1) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(-2, 0), new Position(1, -2) , new Position(-2, 1) },
                    new Position[5] {  new Position(0, 0), new Position(2, 0), new Position(-1, 0), new Position(2, 1) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(2, 0), new Position(-1, 2) , new Position(2, -1) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(2, 0), new Position(-1, 2) , new Position(2, -1) },
                    new Position[5] {  new Position(0, 0), new Position(-2, 0), new Position(1, 0), new Position(-2, -1) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(-2, 0), new Position(1, -2) , new Position(-2, 1) },
                    new Position[5] {  new Position(0, 0), new Position(2, 0), new Position(-1, 0), new Position(2, 1) , new Position(-1, -2) }
                }
            };
        }

        private static Piece T()
        {
            return new Piece
            {
                initPos = new Position(4, 18),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(0, 0), new Position(1, 0), new Position(0, 1), new Position(-1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, 1), new Position(-1, 0), new Position(0, -1) },
                    new Position[4] {  new Position(0, 0), new Position(-1, 0), new Position(0, -1), new Position(1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, -1), new Position(1, 0), new Position(0, 1) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) }
                }
            };
        }

        private static Piece S()
        {
            return new Piece
            {
                initPos = new Position(4, 18),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(0, 0), new Position(1, 1), new Position(0, 1), new Position(-1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(-1, 1), new Position(-1, 0), new Position(0, -1) },
                    new Position[4] {  new Position(0, 0), new Position(-1, -1), new Position(0, -1), new Position(1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(1, -1), new Position(1, 0), new Position(0, 1) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) }
                }
            };
        }

        private static Piece Z()
        {
            return new Piece
            {
                initPos = new Position(4, 18),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(0, 0), new Position(1, 0), new Position(0, 1), new Position(-1, 1) },
                    new Position[4] {  new Position(0, 0), new Position(0, 1), new Position(-1, 0), new Position(-1, -1) },
                    new Position[4] {  new Position(0, 0), new Position(-1, 0), new Position(0, -1), new Position(1, -1) },
                    new Position[4] {  new Position(0, 0), new Position(0, -1), new Position(1, 0), new Position(1, 1) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) }
                }
            };
        }

        private static Piece L()
        {
            return new Piece
            {
                initPos = new Position(4, 18),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(-1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, 1), new Position(-1, 1), new Position(0, -1) },
                    new Position[4] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, -1), new Position(1, -1), new Position(0, 1) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) }
                }
            };
        }

        private static Piece J()
        {
            return new Piece
            {
                initPos = new Position(4, 18),
                occupationTable = new Position[4][] {
                    new Position[4] {  new Position(0, 0), new Position(1, 0), new Position(-1, 1), new Position(-1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, 1), new Position(-1, -1), new Position(0, -1) },
                    new Position[4] {  new Position(0, 0), new Position(-1, 0), new Position(1, -1), new Position(1, 0) },
                    new Position[4] {  new Position(0, 0), new Position(0, -1), new Position(1, 1), new Position(0, 1) }
                },
                wallKickTable = new Position[8][]
                {
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, 1), new Position(0, -2) , new Position(1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, -1), new Position(0, 2) , new Position(-1, 2) },
                    new Position[5] {  new Position(0, 0), new Position(-1, 0), new Position(-1, 1), new Position(0, -2) , new Position(-1, -2) },
                    new Position[5] {  new Position(0, 0), new Position(1, 0), new Position(1, -1), new Position(0, 2) , new Position(1, 2) }
                }
            };
        }
    }
}
