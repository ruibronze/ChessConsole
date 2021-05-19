using board;

namespace chess
{

    class Pawn : Piece
    {

        private ChessMatch match;

        public Pawn(Board brd, Color color, ChessMatch match) : base(brd, color)
        {
            this.match = match;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool anyEnemy(Position pos)
        {
            Piece p = brd.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return brd.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                pos.setValues(position.line - 1, position.column);
                if (brd.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (brd.validPosition(p2) && free(p2) && brd.validPosition(pos) && free(pos) && moveQty == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 1, position.column - 1);
                if (brd.validPosition(pos) && anyEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line - 1, position.column + 1);
                if (brd.validPosition(pos) && anyEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // // #EspecialMove En Passant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (brd.validPosition(left) && anyEnemy(left) && brd.piece(left) == match.enPassantVuln)
                    {
                        mat[left.line - 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (brd.validPosition(right) && anyEnemy(right) && brd.piece(right) == match.enPassantVuln)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
            }
            else
            {
                pos.setValues(position.line + 1, position.column);
                if (brd.validPosition(pos) && free(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (brd.validPosition(p2) && free(p2) && brd.validPosition(pos) && free(pos) && moveQty == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 1, position.column - 1);
                if (brd.validPosition(pos) && anyEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.setValues(position.line + 1, position.column + 1);
                if (brd.validPosition(pos) && anyEnemy(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #EspecialMove En Passant
                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (brd.validPosition(left) && anyEnemy(left) && brd.piece(left) == match.enPassantVuln)
                    {
                        mat[left.line + 1, left.column] = true;
                    }
                    Position right = new Position(position.line, position.column + 1);
                    if (brd.validPosition(right) && anyEnemy(right) && brd.piece(right) == match.enPassantVuln)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
            }
            return mat;
        }
    }
}