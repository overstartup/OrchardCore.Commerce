using Atata;
using Lombiq.Tests.UI.Attributes;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace OrchardCore.Commerce.Tests.UI.Tests.PersistenceRegionSettingsTests;

public class PersistenceRegionSettingsTests : UITestBase
{
    public PersistenceRegionSettingsTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory, Chrome]
    public Task RegionSettingsShouldPersist(Browser browser) =>
        ExecuteTestAfterSetupAsync(
            async context =>
            {
                await context.SignInDirectlyAsync();
                await context.ExecuteRecipeDirectlyAsync("OrchardCore.Commerce.Samples.RegionSettings");

                await context.CreateNewContentItemAsync("Order");

                context
                    .GetAll(By.XPath("id('OrderPart_BillingAddress_Address_Region')/option"))
                    .Select(selectListOption => selectListOption.Text)
                    .ToArray()
                    .ShouldBe(new[] { "Argentina", "Hungary", "Luxembourg" });
            },
            browser);
}
