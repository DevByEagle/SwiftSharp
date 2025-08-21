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
            return new Text("Main View");
        }

        public override void ViewDidLoad()
        {
            Show(this, sender: this);
        }
    }

    internal class Program
    {
        public static void Main()
        {
            var mainVC = new MainViewController();

            mainVC.LoadViewIfNeeded();

            Console.WriteLine($"MainViewController IsViewLoaded: {mainVC.IsViewLoaded}");
        } 
    }
}