namespace ChessRules;

class Board
{
    public string fen { get; private set; }
    Figure[,] figures;
    public Color moveColor { get; private set; }
    public int moveNumber { get; private set; }
    public Board(string fen)
    {
        this.fen = fen;
        figures = new Figure[8, 8];
        Init();
    }
    bool CanEatKing()
    {
        Square harshKing = FindHarshKing();
        Moves moves = new Moves(this);
        foreach (FigureOnSquare fs in YieldFigures())
        {
            FigureMoving fm = new FigureMoving(fs, harshKing);
            if (moves.CanMove(fm))
                return true;
        }
        return false;
    }
    private Square FindHarshKing()
    {
        Figure harshKing=moveColor==Color.black
            ?Figure.whiteKing:Figure.blackKing;
        foreach (Square square in Square.YieldSquares())
            if (GetFigureAt(square) == harshKing)
                return square;
        return Square.none;
    }

    string FenFigure()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 7; i >= 0; i--)
        {
            for (int j = 0; j < 8; j++)
                sb.Append(figures[j, i] == Figure.none ? '1' : (char)figures[j, i]);
            if (i > 0)
                sb.Append('/');
        }
        string eight = "11111111";
        for (int i = 8; i >= 2; i--)
            sb.Replace(eight.Substring(0, i), i.ToString());
        return sb.ToString();
    }
    void GenFen()
    {
        fen = FenFigure() + " " +
               (moveColor == Color.white ? "w" : "b") +
               " - - 0 " + moveNumber.ToString();
    }
    public Figure GetFigureAt(Square square)
    {
        if (square.OnBoard())
            return figures[square.x, square.y];
        return Figure.none;
    }
    public Board Move(FigureMoving fiMo)
    {
        Board next = new Board(fen);
        next.SetFigureAt(fiMo.from, Figure.none);
        next.SetFigureAt(fiMo.to, fiMo.promotion == Figure.none ? fiMo.figure :
            fiMo.promotion);
        if (moveColor == Color.black)
            next.moveNumber++;
        next.moveColor = moveColor.FlipColor();
        next.GenFen();
        return next;
    }

    void Init()
    {
        string[] parts = fen.Split();
        if (parts.Length != 6)
            return;
        InitFigures(parts[0]);
        moveColor = (parts[1] == "b") ? Color.black
           : Color.white;
        moveNumber = int.Parse(parts[5]);
    }
    void InitFigures(string data)
    {
        for (int i = 8; i >= 2; i--)
            data = data.Replace(i.ToString(), (i - 1).ToString() + "1");
        data = data.Replace("1", ".");
        string[] lines = data.Split('/');
        for (int i = 7; i >= 0; i--)
            for (int j = 0; j < 8; j++)
                figures[i, j] = lines[7 - j][i] == '.' ? Figure.none :
                        (Figure)lines[7 - j][i];
    }
    public bool IsCheck()
    {
        Board afterMove = new Board(fen);
        afterMove.moveColor = moveColor.FlipColor();
        return afterMove.CanEatKing();
    }
    public bool IsCheckAfterMove(FigureMoving fm)
    {
        Board after=Move(fm);
        return after.CanEatKing();
    }
    void SetFigureAt(Square square, Figure figure)
    {
        if (square.OnBoard())
            figures[square.x, square.y] = figure;
    }
    public IEnumerable<FigureOnSquare> YieldFigures()
    {

        foreach (Square square in Square.YieldSquares())
            if (GetFigureAt(square).GetColor() == moveColor)
                yield return new FigureOnSquare(GetFigureAt(square), square);

    }
}
