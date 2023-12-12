namespace Control.Overlay.Maui.Views;

public partial class OverlaySamplePage : ContentPage
{
	public OverlaySamplePage()
	{
		InitializeComponent();
	}

    private async void OnLoadingOverlay(object sender, EventArgs e)
    {
        ((TestWindow)this.GetParentWindow()).ShowLoadingOverlay();
        await Task.Delay(4000);
        ((TestWindow)this.GetParentWindow()).HideLoadingOverlay();
    }
}