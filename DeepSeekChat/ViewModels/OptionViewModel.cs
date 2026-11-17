using System.Runtime.Serialization;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// A choice button in Settings: a language or a theme.
/// </summary>
[DataContract]
internal sealed class OptionViewModel : ThemedItem
{
    private string _label;
    private bool _isSelected;

    /// <summary>
    /// Creates a choice button.
    /// </summary>
    /// <param name="id">
    /// Option id (language code or theme id).
    /// </param>
    /// <param name="label">
    /// Button label.
    /// </param>
    /// <param name="swatch">
    /// Sample circle color; an empty string if no circle is needed.
    /// </param>
    /// <param name="ui">
    /// Shared appearance state.
    /// </param>
    /// <param name="select">
    /// Action invoked when the option is picked; receives the id.
    /// </param>
    public OptionViewModel(string id, string label, string swatch, UiContext ui, Func<string, Task> select)
        : base(ui)
    {
        Id = id;
        _label = label;
        Swatch = swatch;
        SelectCommand = new AsyncCommand((parameter, token) => select(id));
    }

    /// <summary>
    /// Gets id.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// Gets swatch.
    /// </summary>
    [DataMember]
    public string Swatch { get; }

    /// <summary>
    /// Gets or sets label.
    /// </summary>
    [DataMember]
    public string Label
    {
        get => _label;
        set => SetProperty(ref _label, value);
    }

    /// <summary>
    /// Gets or sets is selected.
    /// </summary>
    [DataMember]
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    /// <summary>
    /// Gets select command.
    /// </summary>
    [DataMember]
    public AsyncCommand SelectCommand { get; }

    /// <summary>
    /// Marks the selected option in a list.
    /// </summary>
    /// <param name="options">
    /// The list options.
    /// </param>
    /// <param name="selectedId">
    /// Id of the selected option.
    /// </param>
    public static void MarkSelected(IEnumerable<OptionViewModel> options, string selectedId)
    {
        foreach (OptionViewModel option in options)
        {
            option.IsSelected = option.Id == selectedId;
        }
    }
}
