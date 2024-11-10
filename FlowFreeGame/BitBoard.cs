namespace FlowFreeSolver
{
    public class BitBoard : IEquatable<BitBoard>
    {
        private bool[,] board;

        public int Tamaño { get; private set; }

        public BitBoard(int tamaño)
        {
            Tamaño = tamaño;
            board = new bool[tamaño, tamaño];
        }

        public bool GetCell(int x, int y)
        {
            return board[x, y];
        }

        public void SetCell(int x, int y)
        {
            board[x, y] = true;
        }

        public void ClearCell(int x, int y)
        {
            board[x, y] = false;
        }

        public BitBoard Clone()
        {
            var nuevo = new BitBoard(Tamaño);
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    nuevo.board[i, j] = this.board[i, j];
                }
            }
            return nuevo;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as BitBoard);
        }

        public bool Equals(BitBoard? other)
        {
            if (other == null)
                return false;

            if (this.Tamaño != other.Tamaño)
                return false;

            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    if (this.board[i, j] != other.board[i, j])
                        return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = Tamaño;
            for (int i = 0; i < Tamaño; i++)
            {
                for (int j = 0; j < Tamaño; j++)
                {
                    hash = hash * 31 + (board[i, j] ? 1 : 0);
                }
            }
            return hash;
        }
    }
}