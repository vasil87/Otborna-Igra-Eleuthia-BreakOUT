namespace OtbornaIgra.Renderers
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GameObjects;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Input;
    using Misc;
    using Global;
    using System.Drawing;
    public class WpfGameRenderer : IRenderer
    {
        

        private Canvas canvas;


        public int ScreenWidth { get { return (int)(this.canvas.Parent as MainWindow).Width; } }


        public int ScreenHeight { get { return (int)(this.canvas.Parent as MainWindow).Height; } }


        public event EventHandler<KeyDownEventArgs> presingkey;

        public WpfGameRenderer(Canvas gameCanvas)  //constructor i proverka za natisnat buton
        {
            this.canvas = gameCanvas;
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

                     this.presingkey(this, new KeyDownEventArgs(GameComand.Fire));
                 }

             };
        }

        public void Clear()                          //iztriva obektite vurhu canvasa
        {
            this.canvas.Children.Clear();
        }

        public void Draw(params IGameObject[] drawObject)  //risuva obektite vurhu canvasa v zavisimost ot                                                     vida im 

        {
            foreach (var drawing in drawObject)
            {
                if (drawing is PadGameObject)
                {
                    DrawPad(drawing);
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

        private void DrawPad(IGameObject drawing)
        {
            var pad = new Ellipse()
            {
                Width = drawing.Bounds.Width,
                Height = drawing.Bounds.Height,
                Fill = Brushes.Yellow
            };

            Canvas.SetLeft(pad, drawing.Position.Left);
            Canvas.SetTop(pad, drawing.Position.Top);
            this.canvas.Children.Add(pad);
        }

        private void DrawBrick(IGameObject drawing)
        {
            SolidColorBrush brush = GiveMeColor(drawing); //vrushta random color
            var brick = new Rectangle()
            {
                Width = drawing.Bounds.Width,
                Height = drawing.Bounds.Height,
                Fill = brush,
            };
            
            Canvas.SetLeft(brick, drawing.Position.Left);
            Canvas.SetTop(brick, drawing.Position.Top);
            this.canvas.Children.Add(brick);
            




            //Image brick = new Image();

            //BitmapImage brickFacetSource = new BitmapImage();
            //brickFacetSource.BeginInit();
            //brickFacetSource.UriSource = new Uri(@"C:\Users\vasil\Desktop\brick.png");
            //brickFacetSource.EndInit();

            //brick.Source = brickFacetSource;
            //brick.Height = drawing.Bounds.Height;
            //brick.Width = drawing.Bounds.Width;


            //Canvas.SetLeft(brick, drawing.Position.Left);
            //Canvas.SetTop(brick, drawing.Position.Top);
            //this.canvas.Children.Add(brick);
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

        public bool isInBounds(Position position)
        {
            if (position.Left <= 0 || position.Left>=ScreenWidth - GlobalConstants.padWidth-5 
                || position.Top <= 5 || position.Top >= ScreenHeight)
            {
                return false;
            }

            else
            {
                return true;
            }

        }
    }
}
