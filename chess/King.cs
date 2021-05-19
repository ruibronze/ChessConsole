using board;

namespace chess
{
    class King : Piece
    {

        private ChessMatch match;

        public King(Board brd, Color color, ChessMatch match) : base(brd, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = brd.piece(pos);
            return p == null || p.color != color;
        }

        private bool castlingTowerTest(Position pos)
        {
            Piece p = brd.piece(pos);
            return p != null && p is Tower && p.color == color && p.moveQty == 0;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            // UP
            pos.setValues(position.line - 1, position.column);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // NE
            pos.setValues(position.line - 1, position.column + 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // RIGHT
            pos.setValues(position.line, position.column + 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // SE
            pos.setValues(position.line + 1, position.column + 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // DOWN
            pos.setValues(position.line + 1, position.column);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // SW
            pos.setValues(position.line + 1, position.column - 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // LEFT
            pos.setValues(position.line, position.column - 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // NW
            pos.setValues(position.line - 1, position.column - 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // #EspecialMove Castling
            if (moveQty == 0 && !match.check)
            {
                // #EspecialMove Small Castling
                Position posT1 = new Position(position.line, position.column + 3);
                if (castlingTowerTest(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (brd.piece(p1) == null && brd.piece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                // #EspecialMove Big Castling
                Position posT2 = new Position(position.line, position.column - 4);
                if (castlingTowerTest(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (brd.piece(p1) == null && brd.piece(p2) == null && brd.piece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
