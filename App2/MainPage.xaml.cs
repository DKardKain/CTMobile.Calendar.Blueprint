using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App2
{
    public class CalendarItems
    {
        public long Index { get; set; }
        public Grid Element { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private List<CalendarItems> items = new List<CalendarItems>();

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)

        {
            /*
            if (scrollViewer == null)

            {

                return;

            }

            ComboBox cb = sender as ComboBox;

            if (cb != null)

            {

                switch (cb.SelectedIndex)

                {

                    case 0:

                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.None;

                        break;

                    case 1:

                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.Optional;

                        break;

                    case 2:
                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.OptionalSingle;

                        break;

                    case 3:
                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.Mandatory;

                        break;

                    case 4:

                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.MandatorySingle;

                        break;

                    default:

                        scrollViewer.HorizontalSnapPointsType = SnapPointsType.None;

                        break;

                }

            }
            */
        }

        private void scenario2Clear_Click(object sender, RoutedEventArgs e)
        {
            scrollViewer.ChangeView(2650, null, null, true);
        }

        private void ShowItem(int index)
        {
            scrollViewer.ChangeView(2650, null, null, true);

            var currentScrollIndex = (long)(scrollViewer.HorizontalOffset / 500);

            //StackPanel.Children.Clear();

            for (int i = 0; i < 11; i++)
            {
                int indexToInsert = (index - 5) + i;
                var itemToInsert = items[indexToInsert];

                if (StackPanel.Children.Contains(itemToInsert.Element))
                {
                    StackPanel.Children.Remove(itemToInsert.Element);
                }
                else
                {
                    StackPanel.Children.RemoveAt(0);
                }

                StackPanel.Children.Add(itemToInsert.Element);                
            }         
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            Byte[] b = new Byte[3];        

            for (int i = 0; i < 201; i++)
            {
                rnd.NextBytes(b);
                Color color = Color.FromArgb(255, b[0], b[1], b[2]);

                Brush brush = new SolidColorBrush(color);

                Grid grid = new Grid() { Height = 500, Width = 500, Margin = new Thickness(5) };

                grid.Children.Add(new Rectangle() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch, Fill = brush });

                grid.Children.Add(new TextBlock() { Text = i.ToString(), FontSize=25 });

                grid.DataContext = i;

                items.Add(new CalendarItems()
                {
                    Element = grid,
                    Index = i
                });

                if (i > 100 & i <112 )
                {
                    StackPanel.Children.Add(grid);
                }

                StackPanelH.Children.Add(new TextBlock() { Height = 500, Width = 500, Margin = new Thickness(5), Text = i.ToString(), FontSize = 25 });
            }

            scrollViewerH.ChangeView(53000, null, null, true);

            scrollViewer.ViewChanged += MainScrollViewer_ViewChanged;
        }

        private void MainScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var offset = scrollViewer.HorizontalOffset;

            var index = (int)offset / 500;

            var changetoOffset = (int)((Grid)StackPanel.Children[index]).DataContext * 500 + offset;

            scrollViewerH.ChangeView(changetoOffset, null, null, true);
        }

        private void scrollViewer_DirectManipulationCompleted(object sender, object e)
        {
            var offset = scrollViewer.HorizontalOffset;

            var index = (int)offset / 500;

            ShowItem((int)((Grid)StackPanel.Children[index]).DataContext);
        }
    }
}
