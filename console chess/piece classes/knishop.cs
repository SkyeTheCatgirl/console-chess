using console_chess;

public class knishop : Board
{
    public knishop(int input)
    {
        value = 6;
        name = "Knishop";
        identifier = "I";
        side = input;
    }
    public List<int> validateknishop(int square, object[] board)
    {
        knight knight = new knight(side);
        bishop bishop = new bishop(side);
        return bishop.validatebishop(square, board).Concat(knight.validateknight(square, board)).ToList();
    }
}