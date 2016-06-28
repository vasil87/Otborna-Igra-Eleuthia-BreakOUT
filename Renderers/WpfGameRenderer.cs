

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
    public class WpfGameRenderer : IRenderer
    {
        private Canvas canvas;


        public int ScreenWidth { get { return (int)(this.canvas.Parent as MainWindow).Width; } }


        public int ScreenHeight { get { return (int)(this.canvas.Parent as MainWindow).Height; } }


        public event EventHandler<KeyDownEventArgs> presingkey;

        public WpfGameRenderer(Canvas gameCanvas)
        {

            this.canvas = gameCanvas;
            (this.canvas.Parent as MainWindow).KeyDown += (sender, args) =>
             {


                 var key = args.Key;

                 if (key == Key.Left)
                 {   
                     
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

        public void Clear()
        {
            this.canvas.Children.Clear();
        }

        public void Draw(params GameObjects[] drawObject)

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
                    DrawBall(drawing);
                }
            }

        }

        private void DrawBall(GameObjects drawing)
        {
            Image ball = new Image();

            BitmapImage brickFacetSource = new BitmapImage();
            brickFacetSource.BeginInit();
            brickFacetSource.UriSource = new Uri(@"C:\Users\vasil\Desktop\146716091038845.gif");
            brickFacetSource.EndInit();

            ball.Source = brickFacetSource;
            ball.Height = drawing.Bounds.Height;
            ball.Width = drawing.Bounds.Width;


            Canvas.SetLeft(ball, drawing.Position.Left);
            Canvas.SetTop(ball, drawing.Position.Top);
            this.canvas.Children.Add(ball);



        }

        private void DrawPad(GameObjects drawing)
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

        private void DrawBrick(GameObjects drawing)
        {

            var brick = new Rectangle()
            {
                Width = drawing.Bounds.Width,
                Height = drawing.Bounds.Height,
                Fill = Brushes.Brown,
                Stroke = Brushes.AntiqueWhite,
                StrokeThickness = 3
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
    }
}
