namespace OtbornaIgra
{
    using GameEngines;
    using Interfaces;
    using Renderers;
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.InitializeComponent();
            IRenderer renderer = new WpfGameRenderer(this.GameCanvas);
            
            this.Engine = new GameEngine(renderer);
            this.Engine.InitGame();
            this.Engine.StartGame();
           
        }

       
        public GameEngine Engine { get; private set; }
       
    }
}
