namespace Auction.WebSite.BootstrapSupport
{
    public static class Alerts
    {
        public const string Success = "Успешно";
        public const string Attention = "Внимание";
        public const string Error = "Ошибка";
        public const string Information = "Информация";

        public static string[] All => new[] { Success, Attention, Information, Error };
    }
}