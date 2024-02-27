using System.Windows.Controls;

namespace AnotherByrd.Views.Pages;

public partial class FileView : Page
{
    public FileView()
    {
        InitializeComponent();
    }
    private Uri? _uri;
    public void OpenFile(string?  path)
    {
        if (path == null) return;
        _uri = new Uri(path);
        WebView.Source = _uri;
    }
}