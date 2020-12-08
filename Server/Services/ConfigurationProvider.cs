using System.Linq;
using AuthServer.Server.Models;

namespace AuthServer.Server.Services
{
    class ConfigurationProvider
    {
        private readonly AuthDbContext _authDbContext;

        public ConfigurationProvider(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        public void Set(string key, string value)
        {
            SystemSetting? setting = _authDbContext.SystemSettings
                .SingleOrDefault(s => s.Name == key);

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    Name = key,
                };
                _authDbContext.Add(setting);
            }
            setting.Value = value;

            _authDbContext.SaveChangesAsync();
        }

        public bool TryGet(string key, out string value)
        {
            SystemSetting? setting = _authDbContext.SystemSettings
                .SingleOrDefault(s => s.Name == key);


            if (setting == null)
            {
                value = "";
                return false;
            }

            value = setting.Value;
            return true;
        }
    }
}