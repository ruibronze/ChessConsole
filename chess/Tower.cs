using board;

namespace chess
{
    class Tower : Piece
    {

        public Tower(Board brd, Color color) : base(brd, color)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool canMove(Position pos)
        {
            Piece p = brd.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            // UP
            pos.setValues(position.line - 1, position.column);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            // DOWN
            pos.setValues(position.line + 1, position.column);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }

            // RIGHT
            pos.setValues(position.line, position.column + 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }

            // LEFT
            pos.setValues(position.line, position.column - 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }
            return mat;
        }
    }
}