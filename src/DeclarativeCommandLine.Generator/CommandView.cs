using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DeclarativeCommandLine.Generator;

public class CommandView
{
	public int Index { get; } = Counter.Next();

	public AttributeData Attribute { get; private set; } = null!;

	public INamedTypeSymbol Symbol { get; private set; } = null!;

	public ICollection<PropertyView> Properties { get; private set; } = null!;

	public string FullName => $"global::{Symbol.ToDisplayString()}";

	public string Name => Symbol.Name;

	public string? CmdDescription { get; private set; }

	public string CmdName { get; private set; } = null!;

	public INamedTypeSymbol? CmdParent { get; set; }

	public IReadOnlyCollection<string> CmdAliases { get; private set; } = [];

	public bool CmdHidden { get; private set; }

	public bool IsExecutable { get; private set; }

	public static bool TryParse(DeclContext ctx, TypeDeclarationSyntax decl, out CommandView? view)
	{
		if (ctx == null)
		{
			throw new ArgumentNullException(nameof(ctx));
		}

		if (decl == null)
		{
			throw new ArgumentNullException(nameof(decl));
		}

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

		view = new();

		// Type info
		view.Attribute = cmdAttr;
		view.Symbol = namedSymbol;

		// Child properties (attributes and options)
		view.Properties =
		[
			.. namedSymbol
				.GetAllTypes()
				.SelectMany(t => t.GetMembers())
				.OfType<IPropertySymbol>()
				.Select(s => PropertyView.TryParse(ctx, s, out var p) ? p : null)
				.Where(p => p != null)
				.Select(p => p!)
		];

		// IsExecutable
		view.IsExecutable = namedSymbol
			.GetAllTypes()
			.Any(t => t.Interfaces.Any(i => ctx.Types.ExecutableCommandTypeNames.Any(ex => i.OriginalDefinition.EqualsNamedSymbol(ex))));

		// Name
		var cmdNameFromConstrArg = cmdAttr.ConstructorArguments.FirstOrDefault().Value as string;
		var cmdNameFromNamedArg = cmdAttr.NamedArguments.GetNamedArgument("Name") as string;
		var cmdNameFromType = NameFormatter.CommandTypeToCommandName(symbol.Name);
		view.CmdName = cmdNameFromConstrArg ?? cmdNameFromNamedArg ?? cmdNameFromType;

		// Aliases
		view.CmdAliases = cmdAttr.NamedArguments.GetNamedArgumentArray<string>("Aliases");

		// Description
		view.CmdDescription = cmdAttr.NamedArguments.GetNamedArgument("Description") as string;

		// Hidden
		view.CmdHidden = cmdAttr.NamedArguments.GetNamedArgument("Hidden") as bool? ?? false;

		// Parent
		view.CmdParent = cmdAttr.NamedArguments.GetNamedArgument("Parent") as INamedTypeSymbol;

		return true;
	}

	public override string ToString() => FullName;
}