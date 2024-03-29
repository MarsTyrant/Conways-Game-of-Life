namespace GameOfLife
{
    public class Grid : IGrid
    {
        public Cell[,] Cells { get; set; }
        public int Generation { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Generation = 0;
            Cells = new Cell[columns, rows];
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Cells[i,j] = new Cell();
        }
        public void Randomize()
        {
            Random rand = new();
            for (int i = 0; i < Columns; i++)
                for (int j = 0; j < Rows; j++)
                    Cells[i,j].SetCellState(rand.NextDouble() >= 0.5);
        }
        public void SetGeneration(int generation)
        {
            Generation = generation;
        }
        public void Update(Cell[,] newGrid)
        {
            if (Cells.GetLength(0) != newGrid.GetLength(0) || Cells.GetLength(1) != newGrid.GetLength(1))
                throw new ArgumentOutOfRangeException("newGrid dimensions do not match those of existing grid!");
            
            Cells = newGrid;
        }
        public int GetLiveCellNeighborCount(int x, int y)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException(x.ToString());
            else if (y < 0)
                throw new ArgumentOutOfRangeException(y.ToString());
            else if (x >= Columns)
                throw new ArgumentOutOfRangeException(x.ToString());
            else if (y >= Rows)
                throw new ArgumentOutOfRangeException(y.ToString());

            int liveNeighbors = 0;

            bool topEdge = y == 0;
            bool leftEdge = x == 0;
            bool rightEdge = x == Columns - 1;
            bool bottomEdge = y == Rows - 1;

            // Start at east, then iterate over all neighbors clockwise.
            if (!rightEdge && Cells[x+1, y].GetCellState()) // east
                liveNeighbors++;
            if (!rightEdge && !bottomEdge && Cells[x+1, y+1].GetCellState()) // southeast
                liveNeighbors++;
            if (!bottomEdge && Cells[x, y+1].GetCellState()) // south
                liveNeighbors++;
            if (!bottomEdge && !leftEdge && Cells[x-1, y+1].GetCellState()) // southwest
                liveNeighbors++;
            if (!leftEdge && Cells[x-1, y].GetCellState()) // west
                liveNeighbors++;
            if (!leftEdge && !topEdge && Cells[x-1, y-1].GetCellState()) // northwest
                liveNeighbors++;
            if (!topEdge && Cells[x, y-1].GetCellState()) // north
                liveNeighbors++;
            if (!topEdge && !rightEdge && Cells[x+1, y-1].GetCellState()) // northeast
                liveNeighbors++;
            
            return liveNeighbors;
        }
    }
}