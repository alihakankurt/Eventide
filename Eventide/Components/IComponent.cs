namespace Eventide.Components;

/// <summary>
/// Defines implementations of this interface as a component.
/// </summary>
public interface IComponent : IDisposable
{
    #region Properties

    /// <summary>
    /// Gets the owner of the <see cref="IComponent"/>.
    /// </summary>
    public Entity Owner { get; }

    #endregion
}

/// <summary>
/// Base class of components.
/// </summary>
public abstract class ComponentBase : ObjectBase, IComponent
{
    #region Properties

    /// <inheritdoc/>
    public Entity Owner { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new component object that owns to <paramref name="owner"/>.
    /// </summary>
    /// <param name="owner">Owner of the component.</param>
    public ComponentBase (Entity owner)
    {
        Owner = owner;
    }

    #endregion
}
