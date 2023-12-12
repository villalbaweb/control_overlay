namespace Control.Overlay.Maui.Overlays;

public class LoadingOverlay : WindowOverlay
{
    private readonly IWindowOverlayElement _loadingElementOverlay;

    public LoadingOverlay(IWindow window)
        : base(window)
    {
        _loadingElementOverlay = new LoadingElementOverlay(this);
        AddWindowElement(_loadingElementOverlay);
    }
}
