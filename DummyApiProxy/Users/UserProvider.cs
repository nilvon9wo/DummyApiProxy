using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Users.User;
using UserClient = FluxSzerviz.DummyApi.Client.Users.UserClient;

namespace FluxSzerviz.DummyApiProxy.Host.Users;
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
