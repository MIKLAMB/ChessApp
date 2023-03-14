namespace ChessDemo;
class Program
{
    static void Main(string[] args)
    {
        #region Visual Part of Chess [text,interactive and so on...]
        Timer? timer = null;
        Random random = new Random();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Hi there my name is {ChessInfo.ChessName}{ChessInfo.ChessVersion} Welcome and Good luck " +
            "\nif you wont to get several information about our game please press 'Enter' " +
            "\nor if you already know how to play chess ");
        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("The player controlling the white pieces is named \"White\"; the player controlling the black pieces is named \"Black\". White moves first, then players alternate moves. Making a move is required; it is not legal to skip a move, even when having to move is detrimental. Play continues until a king is checkmated, a player resigns, or a draw is declared, as explained below. In addition, if the game is being played under a time control, a player who exceeds the time limit loses the game unless they cannot be checkmated.\r\n\r\nThe official chess rules do not include a procedure for determining who plays White. Instead, this decision is left open to tournament-specific rules (e.g. a Swiss system tournament or round-robin tournament) or, in the case of non-competitive play, mutual agreement, in which case some kind of random choice is often employed. A common method is for one player to conceal a piece (usually a pawn) of each color in either hand; the other player chooses a hand to open and receives the color of the piece that is revealed.\r\n\r\nBasic moves\r\n\r\nThe pawns can move to the squares marked \"×\" in front of them. The pawn on c6 can also take either black rook.\r\nEach type of chess piece has its own method of movement. A piece moves to a vacant square except when capturing an opponent's piece.\r\n\r\nExcept for any move of the knight and castling, pieces cannot jump over other pieces. A piece is captured (or taken) when an attacking enemy piece replaces it on its square. The captured piece is thereby permanently removed from the game. The king can be put in check but cannot be captured (see below).\r\n\r\nThe king moves exactly one square horizontally, vertically, or diagonally. A special move with the king known as castling is allowed only once per player, per game (see below).\r\nA rook moves any number of vacant squares horizontally or vertically. It also is moved when castling.\r\nA bishop moves any number of vacant squares diagonally.\r\nThe queen moves any number of vacant squares horizontally, vertically, or diagonally.\r\nA knight moves to one of the nearest squares not on the same rank, file, or diagonal. (This can be thought of as moving two squares horizontally then one square vertically, or moving one square horizontally then two squares vertically—i.e. in an \"L\" pattern.) The knight is not blocked by other pieces; it jumps to the new location.\r\nPawns have the most complex rules of movement:\r\nA pawn moves straight forward one square, if that square is vacant. If it has not yet moved, a pawn also has the option of moving two squares straight forward, provided both squares are vacant. Pawns cannot move backwards.\r\nA pawn, unlike other pieces, captures differently from how it moves. A pawn can capture an enemy piece on either of the two squares diagonally in front of the pawn. It cannot move to those squares when vacant except when capturing en passant.\r\nThe pawn is also involved in the two special moves en passant and promotion.");
        }
        else
        {
            Console.WriteLine("u get me?\n if yes press enter again (its your last opportunity)");
            if (Console.ReadKey().Key == ConsoleKey.Enter)
                Console.WriteLine("HERE WE GO GOOD GAME"); 
        }
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Please Enter Chess Fen (if u don't chess will starts with default layout)");
        timer = new Timer(callback: TimerCallback, null, 0, 2000);
        if (Timer.ActiveCount > 10000)
            Console.WriteLine("btw put '1' in the spaces to get program understand");
        #endregion
        Console.ForegroundColor = ConsoleColor.Gray;
        ChessRules.Chess chess = new ChessRules.Chess("rnbqkbnr/1p1111p1/8/8/8/8/1P1111P1/RNBQKBNR w KQkq - 0 1");
        var enteredFen = Console.ReadLine();
        if (enteredFen == null || enteredFen == "" || enteredFen == " ")
            chess = new ChessRules.Chess("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        else
            chess = new ChessRules.Chess(enteredFen);

        List<string> movesInChess;

        while (true)
        {
            movesInChess = chess.GetAllMoves();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Fen - " + chess.fen);
            Console.WriteLine();
            Print(ChessToAscii(chess));
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(chess.IsCheck() ? "CHECK " : "- ");
            Console.WriteLine("\nALL POSIBLE MOVES`\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            foreach (string moves in movesInChess)
                Console.Write(moves + " \t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");
            Console.Write("> ");
            string move = Console.ReadLine();
            if (move == "q")
                break;
            if (move == "")
                move = movesInChess[random.Next(movesInChess.Count)];
            chess = chess.Move(move);
            Console.WriteLine("\n");
        }
    }
    static string ChessToAscii(ChessRules.Chess chess)
    {

        string text = "  +-----------------+\n";
        for (int i = 7; i >= 0; i--)
        {
            text += i + 1;
            text += " | ";
            for (int j = 0; j < 8; j++)
                text += chess.GetFifureAt(j, i) + " ";
            text += "|\n";
        }
        text += "  +-----------------+\n";
        text += "    a b c d e f g h\n";
        return text;
    }
    static void Print(string text)
    {
        ConsoleColor color = Console.ForegroundColor;
        foreach (char x in text)
        {
            if (x >= 'a' && x <= 'z')
                Console.ForegroundColor = ConsoleColor.Red;
            else if (x >= 'A' && x <= 'Z')
                Console.ForegroundColor = ConsoleColor.Gray;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(x);
        }
        Console.ForegroundColor = color;
    }
    private static void TimerCallback(Object o)
    {

    }
}