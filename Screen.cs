using System;
using System.Collections.Generic;
using board;
using chess;

namespace xadrez_console
{
    class Screen
    {

        public static void printMatch(ChessMatch match)
        {
            printMatch(match.brd);
            Console.WriteLine();
            printCapturedPieces(match); 

            Console.WriteLine();
            Console.WriteLine("Shift: " + match.shift);
            if (!match.finished)
            {
                Console.WriteLine("Waiting a move: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("  !! CHECK !!");
                }
            }
            else
            {
                Console.WriteLine("  !! CHECKMATE !!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }

        public static void printCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("Whites: ");
           printCollection(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printCollection(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printCollection(HashSet<Piece> collection)
        {
            Console.Write("[");
            foreach (Piece x in collection)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void printBoard(Board brd)
        {

            for (int i = 0; i < brd.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.columns; j++)
                {
                    printPiece(brd.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board brd, bool[,] possiblePositions)
        {

            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor otherBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < brd.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = otherBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    printPiece(brd.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

    }
}