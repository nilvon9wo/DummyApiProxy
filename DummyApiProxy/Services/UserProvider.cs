using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Models.User;
using UserClient = DummyApi.Client.Users.UserProvider;

namespace FluxSzerviz.DummyApiProxy.Host.Services;
public class UserProvider(UserClient userProvider)
{
	public async Task<ICollection<OutboundUser>> GetUsers(CancellationToken cancellationToken) 
		=> (await userProvider.GetUsers(cancellationToken))
			.Match(
				Succ: users 
					=> users.Select(user => OutboundUser.From(user.ToCommon()))
						.ToList(),
				Fail: []
			);
}
