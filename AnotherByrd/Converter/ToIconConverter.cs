using System.Globalization;
using System.Windows.Data;
using static FontAwesome.WPF.FontAwesomeIcon;
namespace AnotherByrd.Converter 
{
    public class ToIconConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var objectString = value?.ToString();
            var lastDot = objectString?.LastIndexOf('.');
            var fileEnding = lastDot < 0 ? "" : objectString?.Substring((int)(lastDot+1)!).ToLower();
            return fileEnding switch
            {
                "png" or "jpeg" or "jpg" or "bmp" or "svg" or "tif" => FilePictureOutline,
                "pdf" => FilePdfOutline,
                "word" or "doc" or "docx" => FileWordOutline,
                "xlsx" or "xlsm" or "xlsx" or "xls" => FileExcelOutline,
                "txt" or "odt" or "md"=> FileTextOutline,
                "zip" or "tar.gz" or "gzip" or "tar" or "7z" or "rar" or "jar" => FileZipOutline,
                "mp3" or "mid" or "wav" or "wave" or "ogg" or "flac" => FileAudioOutline,
                "avi" or "flv" or "mov" or "mp4" => FileVideoOutline,
                _ => false
            };
        }
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}