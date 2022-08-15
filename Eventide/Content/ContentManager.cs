namespace Eventide.Content;

/// <summary>
/// A manager class that manages the game content.
/// </summary>
public sealed class ContentManager : ObjectBase
{
    #region Constants

    /// <summary>
    /// A readonly collection that contains supported image extensions.
    /// </summary>
    public static readonly string[] SupportedImages =
    {
        ".bmp",
        ".png",
        ".tpa",
        ".jpg",
        ".gif",
        ".psd",
        ".hdr",
        ".pic",
    };

    /// <summary>
    /// A readonly collection that contains supported font extensions.
    /// </summary>
    public static readonly string[] SupportedFonts =
    {
        ".ttf",
        ".otf",
    };

    /// <summary>
    /// A readonly collection that contains supported sound extensions.
    /// </summary>
    public static readonly string[] SupportedSounds =
    {
        ".wav",
        ".ogg",
    };

    #endregion

    #region Fields

    /// <summary>
    /// A readonly collection that contains loaded game content.
    /// </summary>
    public readonly Dictionary<string, IContent> Collection;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the root path of content files.
    /// </summary>
    public string RootPath { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of <see cref="ContentManager"/>.
    /// </summary>
    internal ContentManager (string rootPath)
    {
        RootPath = rootPath;
        Collection = new ();
    }

    #endregion

    #region Finalizers

    /// <inheritdoc/>
    protected override void Destroy (bool disposing)
    {
        foreach (var content in Collection.Values)
        {
            content.Dispose ();
        }

        if (disposing)
        {
            Collection.Clear ();
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Returns the texture that file name is <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Name of the file. e.g: <code>"Content\Images\Player"</code></param>
    /// <returns><see cref="Texture"/></returns>
    public Texture? LoadTexture (string name)
    {
        if (!Collection.TryGetValue (name, out var content))
            return null;

        return (content is Texture texture) ? texture : null;
    }

    /// <summary>
    /// Returns the font that file name is <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Name of the file. e.g: <code>"Content\Fonts\Cascadia-Code"</code></param>
    /// <returns><see cref="Font"/></returns>
    public Font? LoadFont (string name)
    {
        if (!Collection.TryGetValue (name, out var content))
            return null;

        return (content is Font font) ? font : null;
    }

    /// <summary>
    /// Returns the sound that file name is <paramref name="name"/>.
    /// </summary>
    /// <param name="name">Name of the file. e.g: <code>"Content\Sounds\MainMenu"</code></param>
    /// <returns><see cref="Sound"/></returns>
    public Sound? LoadSound (string name)
    {
        if (!Collection.TryGetValue (name, out var content))
            return null;

        return (content is Sound sound) ? sound : null;
    }

    /// <summary>
    /// Visits all directories in <see cref="RootPath"/> and loads the supported files as game content.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public void LoadContent ()
    {
        if (string.IsNullOrEmpty (RootPath))
            throw new (nameof (RootPath));

        if (!Directory.Exists (RootPath))
            Directory.CreateDirectory (RootPath);

        LoadContent (Path.Combine (AppContext.BaseDirectory, RootPath), string.Empty);
    }

    // Loads the content that path includes and add them into to collection.
    internal void LoadContent (string path, string prefix)
    {
        string[] files = Directory.GetFiles (path);
        string[] directories = Directory.GetDirectories (path);

        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            string extension = Path.GetExtension (file);
            string name = Path.Combine (prefix, Path.GetFileNameWithoutExtension (file));

            if (SupportedImages.Contains (extension))
            {
                Collection.Add (name, new Texture (name, file));
            }
            else if (SupportedFonts.Contains (extension))
            {
                Collection.Add (name, new Font (name, file));
            }
            else if (SupportedSounds.Contains (extension))
            {
                Collection.Add (name, new Sound (name, file));
            }
        }

        for (int i = 0; i < directories.Length; i++)
        {
            string directory = directories[i];
            LoadContent (Path.Combine (path, directory), Path.Combine (prefix, directory.Split ('\\')[^1]));
        }
    }

    #endregion
}
