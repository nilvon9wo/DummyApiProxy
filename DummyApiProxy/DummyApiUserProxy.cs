using Ardalis.GuardClauses;

using DotNetTips.Spargine.Extensions;

using FluxSzerviz.DummyApiProxy.Host.Users;

using LanguageExt;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

using System.Net;

using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Users.User;
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
		_ = Guard.Against.Null(userProvider);
		logger.InterpolatedInformation($"C# HTTP trigger function received a request.");
		TryAsync<IEnumerable<OutboundUser>> outboundUserAttempt = userProvider.GetUsers(cancellationToken);
		IActionResult result = await outboundUserAttempt.Match(
				Succ: users 
					=> result = new OkObjectResult(UsersResponse.From(users.ToCollection())),

				Fail: exception 
					=> result = new ObjectResult(UsersResponse.From(exception)) { 
							StatusCode = StatusCodes.Status500InternalServerError
						});
		return result;
	}
}
