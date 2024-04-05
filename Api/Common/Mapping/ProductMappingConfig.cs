using Application.Services.ProductService;
using Contracts.Product;
using Domain.Common.Models;
using Mapster;

namespace Api.Common.Mapping
{
    public class ProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ProductResult, ProductResponse>()
                .Map(dest => dest.Id, src => src.product.Id.value)
                .Map(dest => dest.Price, src => src.product.Price.value)
                .Map(dest => dest.Name, src => src.product.Name.value)
                .Map(dest => dest.Description, src => src.product.Description)
                .Map(dest => dest.Images, src => src.product.Images)
                .Map(dest => dest.Category, src => new ProductCategoryResponse()
                {
                    CategoryId = src.product.Category.Id.value,
                    Name = src.product.Category.Name.value,
                    Image = src.product.Category.Image
                })
                .Map(dest => dest, src => src.product);

            config.NewConfig<PagedList<ProductResult>, PagedList>()
                .Map(dest => dest.Page, src => src.Page)
                .Map(dest => dest.PageSize, src => src.PageSize)
                .Map(dest => dest.TotalPageCount, src => src.TotalPageCount)
                .Map(dest => dest.TotalItemCount, src => src.TotalItemCount)
                .Map(dest => dest.HasPrevPage, src => src.HasPrevPage)
                .Map(dest => dest.HasNextPage, src => src.HasNextPage);

        }
    }
}
