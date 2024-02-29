using Application.Services.CategoryService;
using Contracts.Category;
using Mapster;

namespace Api.Common.Mapping
{
    public class CategoryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CategoryResult, CategoryResponse>()
            .Map(dest => dest.Id, src => src.category.Id.value)
            .Map(dest => dest.Name, src => src.category.Name.value)
            .Map(dest => dest.Image, src => src.category.Image);
        }
    }
}
