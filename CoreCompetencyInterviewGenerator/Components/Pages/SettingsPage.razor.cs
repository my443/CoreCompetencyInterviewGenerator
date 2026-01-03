using CommunityToolkit.Maui.Storage;
using InterviewGeneratorBlazorHybrid.Data;
using InterviewGeneratorBlazorHybrid.Helpers;
using InterviewGeneratorBlazorHybrid.Models;
using InterviewGeneratorBlazorHybrid.ViewModels;

namespace InterviewGeneratorBlazorHybrid.Components.Pages;

public partial class SettingsPage
{
    private bool _isPicking;
    private string GeneralMessage = string.Empty;
    private string ErrorMessage = string.Empty;
    private string SuccessMessage = string.Empty;
    private string DisplayPath = "Not Set";       
    private AppDbContextFactory _appDbFactory = new AppDbContextFactory();
    private AppDbIntegrityCheck _appDbIntegrityCheck;

    protected override void OnInitialized()
    {
        if (Preferences.Get("DatabaseFilePath", "Not Set") == "Not Set")
        {
            GeneralMessage = "You haven't selected a valid database file yet. Please select one to proceed.";
        }
        DisplayPath = Preferences.Get("DatabaseFilePath", "Not set");            
        _appDbIntegrityCheck = new AppDbIntegrityCheck(_appDbFactory);
    }

    private async Task SelectDifferentDatabase()
    {
        if (_isPicking)
            return;

        _isPicking = true;
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                // FullPath may be null on some platforms — fall back to FileName if needed.
                var selectedPath = result.FullPath ?? result.FileName;
                // Set the preferences, then validate the database
                Preferences.Set("DatabaseFilePath", selectedPath);

                if (!_appDbIntegrityCheck.IsValidDatabase())
                {
                    DisplayErrorMessage("The selected file is not a valid database. Please select a different file.");
                }
                else
                {
                    DisplaySuccessMessage(selectedPath);
                    ResetViewModels();
                }


                // DisplayPath = selectedPath;
                // StateHasChanged();
                // NavigationManager.NavigateTo("/");
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage("The selected file is not a valid database. Please select a different file.");
            StateHasChanged();
        }
        finally
        {
            _isPicking = false;
        }
    }

    private async Task SelectDifferentDocTemplate()
    {
        if (_isPicking)
            return;

        _isPicking = true;
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                
                var selectedPath = result.FullPath ?? result.FileName;                
                Preferences.Set("TemplateDocumentPath", selectedPath);
                InterviewViewModel.UpdateTemplatePath(selectedPath);

                // TODO - DO AN INTEGRITY CHECK ON THE TEMPLATE FILE

                //if (!_appDbIntegrityCheck.IsValidDatabase())
                //{
                //    DisplayErrorMessage("The selected file is not a valid database. Please select a different file.");
                //}
                //else
                //{
                //    DisplaySuccessMessage(selectedPath);
                //    ResetViewModels();
                //}
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage("There was a problem with the selected template file. Please select a different file.");
            StateHasChanged();
        }
        finally
        {
            _isPicking = false;
        }
    }

    public async Task AddNewDatabase()
    {
        SuccessMessage = string.Empty;
        try
        {
            var docxTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        // Windows: extension works
                        { DevicePlatform.WinUI, new[] { ".db" } },

                    });
            //using MemoryStream stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
            using MemoryStream stream = new MemoryStream();
            var result = await FileSaver.Default.SaveAsync($"New Database.db", stream, CancellationToken.None);

            if (result != null && (result.IsSuccessful))
            {
                // Create the ViewModelStore to pass to the CreateDatabaseHelper
                //var viewModelStore = new ViewModelStore(CategoryViewModel, QuestionViewModel, InterviewViewModel);
                CreateDatabaseHelper dbHelper = new CreateDatabaseHelper(_appDbFactory);
                dbHelper.CreateDatabase(result.FilePath);

                ResetViewModels();

                DisplaySuccessMessage(result.FilePath);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage("There was an error. Selected file could not be created.");
        }
    }

    public async Task CreateSampleDatabase() {
      
        await AddNewDatabase();

        // Add Sample Data
        CreateDatabaseHelper dbHelper = new CreateDatabaseHelper(_appDbFactory);
        dbHelper.AddSampleData();
        ResetViewModels();
    }

    private void DisplaySuccessMessage(string selectedPath)
    {
        SuccessMessage = "Database file selected successfully.";
        GeneralMessage = string.Empty;
        ErrorMessage = string.Empty;
        DisplayPath = selectedPath;
    }

    private void DisplayErrorMessage(string errorMessage)
    {
        ErrorMessage = errorMessage;
        DisplayPath = "Not Set";
        Preferences.Set("DatabaseFilePath", "Not Set");
    }

    private void ClearMessages()
    {
        GeneralMessage = string.Empty;
        ErrorMessage = string.Empty;
        SuccessMessage = string.Empty;
    }

    private void ResetViewModels()
    {
        CategoryViewModel.ResetViewModel();
        QuestionViewModel.ResetViewModel();
        InterviewViewModel.ResetViewModel();
    }
}
