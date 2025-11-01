using System.Runtime.InteropServices;

namespace DeclarativeCommandLine.Generator;

public class PropertyView
{
	public int Index { get; set; } = Counter.Next();

	public IPropertySymbol Symbol { get; private set; } = null!;

	public AttributeData? ArgumentAttribute { get; private set; }

	public AttributeData? OptionAttribute { get; private set; }

	public string? OptName { get; private set; }

	public string PropertyTypeName { get; private set; }

	public string PropertyTypeNameWithNullable => PropertyIsNullable ? $"{PropertyTypeName}?" : PropertyTypeName;

	public bool PropertyIsNullable { get; private set; }

	public string PropertyName => Symbol.Name;

	public bool OptHidden { get; private set; }

	public bool OptRequired { get; private set; }

	public string? OptDescription { get; private set; }

	public static bool TryParse(DeclContext ctx, IPropertySymbol symbol, out PropertyView? view)
	{
		if (ctx == null)
		{
			throw new ArgumentNullException(nameof(ctx));
		}

		if (symbol == null)
		{
			throw new ArgumentNullException(nameof(symbol));
		}

		view = null;

		var attrs = symbol.GetAttributes();

		var argumentAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.ArgumentAttribute) ?? false);
		var optionAttr = attrs.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.OptionAttribute) ?? false);

		if (argumentAttr == null && optionAttr == null)
		{
			return false;
		}

		view = new() { Symbol = symbol, ArgumentAttribute = argumentAttr, OptionAttribute = optionAttr, };

		view.PropertyTypeName = symbol.Type.Name;

		if (symbol.Type.Name.Equals("Nullable", StringComparison.Ordinal))
		{
			view.PropertyIsNullable = true;

			var innerType = (symbol.Type as INamedTypeSymbol)?.TypeArguments.FirstOrDefault();
			view.PropertyTypeName = innerType.Name;
		}

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
				case "Description":
					{
						view.OptDescription = constrArg.Value.Value as string;
						break;
					}

				case "Hidden":
					{
#pragma warning disable CS8605 // Unboxing a possibly null value. // MvdO: TODO: Refactor to extension methods, like for Command
						view.OptHidden = (bool)constrArg.Value.Value;
#pragma warning restore CS8605 // Unboxing a possibly null value.
						break;
					}

				case "Name":
					{
						view.OptName = constrArg.Value.Value as string;
						break;
					}

				case "Required":
					{
#pragma warning disable CS8605 // Unboxing a possibly null value. // MvdO: TODO: Refactor to extension methods, like for Command
						view.OptRequired = (bool)constrArg.Value.Value;
#pragma warning restore CS8605 // Unboxing a possibly null value.
						break;
					}
			}
		}
	}
}