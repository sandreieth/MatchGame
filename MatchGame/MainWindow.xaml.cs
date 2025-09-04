using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        int tenthsOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            //List<string> animalEmoji = new List<string>()
            //{
            //    "A", "A",
            //    "B", "B",
            //    "C", "C",
            //    "D", "D",
            //    "E", "E",
            //    "F", "F",
            //    "G", "G",
            //    "H", "H",
            //};

            //Random random = new Random();

            //foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            //{
            //    if (textBlock.Name == "timeTextBlock")
            //    {
            //        continue;
            //    }
            //    int index = random.Next(animalEmoji.Count);
            //    string nextEmoji = animalEmoji[index];
            //    textBlock.Text = nextEmoji;
            //    animalEmoji.RemoveAt(index);
            //}

            List<ImageSource> images = new List<ImageSource>()
            {
                new BitmapImage(new Uri("../../Assets/c-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/cplusplus-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/c-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/cplusplus-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/csharp-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/go-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/csharp-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/go-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/python-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/java-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/python-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/java-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/ruby-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/rust-original.png", UriKind.Relative)),
                new BitmapImage(new Uri("../../Assets/ruby-original.png", UriKind.Relative)), new BitmapImage(new Uri("../../Assets/rust-original.png", UriKind.Relative)),
            };

            Random random = new Random();

            foreach (Image image in mainGrid.Children.OfType<Image>())
            {
                int index = random.Next(images.Count);
                ImageSource nextImage = images[index];
                image.Source = images[index];
                images.RemoveAt(index);
            }
        }

        //TextBlock lastTextBlockClicked;
        Image lastImageClicked;
        bool findingMatch = false;
        bool invoked = false;

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!invoked)
            {
                timer.Start();
                tenthsOfSecondsElapsed = 0;
                matchesFound = 0;
            }
            invoked = true;

            Image image = sender as Image;

            if (findingMatch == false)
            {
                image.Visibility = Visibility.Hidden;
                lastImageClicked = image;
                findingMatch = true;
            }
            else if ((image.Source as BitmapImage).UriSource == (lastImageClicked.Source as BitmapImage).UriSource)
            {
                image.Visibility = Visibility.Hidden;
                findingMatch = false;
                matchesFound++;
            }
            else
            {
                lastImageClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        //private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if (!invoked)
        //    {
        //        timer.Start();
        //        tenthsOfSecondsElapsed = 0;
        //        matchesFound = 0;
        //    }
        //    invoked = true;

        //    TextBlock textBlock = sender as TextBlock;

        //    if (findingMatch == false)
        //    {
        //        textBlock.Visibility = Visibility.Hidden;
        //        lastTextBlockClicked = textBlock;
        //        findingMatch = true;
        //    }
        //    else if (textBlock.Text == lastTextBlockClicked.Text)
        //    {
        //        textBlock.Visibility = Visibility.Hidden;
        //        findingMatch = false;
        //        matchesFound++;
        //    }
        //    else
        //    {
        //        lastTextBlockClicked.Visibility = Visibility.Visible;
        //        findingMatch = false;
        //    }
        //}

        private void timeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound != 8)
            {
                return;
            }

            matchesFound = 0;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            TextBlock timeTextBlock = mainGrid.FindName("timeTextBlock") as TextBlock;
            timeTextBlock.Text = "0.0s";

            foreach (Image image in mainGrid.Children.OfType<Image>())
            {
                image.Visibility = Visibility.Visible;
            }
            SetUpGame();

            //lastTextBlockClicked = null;
            lastImageClicked = null;
            invoked = false;
        }
    }
}
