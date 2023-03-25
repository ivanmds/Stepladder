using App.Settings;
using App.Settings.Connections;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace App.Extensions
{
    public static class LoadConnectionsExtension
    {
        public static void  AddConnections(this WebApplicationBuilder builder)
        {
            var appConfig = ApplicationSetting.Current;
            if(appConfig.Connections != null) 
            {
                if(appConfig.Connections.Redis != null)
                {
                    RedisConnection(builder, appConfig.Connections.Redis);
                }
            }
        }


        private static void RedisConnection(WebApplicationBuilder builder, RedisSetting redisSetting)
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisSetting.ConnectionString);
            
            if (multiplexer.IsConnected == false)
                throw new Exception($"Redis error when try connection in {redisSetting.ConnectionString}");

            var database = multiplexer.GetDatabase();
            
            builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            builder.Services.AddSingleton(database);
        }
    }
}
