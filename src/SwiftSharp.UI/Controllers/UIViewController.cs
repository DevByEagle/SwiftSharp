using System;
using System.Linq;
using System.Threading;

namespace SwiftSharp.UI
{
    public abstract partial class UIViewController
    {
        #region Fields & Properties

        public Array<View> Children { get; } = new();

        public View? View { get; private set; }

        public bool IsViewLoaded => View != null;

        #endregion

        #region Constructors

        #endregion

        #region Abstract Methods

        protected abstract View CreateView();

        #endregion

        #region Lifecycle Methods

        public void LoadView()
        {
            View = CreateView();
            RenderView();
        }

        public void LoadViewIfNeeded()
        {
            if (!IsViewLoaded)
            {
                LoadView();
                ViewDidLoad();
            }
        }

        public virtual void ViewDidLoad() { }

        protected virtual void ViewWillAppear() { }

        protected virtual void ViewDidAppear() { }

        #endregion

        #region Presentation Methods

        public void Show(UIViewController vc, object? sender = null)
        {
            RunOnMainThread(() =>
            {
                vc.LoadViewIfNeeded();
                PresentViewController(vc, sender);
            });
        }

        #endregion

        #region Child Controller Management

        public void AddChild(UIViewController childController)
        {
            if (!Children.Contains(childController.View))
            {
                childController.ViewWillAppear();

                Children.Append(childController.View!);

                childController.ViewDidAppear();
            }
        }

        #endregion

        #region Utilities

        private void PresentViewController(UIViewController vc, object? sender)
        {
            if (!vc.IsViewLoaded)
                throw new InvalidOperationException("View must be loaded before presenting.");

            AddChild(vc);
        }

        private void RenderView()
        {
            foreach (var child in Children)
            {
                // TODO: Render each subview on screen
                child.Render(Rendering.GraphicsContext.Current);
            }
        }

        private void RunOnMainThread(Action action)
        {
            var context = SynchronizationContext.Current;
            if (context != null)
            {
                context.Post(_ => action(), null);
            }
            else
            {
                action();
            }
        }

        #endregion
    }
}