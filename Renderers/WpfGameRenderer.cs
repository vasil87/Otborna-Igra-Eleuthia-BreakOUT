﻿namespace OtbornaIgra.Renderers
{
    using Interfaces;
    using System;
    using GameObjects;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Input;
    using Misc;
    using Global;
    using System.Windows;

    public delegate void KeyDownEventHander(object obj, KeyDownEventArgs args);

    public class WpfGameRenderer : IRenderer
    {


        private Canvas canvas;


        public int ScreenWidth { get { return (int)(this.canvas.Parent as MainWindow).Width; } }


        public int ScreenHeight { get { return (int)(this.canvas.Parent as MainWindow).Height; } }


        public event KeyDownEventHander presingkey;

        public WpfGameRenderer(Canvas gameCanvas)  //constructor i proverka za natisnat buton
        {
            string pathBackground = System.IO.Path.GetFullPath(@"..\..\Images\background36.jpg");
            this.canvas = gameCanvas;
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(pathBackground));
            myBrush.ImageSource = image.Source;
            this.canvas.Background = myBrush;

            //main window slusha za keydown i ako ima vika delegata
            (this.canvas.Parent as MainWindow).KeyDown += (sender, args) =>
             {
                 var key = args.Key;

                 if (key == Key.Left)
                 {     //reizvash eventa na renderer (presingkey) sys this i keydown comand moveleft
                     this.presingkey(this, new KeyDownEventArgs(GameComand.MoveLeft));
                 }

                 else if (key == Key.Right)
                 {
                     this.presingkey(this, new KeyDownEventArgs(GameComand.MoveRight));
                 }

                 else if (key == Key.Space)
                 {

                     this.presingkey(this, new KeyDownEventArgs(GameComand.Pause));
                 }
                 else
                 {
                     this.presingkey(this, new KeyDownEventArgs(GameComand.other));
                 }

             };
        }

        public void Clear()                          //iztriva obektite vurhu canvasa
        {
            this.canvas.Children.Clear();
        }

        public void Draw(params IGameObject[] drawObject)  //risuva obektite vurhu canvasa v zavisimost ot vida im 

        {
            foreach (var drawing in drawObject)
            {
                if (drawing is PadGameObject)
                {
                    IRebouncable drawingImovable = drawing as IRebouncable;
                    DrawPad(drawingImovable);
                }
                else if (drawing is BricksGameObject)
                {
                    DrawBrick(drawing);

                }
                else if (drawing is BallGameObject)
                {
                    IMovable drawingImovable = drawing as IMovable;
                    DrawBall(drawingImovable);
                }
                else if (drawing is ErorTextGameObject)
                {

                    DrawErrorMsg(drawing as ErorTextGameObject);
                }
                else if (drawing is ITextGameObject)
                {
                    DrawText(drawing as TextGameObject);

                }
            }

        }

        private void DrawBall(IMovable drawing)
        {

            //inicializaciq na bitmap 
            BitmapImage ballFacetSource = new BitmapImage();
            ballFacetSource.BeginInit();                //putq do image v papka  images
            string path = System.IO.Path.GetFullPath(@"..\..\Images\Ball.png");
            ballFacetSource.UriSource = new Uri(path);
            ballFacetSource.EndInit();

            //pravim nov image koito vzima source bitmapimage
            Image ball = new Image();
            ball.Source = ballFacetSource;
            ball.Height = drawing.Bounds.Height;
            ball.Width = drawing.Bounds.Width;

            //static method za da setvane poziciq na ball sprqmo canvasa
            Canvas.SetLeft(ball, drawing.Position.Left);
            Canvas.SetTop(ball, drawing.Position.Top);
            //dobavqme v canvasa ball obekta kato children na cavasa 
            this.canvas.Children.Add(ball);
        }

        private void DrawPad(IRebouncable drawing)
        {
            //inicializaciq na bitmap 
            BitmapImage ballFacetSource = new BitmapImage();
            ballFacetSource.BeginInit();                //putq do image v papka  images
            string path = System.IO.Path.GetFullPath(@"..\..\Images\neon_line.png");
            ballFacetSource.UriSource = new Uri(path);
            ballFacetSource.EndInit();

            //pravim nov image koito vzima source bitmapimage
            Image pad = new Image();
            pad.Source = ballFacetSource;
            pad.Height = drawing.Bounds.Height;
            pad.Width = drawing.Bounds.Width;

            //static method za da setvane poziciq na ball sprqmo canvasa
            Canvas.SetLeft(pad, drawing.Position.Left);
            Canvas.SetTop(pad, drawing.Position.Top);
            //dobavqme v canvasa ball obekta kato children na cavasa 
            this.canvas.Children.Add(pad);



            //BitmapImage ballFacetSource = new BitmapImage();
            //ballFacetSource.BeginInit();                //putq do image v papka images
            //string path = System.IO.Path.GetFullPath(@"..\..\Images\Ball.png");
            //ballFacetSource.UriSource = new Uri(path);
            //ballFacetSource.EndInit();

            //var pad = new Ellipse()
            //{
            //    Width = drawing.Bounds.Width,
            //    Height = drawing.Bounds.Height,
            //    Fill = Brushes.LimeGreen
            //};

            //Canvas.SetLeft(pad, drawing.Position.Left);
            //Canvas.SetTop(pad, drawing.Position.Top);
            //this.canvas.Children.Add(pad);
        }

        private void DrawText(TextGameObject drawing)
        {
            var text = new TextBlock()
            {
                Width = drawing.Bounds.Width,
                Height = drawing.Bounds.Height,
                Foreground = Brushes.WhiteSmoke,
                Text = drawing.Text,
                FontSize = 15,

            };

            Canvas.SetLeft(text, drawing.Position.Left);
            Canvas.SetRight(text, drawing.Position.Top);
            this.canvas.Children.Add(text);
        }

        private void DrawErrorMsg(ErorTextGameObject drawing)
        {
            var text = new TextBlock()
            {  
                Width = drawing.Bounds.Width,
                Height = drawing.Bounds.Height,
                Foreground = Brushes.WhiteSmoke,
                Text = drawing.Text,
                FontSize = 25,

            };

            Canvas.SetLeft(text, drawing.Position.Left);
            Canvas.SetRight(text, drawing.Position.Top);
            this.canvas.Children.Add(text);
            
        }

        /*
         BitmapImage padImage = new BitmapImage();
            padImage.BeginInit();                //putq do image v papka images
            string path = System.IO.Path.GetFullPath(@"..\..\Images\pad.png");
            padImage.UriSource = new Uri(path);
            padImage.EndInit();

            //pravim nov image koito vzima source bitmapimage
            Image pad = new Image();
            pad.Source = padImage;
            pad.Height = drawing.Bounds.Height;
            pad.Width = drawing.Bounds.Width;

            //static method za da setvane poziciq na ball sprqmo canvasa
            Canvas.SetLeft(pad, drawing.Position.Left);
            Canvas.SetTop(pad, drawing.Position.Top);
            //dobavqme v canvasa ball obekta kato children na cavasa 
            this.canvas.Children.Add(pad);
             */



        private void DrawBrick(IGameObject drawing)
        {
            //SolidColorBrush brush = GiveMeColor(drawing); //vrushta random color
            //var brick = new Rectangle()
            //{
            //    Width = drawing.Bounds.Width,
            //    Height = drawing.Bounds.Height,
            //    Fill = brush,
            //};

            //Canvas.SetLeft(brick, drawing.Position.Left);
            //Canvas.SetTop(brick, drawing.Position.Top);
            //this.canvas.Children.Add(brick);





            Image brick = new Image();

            BitmapImage brickFacetSource = new BitmapImage();
            brickFacetSource.BeginInit();
            string path = System.IO.Path.GetFullPath(@"..\..\Images\brick.png");
            brickFacetSource.UriSource = new Uri(path);
            brickFacetSource.EndInit();

            brick.Source = brickFacetSource;
            brick.Height = drawing.Bounds.Height;
            brick.Width = drawing.Bounds.Width;


            Canvas.SetLeft(brick, drawing.Position.Left);
            Canvas.SetTop(brick, drawing.Position.Top);
            this.canvas.Children.Add(brick);
        }


        private static SolidColorBrush GiveMeColor(IGameObject drawing)
        {
            var brush = Brushes.LawnGreen;
            Random cvqt = new Random(drawing.GetHashCode());
            int randomChislo = cvqt.Next(1, 4);
            if (randomChislo == 1)
            { brush = Brushes.Indigo; }
            else if (randomChislo == 2)
            { brush = Brushes.White; }

            return brush;
        }

        //proverka za dali pada e v granici kato natiskame lqvo i dqsno
        public bool isInBounds(Position position)
        {
            if (position.Left <= -10 || position.Left >= ScreenWidth - GlobalConstants.padWidth - 5
                || position.Top <= 5 || position.Top >= ScreenHeight)
            {
                return false;
            }

            else
            {
                return true;
            }

        }

        public void ShowStartGameScreen()
        {
            string pathBackground = System.IO.Path.GetFullPath(@"..\..\Images\vav.png");
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(pathBackground));
            myBrush.ImageSource = image.Source;

            var window = new Window
            {
                Width = 1020,
                Height = 600,
                Background = myBrush,
            };
            window.Show();
            System.Threading.Thread.Sleep(3000);
            window.Close();
        }
        public void ShowEndGameScreen()
        {
            var parent = this.canvas.Parent;
            while (!(parent is Window))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            string pathBackground = System.IO.Path.GetFullPath(@"..\..\Images\endscreen.jpg");
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(pathBackground));
            myBrush.ImageSource = image.Source;

            StackPanel panel = new StackPanel
            {
            };

            var button = new Button
            {
                FontSize = 20,
                Content = "PLAY AGAIN",
                Width = 250,
                Height = 50,
                Background = Brushes.Transparent,
                Foreground = Brushes.LightSkyBlue,
            };
            panel.Children.Add(button);
            var buttonEnd = new Button
            {
                FontSize = 20,
                Content = "EXIT GAME",
                Width = 250,
                Height = 50,
                Background = Brushes.Transparent,
                Foreground = Brushes.LightSkyBlue,

            };
            panel.Children.Add(buttonEnd);



            var window = new Window
            {
                Content = panel,
                Width = 1020,
                Height = 600,
                Background = myBrush,
            };

            button.Click += (snd, ev) =>
              {
                  new MainWindow().Show();
                  window.Close();
              };
            buttonEnd.Click += (snd, ev) =>
            {
                Environment.Exit(0);
            };
            window.Show();
            (parent as Window).Close();

        }

        public void ShowWinGameScreen(string highScore)
        {
            var parent = this.canvas.Parent;
            while (!(parent is Window))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            string pathBackground = System.IO.Path.GetFullPath(@"..\..\Images\endscreen.jpg");
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(pathBackground));
            myBrush.ImageSource = image.Source;

            StackPanel panel = new StackPanel
            {
            };
            var text = new TextBlock
            {
                FontSize = 25,
                Width = 250,
                Height = 100,
                Background = Brushes.Transparent,
                Foreground = Brushes.LightSkyBlue,
                Text = string.Format(" GOOD GAME YOUR {1}      {0}", highScore, Environment.NewLine)
            };
            panel.Children.Add(text);
            var button = new Button
            {
                FontSize = 20,
                Content = "PLAY AGAIN",
                Width = 250,
                Height = 50,
                Background = Brushes.Transparent,
                Foreground = Brushes.LightSkyBlue,
            };
            panel.Children.Add(button);
            var buttonEnd = new Button
            {
                FontSize = 20,
                Content = "EXIT GAME",
                Width = 250,
                Height = 50,
                Background = Brushes.Transparent,
                Foreground = Brushes.LightSkyBlue,

            };
            panel.Children.Add(buttonEnd);



            var window = new Window
            {
                Content = panel,
                Width = 1020,
                Height = 600,
                Background = myBrush,
            };

            button.Click += (snd, ev) =>
            {
                new MainWindow().Show();
                window.Close();
            };
            buttonEnd.Click += (snd, ev) =>
            {
                Environment.Exit(0);
            };
            window.Show();
            (parent as Window).Close();

        }

    }
}

