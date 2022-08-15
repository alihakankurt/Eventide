namespace Eventide.Content;

/// <summary>
/// Defines implementations of this interface as content.
/// </summary>
public interface IContent : IDisposable
{
    #region Properties

    /// <summary>
    /// Gets the name of the <see cref="IContent"/>.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the path to the file.
    /// </summary>
    string Path { get; }

    #endregion
}

/// <summary>
/// Base class for contents.
/// </summary>
public abstract class ContentBase : ObjectBase, IContent
{
    #region Properties

    /// <inheritdoc/>
    public string Name { get; }

    ///<inheritdoc/>
    public string Path { get; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new content object that have <paramref name="name"/> as <see cref="Name"/> and <paramref name="path"/> as <see cref="Path"/>.
    /// </summary>
    /// <param name="name">The name of file.</param>
    /// <param name="path">The path to file.</param>
    public ContentBase (string name, string path)
    {
        Name = name;
        Path = path;
    }

    #endregion
}
