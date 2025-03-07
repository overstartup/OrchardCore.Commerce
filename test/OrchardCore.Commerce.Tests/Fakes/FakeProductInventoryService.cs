using OrchardCore.Commerce.Abstractions;
using OrchardCore.Commerce.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Tests.Fakes;

public class FakeProductInventoryService : IProductInventoryService
{
    // IProductInventoryService's method needs to be created, but implementation is unnecessary as the tests do not use it.
    public Task<IList<ShoppingCartItem>> UpdateInventoriesAsync(IList<ShoppingCartItem> items) =>
        throw new NotSupportedException();
}
