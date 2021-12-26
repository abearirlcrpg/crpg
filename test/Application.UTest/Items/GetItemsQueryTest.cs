using Crpg.Application.Items.Queries;
using Crpg.Domain.Entities.Items;
using NUnit.Framework;

namespace Crpg.Application.UTest.Items;

public class GetItemsQueryTest : TestBase
{
    [Test]
    public async Task BaseItems()
    {
        Item[] items =
        {
            new()
            {
                Name = "toto",
                Value = 100,
                Type = ItemType.BodyArmor,
                Rank = 0,
            },
            new()
            {
                Name = "toto",
                Value = 100,
                Type = ItemType.ShoulderArmor,
                Rank = 3,
            },
            new()
            {
                Name = "tata",
                Value = 200,
                Type = ItemType.HandArmor,
                Rank = 0,
            },
        };
        ArrangeDb.Items.AddRange(items);
        await ArrangeDb.SaveChangesAsync();

        GetItemsQuery.Handler handler = new(ActDb, Mapper);
        var result = await handler.Handle(new GetItemsQuery { BaseItems = true }, CancellationToken.None);

        Assert.AreEqual(2, result.Data!.Count);
    }

    [Test]
    public async Task AllItems()
    {
        Item[] items =
        {
            new()
            {
                Name = "toto",
                Value = 100,
                Type = ItemType.BodyArmor,
                Rank = 0,
            },
            new()
            {
                Name = "toto",
                Value = 100,
                Type = ItemType.ShoulderArmor,
                Rank = 3,
            },
            new()
            {
                Name = "tata",
                Value = 200,
                Type = ItemType.HandArmor,
                Rank = 0,
            },
        };
        ArrangeDb.Items.AddRange(items);
        await ArrangeDb.SaveChangesAsync();

        GetItemsQuery.Handler handler = new(ActDb, Mapper);
        var result = await handler.Handle(new GetItemsQuery { BaseItems = false }, CancellationToken.None);

        Assert.AreEqual(3, result.Data!.Count);
    }
}
