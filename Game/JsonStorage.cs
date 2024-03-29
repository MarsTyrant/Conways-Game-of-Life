using System.Text.Json;

namespace GameOfLife
{
    public class JsonStorage : IStorage
    {
        // The Cells array in Grid cannot be serialized to JSON directly, so instead we use the struct below.
        public struct JsonGrid(Grid inputGrid)
        {
            public int Generation { get; set; } = inputGrid.Generation;
            public int Rows { get; set; } = inputGrid.Rows;
            public int Columns { get; set; } = inputGrid.Columns;
            public bool[][] Grid { get; set; }
        }
        public Grid LoadGrid(string filePath)
        {
            try
            {
                string jsonString = File.ReadAllText(filePath);
                JsonGrid jsonGrid = JsonSerializer.Deserialize<JsonGrid>(jsonString)!;
                Grid grid = new(jsonGrid.Rows,jsonGrid.Columns);
                grid.SetGeneration(jsonGrid.Generation);

                // Convert array of array of booleans to 2D array of Cell objects.
                for (int i = 0; i < jsonGrid.Columns; i++)
                    for (int j = 0; j < jsonGrid.Rows; j++)
                        grid.Cells[i,j].SetCellState(jsonGrid.Grid[i][j]);
                
                return grid;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
        }
        public void StoreGrid(Grid grid)
        {
            // Convert 2D array of Cell objects into serializable array of arrays of booleans.
            bool[][] jsonCells = new bool[grid.Columns][];
            for (int i = 0; i < grid.Columns; i++)
            {
                bool[] jsonGridRow = new bool[grid.Rows];
                for (int j = 0; j < grid.Rows; j++)
                    jsonGridRow[j] = grid.Cells[i,j].GetCellState();
                jsonCells[i] = jsonGridRow;
            }

            JsonGrid jsonGrid = new(grid)
            {
                Grid = jsonCells
            };

            string fileName = $@"Grids\\grid-{DateTime.Now:yyyy-dd-M-HH-mm-ss}.json"; // All saved files contain a date/timestamp.
            string jsonString = JsonSerializer.Serialize(jsonGrid);
            File.WriteAllText(fileName,jsonString);
        }
    }
}