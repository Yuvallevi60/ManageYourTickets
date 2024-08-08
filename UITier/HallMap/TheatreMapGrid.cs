using System.Windows.Controls;

namespace UITier.HallMap
{
    // Part of the Template Method design pattern
    //
    // Defines the "algorithm steps" methods to create a Hall Map of Theatre hall.
    // Theatre hall is a hall with one stand in front of the stage
    internal class TheatreMapGrid : HallMapGrid
    {

        // Sets the values of the properties 'GridRows' and 'GridColumns'.
        protected override void SetGridRowsAndGridColumns()
        {
            GridRows = Lines + 2;
            GridColumns = SeatsInLine + 1;
        }

        // Adds to 'HallMap' the elements that represent the seats lines number.
        protected override void AddLinesNumbers()
        {
            for (int r = 2; r < GridRows; r++)
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{r - 1}");
                AddElementToGrid(r, 0, textBlock);
            }
        }

        // Adds to 'HallMap' the Buttons that represent the seats of the hall.
        protected override void AddSeatsButtons()
        {
            for (int c = 1; c < GridColumns; c++)
            {
                for (int r = 2; r < GridRows; r++)
                {
                    Button button = CreateSeatButton(r - 1, c);
                    AddElementToGrid(r, c, button);
                }
            }
        }

        // Adds to 'HallMap' the element that represent the stage of the hall.
        protected override void AddStage()
        {
            Border stage = CreateStageBorder();
            AddElementToGrid(0, 1, stage, 1, SeatsInLine);
        }
    }
}
