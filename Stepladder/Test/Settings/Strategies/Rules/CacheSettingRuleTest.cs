using App.Settings;
using App.Settings.Connections;
using App.Settings.Strategies;
using App.Settings.Strategies.Rules;
using App.Settings.Strategies.Types;

namespace Test.Settings.Strategies.Rules
{
    public class CacheSettingRuleTest
    {
        [Fact]
        public void WhenCacheHasEmptyOrNullId_ShouldReturnError()
        {
            // arrange
            var setting = new CacheSetting();
            var rule = new CacheSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("Cache.Id is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenCacheHasNoneType_ShouldReturnError()
        {
            // arrange
            var setting = new CacheSetting { Type = CacheType.None };
            var rule = new CacheSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("Cache.Type is required");
            Assert.True(contains);
        }

        [Fact]
        public void WhenConfigureCacheRedisWioutConfigureConnectionRedis_ShouldReturnError()
        {
            // arrange
            var appSetting = new ApplicationSetting() { Connections = new ConnectionSetting() { Redis = null } };
            var setting = new CacheSetting { Type = CacheType.Redis };
            var rule = new CacheSettingRule();

            // act
            var result = rule.Do(setting);

            // assert
            var contains = result.Errors.Contains("Configure connection redis before use cache redis");
            Assert.True(contains);
        }
    }
}
