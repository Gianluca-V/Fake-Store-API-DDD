using Application.Services.AuthenticationService;
using Contracts.Authentication;
using Mapster;

namespace Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src => src.user.id.value)
            .Map(dest => dest.Email, src => src.user.email.value)
            .Map(dest => dest, src => src.user);
        }
    }
}
