using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Models.User;
using UserClient = DummyApi.Client.UserProvider;

namespace FluxSzerviz.DummyApiProxy.Host.Services;
public class UserProvider(UserClient userProvider)
{
	public async Task<List<OutboundUser>> GetUsers()
		=> (await userProvider.GetUsers())
			.Select(user => OutboundUser.From(user.ToCommon()))
			.ToList();
}
