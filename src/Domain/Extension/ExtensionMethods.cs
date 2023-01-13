using MongoDB.Driver;
using System.Net;
using System;
using Domain.Models;

namespace Domain.Extension;

public static class IncludProperties
{
    public static async Task<Product> IncludeProperty(this Product model, IMongoDatabase mongoDB)
    {
        var relatedInfo = mongoDB.GetCollection<Category>("Categories");
        model.Category = await relatedInfo.Find(add => add.Id == model.CategoryId).FirstOrDefaultAsync();
        return model;
    }
}


