using System.Globalization;
using System.Windows.Data;

namespace AnotherBird.Converter;

public class MailSenderConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((bool)value?.ToString()!.Contains('<'))
        {
            var convertedMail = value?.ToString()!.Split("<")[1].Split(">")[0];
            return convertedMail;
        }
        else
        {
            return value;
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}