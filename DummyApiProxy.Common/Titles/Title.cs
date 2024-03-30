using System.ComponentModel;

namespace DummyApiProxy.Common.Enums;
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
