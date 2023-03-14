namespace ChessRules;

class FigureMoving
{
    public Figure figure { get; private set; }
    public Square from { get; private set; }
    public Square to { get; private set; }
    public Figure promotion { get; private set; }

    public int DeltaX { get { return to.x - from.x; } }
    public int DeltaY { get { return to.y - from.y; } }

    public int AbsDeltaX { get { return Math.Abs(DeltaX); } }
    public int AbsDeltaY { get { return Math.Abs(DeltaY); } }

    public int SignX { get { return Math.Sign(DeltaX); } }
    public int SignY { get { return Math.Sign(DeltaY); } }

    public FigureMoving(FigureOnSquare figOnSq, Square to, Figure promotion = Figure.none)
    {
        this.figure = figOnSq.figure;
        this.from = figOnSq.square;
        this.to = to;
        this.promotion = promotion;
    }
    public FigureMoving(string move)//Pe2e4  //Pe7e8Q
    {
        try
        {
            this.figure = (Figure)move[0];
            this.from = new Square(move.Substring(1, 2));
            this.to = new Square(move.Substring(3, 2));//from 3- 2 byt
            this.promotion = (move.Length == 6) ?
                 (Figure)move[5] : Figure.none;
        }
        catch (Exception ex)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string varning = "wrong step please try again (Error 0x0008dl - 'Incomprehensible Step')";
            Console.WriteLine(varning);
        }
    }
    public override string ToString()
    {
        string text = (char)figure + from.Name + to.Name;
        if (promotion != Figure.none)
            text += (char)promotion;
        return text;
    }
}
