using Microsoft.VisualBasic.FileIO;

namespace AnotherBird;

public static class ConstPaths
{
    public static readonly string? CachePath = $@"{SpecialDirectories.MyDocuments}\MailClient\\Cache";
    public static readonly string? LoginCachePath = $@"{CachePath}\LoginCache.json";
    public static readonly string? MainDirectory = $@"{SpecialDirectories.MyDocuments}\MailClient";
    public static readonly string? MailAccounts = $@"{SpecialDirectories.MyDocuments}\MailClient\mailaccounts";
}