using System.Windows.Controls;

namespace UITier.HallMap
{
    // Part of the Template Method design pattern
    //
    // Defines the "algorithm steps" methods to create a Hall Map of Thrust hall.
    // Thrust hall is a hall with three stands that surrounding the left, right and front sides of the stage
    internal class ThrustMapGrid : HallMapGrid
    {
        // Sets the values of the properties 'GridRows' and 'GridColumns'.
        protected override void SetGridRowsAndGridColumns()
        {
            GridRows = Lines + 2 + SeatsInLine / 3;
            GridColumns = Lines * 2 + 2 + SeatsInLine / 3;
        }

        // Adds to 'HallMap' the elements that represent the seats lines number.
        protected override void AddLinesNumbers()
        {
            for (int c = 0; c < Lines; c++) //Left stand
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{Lines - c}");
                AddElementToGrid(0, c, textBlock);
            }

            for (int c = GridColumns - Lines; c < GridColumns; c++) // Right stand
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{c + Lines - GridColumns + 1}");
                AddElementToGrid(0, c, textBlock);
            }

            for (int r = 2 + SeatsInLine / 3; r < GridRows; r++) // Front stand
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{r - (2 + SeatsInLine / 3) + 1}");
                AddElementToGrid(r, Lines, textBlock);
            }
        }

        // Adds to 'HallMap' the Buttons that represent the seats of the hall.
        protected override void AddSeatsButtons()
        {
            for (int r = 1; r <= SeatsInLine / 3; r++) //Left stand
            {
                for (int c = 0; c < Lines; c++)
                {
                    Button button = CreateSeatButton(Lines - c, r);
                    AddElementToGrid(r, c, button); ;
                }
            }

            for (int r = 1; r <= SeatsInLine / 3; r++) // Right stand
            {
                for (int c = 2 + Lines + SeatsInLine / 3; c < GridColumns; c++)
                {
                    Button button = CreateSeatButton(c + Lines - GridColumns + 1, SeatsInLine - (r - 1));
                    AddElementToGrid(r, c, button);
                }
            }

            for (int r = 2 + SeatsInLine / 3; r < GridRows; r++) // Front stand
            {
                for (int c = Lines + 1; c < Lines + 1 + SeatsInLine / 3; c++)
                {
                    Button button = CreateSeatButton(r - 1 - SeatsInLine / 3, c - Lines + SeatsInLine / 3);
                    AddElementToGrid(r, c, button);
                }
            }
        }

        // Adds to 'HallMap' the element that represent the stage of the hall.
        protected override void AddStage()
        {
            Border stage = CreateStageBorder();
            AddElementToGrid(1, Lines + 1, stage, SeatsInLine / 3, SeatsInLine / 3);
        }
    }
}
