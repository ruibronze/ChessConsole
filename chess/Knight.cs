using board;

namespace chess
{

    class Knight : Piece
    {

        public Knight(Board brd, Color color) : base(brd, color)
        {
        }

        public override string ToString()
        {
            return "Kn";
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

            pos.setValues(position.line - 1, position.column - 2);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line - 2, position.column - 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line - 2, position.column + 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line - 1, position.column + 2);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line + 1, position.column + 2);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line + 2, position.column + 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line + 2, position.column - 1);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.setValues(position.line + 1, position.column - 2);
            if (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            return mat;
        }
    }
}