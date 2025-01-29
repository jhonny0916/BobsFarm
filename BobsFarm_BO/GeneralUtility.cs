using Microsoft.Extensions.Configuration;

namespace BobsFarm_BO
{
    public static class GeneralUtility
    {
        public static IConfigurationSection GetConfigurationSection(string sectionName)
        {
            var configurationBuilder = new ConfigurationBuilder();
            //Capture Environment Variable
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string path = string.Empty;
            //If id Development environment
            if (env != null && env.Equals("Development"))
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");
            }
            //Id is production environment
            else
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            }

            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();

            return root.GetSection(sectionName);
        }

        public static void GenerateBusinessException(string code, string message)
        {
            var error = new BOError { Code = code, Message = message };
            throw new BLException(error);
        }
    }
}
