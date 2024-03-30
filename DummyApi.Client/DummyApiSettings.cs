﻿namespace FluxSzerviz.DummyApi.Client;

public class DummyApiSettings
{
	public required Uri BaseUrl { get; init; }
	public required string ApiKey { get; init; }
	public required int DefaultPageLimit { get; init; }
}
