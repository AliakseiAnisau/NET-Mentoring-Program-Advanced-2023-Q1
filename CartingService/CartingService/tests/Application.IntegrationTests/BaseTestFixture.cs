using NUnit.Framework;

using static CartingService.Application.IntegrationTests.Testing;

namespace CartingService.Application.IntegrationTests;
[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
