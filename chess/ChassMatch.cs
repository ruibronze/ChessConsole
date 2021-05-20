using System.Collections.Generic;
using board;

namespace chess
{
    class ChessMatch
    {

        public Board brd { get; private set; }
        public int shift { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool check { get; private set; }
        public Piece enPassantVuln { get; private set; }

        public ChessMatch()
        {
            brd = new Board(8, 8);
            shift = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            enPassantVuln = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            setPieces();
        }

        public Piece moveExecution(Position origin, Position destiny)
        {
            Piece p = brd.removePiece(origin);
            p.MoveQtyIncrease();
            Piece capturedPiece = brd.removePiece(destiny);
            brd.setPiece(p, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #EspecialMove Small Castling
            if (p is King && destiny.column == destiny.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinyT = new Position(origin.line, origin.column + 1);
                Piece T = brd.removePiece(originT);
                T.MoveQtyIncrease();
                brd.setPiece(T, destinyT);
            }

            // #EspecialMove Big Castling
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinyT = new Position(origin.line, origin.column - 1);
                Piece T = brd.removePiece(originT);
                T.MoveQtyIncrease();
                brd.setPiece(T, destinyT);
            }

            // #EspecialMove en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(destiny.line + 1, destiny.column);
                    }
                    else
                    {
                        posP = new Position(destiny.line - 1, destiny.column);
                    }
                    capturedPiece = brd.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void undoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = brd.removePiece(destiny);
            p.MoveQtyDecrease();
            if (capturedPiece != null)
            {
                brd.setPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            brd.setPiece(p, origin);

            // #EspecialMove Small Castling
            if (p is King && destiny.column == origin.column + 2)
            {
                Position originT = new Position(origin.line, origin.column + 3);
                Position destinyT = new Position(origin.line, origin.column + 1);
                Piece T = brd.removePiece(destinyT);
                T.MoveQtyDecrease();
                brd.setPiece(T, originT);
            }

            // #EspecialMove Big Castling
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originT = new Position(origin.line, origin.column - 4);
                Position destinyT = new Position(origin.line, origin.column - 1);
                Piece T = brd.removePiece(destinyT);
                T.MoveQtyDecrease();
                brd.setPiece(T, originT);
            }

            // #EspecialMove en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == enPassantVuln)
                {
                    Piece pawn = brd.removePiece(destiny);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, destiny.column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.column);
                    }
                    brd.setPiece(pawn, posP);
                }
            }
        }

        public void makeAMove(Position origin, Position destiny)
        {
            Piece capturedPiece = moveExecution(origin, destiny);

            if (inCheck(currentPlayer))
            {
                undoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put your King in Check!");
            }

            Piece p = brd.piece(destiny);

            // #EspecialMove Promotion
            if (p is Pawn)
            {
                if ((p.color == Color.White && destiny.line == 0) || (p.color == Color.Black && destiny.line == 7))
                {
                    p = brd.removePiece(destiny);
                    pieces.Remove(p);
                    Piece queen = new Queen(brd, p.color);
                    brd.setPiece(queen, destiny);
                    pieces.Add(queen);
                }
            }

            if (inCheck(opponent(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (checkmateTeste(opponent(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                shift++;
                changePlayer();
            }

            // #EspecialMove en passant
            if (p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2))
            {
                enPassantVuln = p;
            }
            else
            {
                enPassantVuln = null;
            }

        }

        public void validateOriginPosition(Position pos)
        {
            if (brd.piece(pos) == null)
            {
                throw new BoardException("No piece on the choosen position!");
            }
            if (currentPlayer != brd.piece(pos).color)
            {
                throw new BoardException("This is not your piece to move!");
            }
            if (!brd.piece(pos).anyPossibleMoves())
            {
                throw new BoardException("This piece has no possible movements!");
            }
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!brd.piece(origin).possibleMoves(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        private Color opponent(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool inCheck(Color color)
        {
            Piece R = king(color);
            if (R == null)
            {
                throw new BoardException("There is no " + color + " King on the board!");
            }
            foreach (Piece x in piecesInGame(opponent(color)))
            {
                bool[,] mat = x.possibleMoves();
                if (mat[R.position.line, R.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool checkmateTeste(Color color)
        {
            if (!inCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for (int i = 0; i < brd.lines; i++)
                {
                    for (int j = 0; j < brd.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = moveExecution(origin, destiny);
                            bool checkTest = inCheck(color);
                            undoMove(origin, destiny, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void setNewPiece(char column, int line, Piece piece)
        {
            brd.setPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void setPieces()
        {
            setNewPiece('a', 1, new Tower(brd, Color.White));
            setNewPiece('b', 1, new Knight(brd, Color.White));
            setNewPiece('c', 1, new Bishop(brd, Color.White));
            setNewPiece('d', 1, new Queen(brd, Color.White));
            setNewPiece('e', 1, new King(brd, Color.White, this));
            setNewPiece('f', 1, new Bishop(brd, Color.White));
            setNewPiece('g', 1, new Knight(brd, Color.White));
            setNewPiece('h', 1, new Tower(brd, Color.White));
            setNewPiece('a', 2, new Pawn(brd, Color.White, this));
            setNewPiece('b', 2, new Pawn(brd, Color.White, this));
            setNewPiece('c', 2, new Pawn(brd, Color.White, this));
            setNewPiece('d', 2, new Pawn(brd, Color.White, this));
            setNewPiece('e', 2, new Pawn(brd, Color.White, this));
            setNewPiece('f', 2, new Pawn(brd, Color.White, this));
            setNewPiece('g', 2, new Pawn(brd, Color.White, this));
            setNewPiece('h', 2, new Pawn(brd, Color.White, this));

            setNewPiece('a', 8, new Tower(brd, Color.Black));
            setNewPiece('b', 8, new Knight(brd, Color.Black));
            setNewPiece('c', 8, new Bishop(brd, Color.Black));
            setNewPiece('d', 8, new Queen(brd, Color.Black));
            setNewPiece('e', 8, new King(brd, Color.Black, this));
            setNewPiece('f', 8, new Bishop(brd, Color.Black));
            setNewPiece('g', 8, new Knight(brd, Color.Black));
            setNewPiece('h', 8, new Tower(brd, Color.Black));
            setNewPiece('a', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('b', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('c', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('d', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('e', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('f', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('g', 7, new Pawn(brd, Color.Black, this));
            setNewPiece('h', 7, new Pawn(brd, Color.Black, this));
        }
    }
}