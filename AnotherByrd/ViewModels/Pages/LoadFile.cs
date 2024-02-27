using Microsoft.Win32;

namespace AnotherBird.ViewModels.Pages;

public static class LoadFile
{
    public static string? OpenDirectory()
    {
        var openFileDialog = new OpenFileDialog();
        if (openFileDialog.ShowDialog() != true) return null;
        var file = openFileDialog.FileName;
        return file;
    }
}