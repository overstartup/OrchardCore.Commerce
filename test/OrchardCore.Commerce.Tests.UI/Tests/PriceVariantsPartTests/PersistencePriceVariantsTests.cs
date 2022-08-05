﻿using Atata;
using GraphQL;
using Lombiq.Tests.UI.Attributes;
using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace OrchardCore.Commerce.Tests.UI.Tests.PriceVariantsPartTests;

public class PersistencePriceVariantsTests : UITestBase
{
    public PersistencePriceVariantsTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }

    [Theory, Chrome]
    public Task CreatingNewPriceVariantShouldPersist(Browser browser) =>
        ExecuteTestAfterSetupAsync(
            async context =>
            {
                await context.SignInDirectlyAsync();
                await context.CreateNewContentItemAsync("PriceVariantsProduct");

                const string sku = "UITESTSKU";
                const string price = "9999";

                await context.ClickAndFillInWithRetriesAsync(By.Id("ProductPart_Sku"), sku);
                await context.ClickAndFillInWithRetriesAsync(By.Id("PriceVariantsPart_VariantsValues__"), price);
                await context.SetDropdownByTextAsync("PriceVariantsPart_VariantsCurrencies__", "USD");

                await context.ClickReliablyOnSubmitAsync();
                context.ShouldBeSuccess();

                await context.GoToContentItemListAsync("PriceVariantsProduct");
                await context.ClickReliablyOnAsync(By.XPath("//a[. = 'Edit']"));

                context.Get(By.Id("ProductPart_Sku")).GetDomProperty("value").ShouldBe(sku);
                context.Get(By.Id("PriceVariantsPart_VariantsValues__")).GetDomProperty("value").ShouldBe("9999,00");
            },
            browser);
}
