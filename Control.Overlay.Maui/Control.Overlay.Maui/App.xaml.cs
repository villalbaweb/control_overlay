using Control.Overlay.Maui.Overlays;
using Control.Overlay.Maui.Views;

namespace Control.Overlay.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
        }

        // You do not need to create a new window to use the overlays,
        // VisualDiagnostics is created on all Windows by default.
        // You can also add them to the generic Window by using "AddOverlay"
        protected override Window CreateWindow(IActivationState activationState) => new TestWindow() { Page = new OverlaySamplePage() };
    }

    public class TestWindow : Window, IVisualTreeElement
    {
        LoadingOverlay loadingOverlay;

        public TestWindow()
        {
            this.loadingOverlay = new LoadingOverlay(this) { IsVisible = false };
        }

        public void ShowLoadingOverlay()
        {
            // For the Loading Overlay, we need to disable touch events so users
            // can't interact with the underlying UI while something is loading.
            this.loadingOverlay.IsVisible = true;
            this.loadingOverlay.DisableUITouchEventPassthrough = true;
        }

        public void HideLoadingOverlay()
        {
            this.loadingOverlay.IsVisible = false;
            this.loadingOverlay.DisableUITouchEventPassthrough = false;
        }

        private void VisualDiagnosticsOverlay_Tapped(object sender, WindowOverlayTappedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.Point);

            // if "DisableUITouchEventPassthrough" is enabled, WindowOverlayTappedEventArgs
            // will contain the elements contained within the point that was touched.
            // Otherwise, it will be empty.

            if (e.VisualTreeElements.Any())
            {
                this.Dispatcher.Dispatch(() => {
                    VisualDiagnosticsOverlay.RemoveAdorners();
                    VisualDiagnosticsOverlay.AddAdorner(e.VisualTreeElements.First(), false);
                });
            }
        }

        protected override void OnCreated()
        {
            // Generally, if you want to add overlays to a Window when it's created,
            // It should go here. This assures that the Window is created, so that
            // the underlying views will be created correctly as well.
            VisualDiagnosticsOverlay.Tapped += VisualDiagnosticsOverlay_Tapped;
            this.AddOverlay(this.loadingOverlay);
            base.OnCreated();
        }

        public IReadOnlyList<IVisualTreeElement> GetVisualChildren()
        {
            var elements = new List<IVisualTreeElement>();
            if (this.Page != null && this.Page is IVisualTreeElement element)
                elements.AddRange(element.GetVisualChildren());

            var overlays = this.Overlays.Where(n => n is IVisualTreeElement).Cast<IVisualTreeElement>();
            elements.AddRange(overlays);

            return elements;
        }

        public IVisualTreeElement? GetVisualParent() => App.Current;
    }

}
