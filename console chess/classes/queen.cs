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
}