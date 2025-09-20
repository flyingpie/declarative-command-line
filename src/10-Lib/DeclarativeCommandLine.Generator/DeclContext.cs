using Microsoft.CodeAnalysis;

namespace DeclarativeCommandLine.Generator;

public class DeclContext(Compilation compilation)
{
	public Compilation Compilation { get; set; } = compilation;

	public DeclTypes Types { get; set; } = new(compilation);
}