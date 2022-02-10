
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CloudnessMarketplace.Blazor;

public class AuthorizationMessageHandler : DelegatingHandler
{

    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IConfiguration _configuration;
    
    public AuthorizationMessageHandler(IAccessTokenProvider accessTokenProvider, IConfiguration configuration)
    {
        _accessTokenProvider = accessTokenProvider;
        _configuration = configuration;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Try to fetch the access token 
        var tokenResult = await _accessTokenProvider.RequestAccessToken(new AccessTokenRequestOptions
        {
            Scopes = new[] { _configuration["AzureAdB2C:DefaultScope"] }
        });

        // Set the access token in the header of the request if it's found
        if (tokenResult.Status == AccessTokenResultStatus.Success)
        {
            if (tokenResult.TryGetToken(out var accessToken))
                request.Headers.Add("Authorization", $"Bearer {accessToken}");
        }

        return await base.SendAsync(request, cancellationToken);
    }

}
