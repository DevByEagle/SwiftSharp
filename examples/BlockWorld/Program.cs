using System;
using SwiftSharp;
using SwiftSharp.Foundation;
using SwiftSharp.UI;

namespace BlockWorld
{
    class BlockView : View
    {
    }

    class MainViewController : UIViewController
    {
        protected override View CreateView()
        {
            var text = new Text("Hello, World!");
            return text;
        }

        public override void ViewDidLoad()
        {
            Show(this, sender: this);
        }
    }

    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            var mainVC = new MainViewController();

            mainVC.LoadViewIfNeeded();

            Console.WriteLine($"MainViewController IsViewLoaded: {mainVC.IsViewLoaded}");
        } 
    }
}