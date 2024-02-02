using console_chess;

public class knook : Board
{
    public knook(int input)
    {
        value = 7;
        name = "Knook";
        identifier = "O";
        side = input;
    }
    public knook(knook input, bool x)
    {
        value = input.value;
        side = input.side;
        killCount = input.killCount;
        name = input.name;
        identifier = input.identifier;
    }
    public List<int> validateknook(int square, object[] board)
    {
        knight knight = new knight(side);
        rook rook = new rook(side);
        return rook.validaterook(square, board).Concat(knight.validateknight(square, board)).ToList();
    }
}