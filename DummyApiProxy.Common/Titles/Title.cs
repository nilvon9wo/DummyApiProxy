using System.ComponentModel;

namespace FluxSzerviz.DummyApiProxy.Common.Titles;
public enum Title
{
	[Description("Mr")]
	Mr,

	[Description("Ms")]
	Ms,

	[Description("Mrs")]
	Mrs,

	[Description("Miss")]
	Miss,

	[Description("Dr")]
	Dr,

	[Description("")]
	None
}
