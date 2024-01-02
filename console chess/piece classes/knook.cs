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
    public List<int> validateknook(int square, object[] board)
    {
        knight knight = new knight(side);
        rook rook = new rook(side);
        return rook.validaterook(square, board).Concat(knight.validateknight(square, board)).ToList();
    }
}