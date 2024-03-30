using OutboundUser = FluxSzerviz.DummyApiProxy.Host.Users.User;
using UserClient = FluxSzerviz.DummyApi.Client.Users.UserClient;
using static LanguageExt.Prelude;
using LanguageExt;

namespace FluxSzerviz.DummyApiProxy.Host.Users;
public class UserProvider(UserClient userProvider)
{
	public virtual TryAsync<IEnumerable<OutboundUser>> GetUsers(CancellationToken cancellationToken)
		=> userProvider.GetUsers(cancellationToken)
			.Map(users => users.Select(user => OutboundUser.From(user.ToCommon())));
}
