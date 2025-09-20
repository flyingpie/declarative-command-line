using Microsoft.CodeAnalysis;
using System.Linq;

namespace DeclarativeCommandLine.Generator;

public class PropertyView
{
	public IPropertySymbol Symbol { get; set; }

	public AttributeData? ArgumentAttribute { get; set; }

	public AttributeData? OptionAttribute { get; set; }

	public static bool TryParse(DeclContext ctx, IPropertySymbol symbol, out PropertyView? view)
	{
		view = null;

		var attrs = symbol.GetAttributes();

		var argumentAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.OptionAttribute) ?? false);
		var optionAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.OptionAttribute) ?? false);

		if (argumentAttr == null && optionAttr == null)
		{
			return false;
		}

		view = new() { Symbol = symbol, ArgumentAttribute = argumentAttr, OptionAttribute = optionAttr, };

		return true;
	}
}