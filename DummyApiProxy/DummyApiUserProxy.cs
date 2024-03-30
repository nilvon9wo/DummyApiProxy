using FluxSzerviz.DummyApiProxy.Host.Models;
using FluxSzerviz.DummyApiProxy.Host.Services;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

using System.Net;

using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Models.User;
namespace FluxSzerviz.DummyApiProxy.Host;

public class DummyApiUserProxy(ILogger<DummyApiUserProxy> logger, UserProvider userProvider)
{
	[Function(nameof(GetUsers))]
	[OpenApiOperation(operationId: "GetMyData", tags: ["My API"], Summary = "Get data from my API")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UsersResponse))]
	public async Task<IActionResult> GetUsers(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/users")] HttpRequest request,
			CancellationToken cancellationToken
		)
	{
		logger.InterpolatedInformation($"C# HTTP trigger function received a request.");
		ICollection<OutboundUser> users = await userProvider.GetUsers(cancellationToken);
		UsersResponse userResponse = UsersResponse.From(users);
		return new OkObjectResult(userResponse);
	}
}
