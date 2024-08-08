using System.Windows.Controls;

namespace UITier.HallMap
{
    // Part of the "Template Method" design pattern.
    //
    // Defines the "algorithm steps" methods to create a Hall Map of Traverse hall.
    // Traverse hall is a hall with two stands, one in each side of the stage.
    internal class TraverseMapGrid : HallMapGrid
    {

        // Sets the values of the properties 'GridRows' and 'GridColumns'.
        protected override void SetGridRowsAndGridColumns()
        {
            GridRows = Lines * 2 + 5;
            GridColumns = SeatsInLine / 2 + 1;
        }

        // Adds to 'HallMap' the elements that represent the seats lines number.
        protected override void AddLinesNumbers()
        {
            for (int r = 0; r < Lines; r++) // Upper stand (In the map's perspective)
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{Lines - r}");
                AddElementToGrid(r, 0, textBlock);
            }

            for (int r = Lines + 5; r < GridRows; r++) // Lower stand (In the map's perspective)
            {
                TextBlock textBlock = CreateLineNumberTextBlock($"{r - (Lines + 5) + 1}");
                AddElementToGrid(r, 0, textBlock);
            }
        }

        // Adds to 'HallMap' the Buttons that represent the seats of the hall.
        protected override void AddSeatsButtons()
        {
            for (int r = 0; r < Lines; r++) // Upper stand (In the map's perspective)
            {
                for (int c = 1; c < GridColumns; c++)
                {
                    Button button = CreateSeatButton(Lines - r, c);
                    AddElementToGrid(r, c, button);
                }
            }

            for (int r = Lines + 5; r < GridRows; r++) // Lower stand (In the map's perspective)
            {
                for (int c = 1; c < GridColumns; c++)
                {
                    Button button = CreateSeatButton(r - (Lines + 5) + 1, c + SeatsInLine / 2);
                    AddElementToGrid(r, c, button);
                }
            }
        }

        // Adds to 'HallMap' the element that represent the stage of the hall.
        protected override void AddStage()
        {
            Border stage = CreateStageBorder();
            AddElementToGrid(Lines + 1, 1, stage, 3, SeatsInLine / 2);
        }
    }
}
