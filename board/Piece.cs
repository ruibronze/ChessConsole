namespace board
{
    abstract class Piece
    {

        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moveQty { get; protected set; }
        public Board brd { get; protected set; }

        public Piece(Board brd, Color cor)
        {
            this.position = null;
            this.brd = brd;
            this.color = color;
            this.moveQty = 0;
        }

        public void MoveQtyIncrease()
        {
            moveQty++;
        }

        public void MoveQtyDecrease()
        {
            moveQty--;
        }

        public bool anyPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < brd.lines; i++)
            {
                for (int j = 0; j < brd.colunms; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool possibleMoves(Position pos)
        {
            return possibleMoves()[pos.line, pos.colnm];
        }

        public abstract bool[,] possibleMoves();
    }
}
