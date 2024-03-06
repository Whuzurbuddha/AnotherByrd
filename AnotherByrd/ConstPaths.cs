using Microsoft.VisualBasic.FileIO;

namespace AnotherBird;

public static class ConstPaths
{
    public static readonly string? MainDirectory = $@"{SpecialDirectories.MyDocuments}\MailClient";
    public static readonly string? MailAccounts = $@"{SpecialDirectories.MyDocuments}\MailClient\mailaccounts";
}