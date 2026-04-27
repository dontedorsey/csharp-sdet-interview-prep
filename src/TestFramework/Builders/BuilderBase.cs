namespace TestFramework.Builders;

/// <summary>
/// Generic fluent builder base. Inherit and add With* methods.
/// </summary>
public abstract class BuilderBase<TBuilder, TEntity>
    where TBuilder : BuilderBase<TBuilder, TEntity>
    where TEntity : class, new()
{
    protected TEntity Entity = new();

    public TEntity Build() => Entity;

    public IList<TEntity> BuildList(int count)
        => Enumerable.Range(0, count).Select(_ => Build()).ToList();
}
