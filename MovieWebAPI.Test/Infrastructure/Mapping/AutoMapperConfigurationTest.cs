using MovieWebAPI.Infrastructure.Mapping;
using NUnit.Framework;

namespace MovieWebAPI.Test.Infrastructure.Mapping
{
    [TestFixture]
    public class AutoMapperConfigurationTest
    {
        [Test]
        public void AutoMapperConfiguration_Init()
        {
            AutoMapperConfiguration.Init();
        }
    }
}
