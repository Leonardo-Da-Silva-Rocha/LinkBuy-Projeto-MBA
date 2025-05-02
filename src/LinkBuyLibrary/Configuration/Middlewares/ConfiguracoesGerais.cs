using System.Globalization;

namespace LinkBuyLibrary.Configuration.Middlewares
{
    public static class ConfiguracoesGerais
    {
        public static void AddCultura()
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
}
