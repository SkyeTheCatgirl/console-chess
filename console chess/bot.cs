namespace console_chess
{
    class bot
    {
        public static void scanXMovesAhead(Int16 X)
        {
            //To break it down;
            //List<int> = all moves a piece can make
            //List<List<int>> = all pieces on a board
            //List<List<List<int>>> = all possible boards in a move
            //LLL<int>[X] = amount of moves ahead
            List<List<List<int>>>[] futureboards = new List<List<List<int>>>[X];
        }
        private void allMoveValidations(object[] board) //runs move validation for every position on the board and adds it to a list
        {
            List<List<int>> allmoves = new List<List<int>>(); //List that contains lists of each pieces possible moves.
            for (int i = 0; i < 64; i++)
            {
               allmoves.Add(Globals.validateMoves(board[i]));
            }
        }
    }
}