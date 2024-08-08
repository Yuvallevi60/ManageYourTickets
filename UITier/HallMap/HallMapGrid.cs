using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UITier.ViewModels;

namespace UITier.HallMap
{
    // Part of the "Template Method" design pattern
    //
    // An abstract class that define an algoritem to create a grid representing a "Hall Map".
    // The method 'CreateGrid()' is the actual template method of this algorithm that return the "hall map" grid.
    // 'CreateGrid()' calls to others methods that declared in the class and they act as steps of the algorithm.
    // In addition the class has properties and supporting methods that can be used by the "algorithm steps" methods.
    //
    // The "Hall Map" grid will included:
    // the hall's stage and seats, numbering of rows and seats, marking of taken seats, and the possibility to click on a seat to select him for purchase.

    internal abstract class HallMapGrid
    {
        public Grid HallMap { get; set; }

        public int Lines { get; set; }

        public int SeatsInLine { get; set; }

        public int GridRows { get; set; }

        public int GridColumns { get; set; }

        public bool[,] TakenSeats { get; set; } // the value at [i,j] is 'true' if seat j+1 in line i+1 is taken. 

        public RelayCommand ClickSeatCommand { get; set; } // The command that added to the free seats and should give the ability to choose this seat to buy.



        public HallMapGrid()
        {
            HallMap = new Grid();

            // "empty" values. will be set again in 'PropertiesInitialize()'
            TakenSeats = new bool[0, 0];
            ClickSeatCommand = new RelayCommand(_ => { });
        }


        // The method that is the actual template method of the algorithm to create Hall Map. 
        public Grid CreateGrid(int lines, int seatsInLine, bool[,] takenSeats, RelayCommand clickSeatCommand)
        {
            PropertiesInitialize(lines, seatsInLine, takenSeats, clickSeatCommand);

            SetGridRowsAndGridColumns();

            AddRowsAndColumns();

            AddLinesNumbers();

            AddSeatsButtons();

            AddStage();

            return HallMap;
        }



        // The methods that CreateGrid() calls
        #region "algorithm steps" methods

        // Initializes Properties that are Initializing the same way for any sub-class of HallMapGrid
        protected void PropertiesInitialize(int lines, int seatsInLine, bool[,] takenSeats, RelayCommand clickSeatCommand)
        {
            Lines = lines;
            SeatsInLine = seatsInLine;
            TakenSeats = takenSeats;
            ClickSeatCommand = clickSeatCommand;
        }

        // Sets the values of the properties 'GridRows' and 'GridColumns'.
        protected virtual void SetGridRowsAndGridColumns()
        {
            GridRows = Lines;
            GridColumns = SeatsInLine;
        }

        // Adds the rows and columns to 'HallMap' according to the values of the 'GridRows' and 'GridColumns'.
        protected void AddRowsAndColumns()
        {
            for (int i = 0; i < GridRows; i++)
            {
                RowDefinition rowDef = new();
                rowDef.Height = new GridLength(30);
                HallMap.RowDefinitions.Add(rowDef);
            }
            for (int c = 0; c < GridColumns; c++)
            {
                ColumnDefinition colDef = new();
                colDef.Width = new GridLength(30);
                HallMap.ColumnDefinitions.Add(colDef);
            }
        }

        // Adds to 'HallMap' the elements that represent the seats lines number.
        protected abstract void AddLinesNumbers();

        // Adds to 'HallMap' the Buttons that represent the seats of the hall.
        protected abstract void AddSeatsButtons();

        // Adds to 'HallMap' the element that represent the stage of the hall.
        protected abstract void AddStage();

        #endregion




        // supporting methods to create UIElement objects and add it to the grid
        #region supporting methods 

        // returns Button that represent the 'seatColumn'-th seat in the 'seatRow'-th line of the hall. 
        protected Button CreateSeatButton(int seatRow, int seatColumn)
        {
            Button button = new()
            {
                Width = 20,
                Height = 20,
                Margin = new Thickness(3),
                Padding = new Thickness(1),
                Content = $"{seatColumn}",
                Background = Brushes.LightBlue,
                Foreground = Brushes.Black,
            };

            if (TakenSeats[seatRow - 1, seatColumn - 1])// taken seat
            {
                button.IsEnabled = false;
            }
            else // free seat, add a command to the button
            {
                button.Command = ClickSeatCommand;
                button.CommandParameter = new Tuple<Button, int, int>(button, seatRow, seatColumn);
            }
            return button;
        }

        // returns TextBlock with the given 'text'.
        protected static TextBlock CreateLineNumberTextBlock(string text)
        {
            return new TextBlock()
            {
                Width = 25,
                Height = 25,
                Text = text
            };
        }

        // returns Border that represent the stage of the hall.
        protected static Border CreateStageBorder()
        {
            return new Border()
            {
                Background = Brushes.Gray,
                Child = new Viewbox
                {
                    MaxHeight = 100,
                    MaxWidth = 100,
                    Child = new TextBlock
                    {
                        Text = "Stage",
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    }
                }
            };
        }

        // Add 'element' to the Grid property "HallMap".
        protected void AddElementToGrid(int row, int column, UIElement element, int rowSpan = 1, int columnSpan = 1)
        {
            Grid.SetRow(element, row);
            Grid.SetColumn(element, column);
            Grid.SetRowSpan(element, rowSpan);
            Grid.SetColumnSpan(element, columnSpan);
            HallMap.Children.Add(element);
        }

        #endregion

    }
}
