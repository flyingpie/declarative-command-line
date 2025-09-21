using Microsoft.CodeAnalysis;
using System.Linq;

namespace DeclarativeCommandLine.Generator;

public class PropertyView
{
	public int Index { get; set; } = Counter.Next();

	public IPropertySymbol Symbol { get; set; }

	public AttributeData? ArgumentAttribute { get; set; }

	public AttributeData? OptionAttribute { get; set; }

	public string? OptName { get; set; }

	public string PropertyTypeName => Symbol.Type.Name;

	public string PropertyName => Symbol.Name;

	public bool OptHidden { get; set; }

	public bool OptRequired { get; set; }

	public string? OptDescription { get; set; }

	public static bool TryParse(DeclContext ctx, IPropertySymbol symbol, out PropertyView? view)
	{
		view = null;

		var attrs = symbol.GetAttributes();

		var argumentAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.ArgumentAttribute) ?? false);
		var optionAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.OptionAttribute) ?? false);

		if (argumentAttr == null && optionAttr == null)
		{
			return false;
		}

		view = new() { Symbol = symbol, ArgumentAttribute = argumentAttr, OptionAttribute = optionAttr, };

		if (argumentAttr != null)
		{
			ParseArgument(view, argumentAttr);
			ParseShared(view, argumentAttr);
		}

		if (optionAttr != null)
		{
			ParseOption(view, optionAttr);
			ParseShared(view, optionAttr);
		}

		view.OptName ??= NameFormatter.PropertyNameToOptionName(symbol.Name);

		return true;
	}

	private static void ParseArgument(PropertyView view, AttributeData optionAttr)
	{
		// TODO
	}

	private static void ParseOption(PropertyView view, AttributeData optionAttr)
	{
		// TODO
	}

	private static void ParseShared(PropertyView view, AttributeData optionAttr)
	{
		view.OptName = optionAttr.ConstructorArguments.FirstOrDefault().Value as string;

		foreach (var constrArg in optionAttr.NamedArguments)
		{
			switch (constrArg.Key)
			{
				case "Aliases":
					{
						var dbg3 = 3;
						break;
					}

				case "Description":
					{
						view.OptDescription = constrArg.Value.Value as string;
						break;
					}

				case "Hidden":
					{
						view.OptHidden = (bool)constrArg.Value.Value;
						break;
					}

				case "Name":
					{
						view.OptName = constrArg.Value.Value as string;
						break;
					}

				case "Required":
					{
						view.OptRequired = (bool)constrArg.Value.Value;
						break;
					}

				default:
					break;
			}
		}
	}
}