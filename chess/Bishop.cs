﻿using board;

namespace chess
{

    class Bishop : Piece
    {

        public Bishop(Board brd, Color color) : base(brd, color)
        {
        }

        public override string ToString()
        {
            return "B";
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

            // NW
            pos.setValues(position.line - 1, position.column - 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.setValues(position.line - 1, position.column + 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.setValues(position.line + 1, position.column + 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line + 1, pos.column + 1);
            }

            // SW
            pos.setValues(position.line + 1, position.column - 1);
            while (brd.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.piece(pos) != null && brd.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.line + 1, pos.column - 1);
            }

            return mat;
        }
    }
}