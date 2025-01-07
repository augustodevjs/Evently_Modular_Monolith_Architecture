using Evently.Common.Application.Caching;
using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Common.Presentation.Endpoints;
using Evently.Modules.Events.Application.Categories.GetCategories;
using Evently.Modules.Events.Application.Categories.GetCategory;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Categories;

internal sealed class GetCategories : IEndpoint
{
    public void MapEndPoint(IEndpointRouteBuilder app)
    {
        app.MapGet("categories", async (ISender sender, ICacheService cacheService) =>
            {
                Result<IReadOnlyCollection<CategoryResponse>>? categoryResponse =
                    await cacheService.GetOrCreateAsync("categories",
                        async () => await sender.Send(new GetCategoriesQuery()));

            return categoryResponse?.Match(Results.Ok, Common.Presentation.ApiResults.ApiResults.Problem);
        })
        .WithTags(Tags.Categories);
    }
}
