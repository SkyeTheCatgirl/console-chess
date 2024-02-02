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
    public queen(queen input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
    }
    public List<int> validatequeen(int square, object[] board)
    {
        bishop bishop = new bishop(side);
        rook rook = new rook(side);
        return rook.validaterook(square, board).Concat(bishop.validatebishop(square, board)).ToList();
    }
}