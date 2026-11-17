using System.Runtime.Serialization;
using DeepSeekChat.Models;
using DeepSeekChat.Services;
using Microsoft.VisualStudio.Extensibility.UI;

namespace DeepSeekChat.ViewModels;

/// <summary>
/// Model picker shown under the input box.
/// </summary>
[DataContract]
internal sealed class ModelPickerViewModel : NotifyPropertyChangedObject
{
    private readonly UiContext _ui;

    private string _selectedName;
    private bool _isMenuOpen;

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelPickerViewModel"/> class.
    /// </summary>
    public ModelPickerViewModel(UiContext ui)
    {
        _ui = ui;
        _selectedName = ui.Model.DisplayName;

        foreach (ChatModel model in ChatModels.All)
        {
            Models.Add(new OptionViewModel(model.Id, model.DisplayName, string.Empty, ui, SelectModelAsync));
        }

        OptionViewModel.MarkSelected(Models, ui.Model.Id);

        OpenMenuCommand = new AsyncCommand((parameter, token) => OpenMenuAsync());
    }

    /// <summary>
    /// Gets models.
    /// </summary>
    [DataMember]
    public ObservableList<OptionViewModel> Models { get; } = new();

    /// <summary>
    /// Gets or sets selected name.
    /// </summary>
    [DataMember]
    public string SelectedName { get => _selectedName; private set => SetProperty(ref _selectedName, value); }

    /// <summary>
    /// Two-way bound to the drop-up list of the model button.
    /// </summary>
    /// <summary>
    /// Gets or sets is menu open.
    /// </summary>
    [DataMember]
    public bool IsMenuOpen
    {
        get => _isMenuOpen;
        set => SetProperty(ref _isMenuOpen, value);
    }

    /// <summary>
    /// Gets open menu command.
    /// </summary>
    [DataMember]
    public AsyncCommand OpenMenuCommand { get; }

    private Task OpenMenuAsync()
    {
        IsMenuOpen = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Selects a model, remembers the choice and closes the list.
    /// </summary>
    /// <param name="modelId">
    /// Id of the chosen model.
    /// </param>
    /// <returns>
    /// A completed task.
    /// </returns>
    private Task SelectModelAsync(string modelId)
    {
        _ui.SetModel(modelId);
        OptionViewModel.MarkSelected(Models, _ui.Model.Id);
        SelectedName = _ui.Model.DisplayName;
        IsMenuOpen = false;
        return Task.CompletedTask;
    }
}
