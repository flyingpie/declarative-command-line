using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DeclarativeCommandLine.Generator;

public static class Counter
{
	public static int _num;

	public static int Next()
	{
		Interlocked.Increment(ref _num);

		return _num;
	}
}

public class CommandView
{
	public int Index { get; set; } = Counter.Next();

	public AttributeData Attribute { get; private set; }

	public INamedTypeSymbol Symbol { get; private set; }

	public List<PropertyView> Properties { get; set; }

	public string FullName => Symbol.ToDisplayString();

	public string Name => Symbol.Name;

	public string CmdName { get; set; }

	public INamedTypeSymbol? CmdParent { get; set; }

	public static bool TryParse(DeclContext ctx, TypeDeclarationSyntax decl, out CommandView? view)
	{
		view = null;

		var model = ctx.Compilation.GetSemanticModel(decl.SyntaxTree);
		var symbol = model.GetDeclaredSymbol(decl);

		if (symbol is not INamedTypeSymbol namedSymbol)
		{
			return false;
		}

		var cmdAttr = symbol
			.GetAttributes()
			.FirstOrDefault(a => a.AttributeClass?.EqualsNamedSymbol(ctx.Types.CommandAttribute) ?? false);

		if (cmdAttr == null)
		{
			return false;
		}

		var typeName = symbol.ToDisplayString(); // fully qualified
		var safeName = symbol.Name; // class name only

		view = new()
		{
			Attribute = cmdAttr,
			Symbol = namedSymbol,
			Properties =
			[
				.. namedSymbol
					.GetMembers()
					.OfType<IPropertySymbol>()
					.Select(s => PropertyView.TryParse(ctx, s, out var p) ? p : null)
					.Where(p => p != null)
					.Select(p => p!)
			],
		};

		view.CmdName = cmdAttr.ConstructorArguments.FirstOrDefault().Value as string;

		foreach (var constrArg in cmdAttr.NamedArguments)
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
						var dbg3 = 3;
						break;
					}

				case "Hidden":
					{
						var dbg3 = 3;
						break;
					}

				case "Name":
					{
						var dbg3 = 3;
						var n = constrArg.Value.Value as string;
						view.CmdName = n;
						break;
					}

				case "Parent":
					{
						var dbg3 = 3;
						view.CmdParent = constrArg.Value.Value as INamedTypeSymbol;
						break;
					}

				// case "Root":
				// 	{
				// 		var dbg3 = 3;
				// 		break;
				// 	}

				default:
					break;
			}
		}

		view.CmdName ??= NameFormatter.CommandTypeToCommandName(symbol.Name);

		return true;
	}

	public override string ToString() => FullName;
}