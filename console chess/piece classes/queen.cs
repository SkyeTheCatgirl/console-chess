using console_chess;

public class queen : Board
{
    public queen(int input)
    {
        value = 9;
        name = "Queen";
        identifier = "Q";
        side = input;
    }
    public List<int> validatequeen(int square)
    {
        bishop bishop = new bishop(side);
        rook rook = new rook(side);
        return rook.validaterook(square).Concat(bishop.validatebishop(square)).ToList();
    }
}