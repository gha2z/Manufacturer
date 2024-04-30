using IntrManApp.Shared.Contract;
using IntrManApp.Shared.Contract.Request.Tools;
using IntrManAppHybridApp.Tools.Bartender;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text.Json;
namespace IntrManHybridApp.UI.Services;

public class LabelPrintingService(ILogger<LabelPrintingService> logger, HttpClient httpClient) : ILabelPrintingService
{
    public async Task<IEnumerable<string>> PrintCartonIds(IEnumerable<ProductCheckInLineDetailResponse> items, string? printerName)
    {
        string filename = "rCartonLabel.btw";
        var path = Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.CommonApplicationData), $"{AppInfo.Name}");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        path = Path.Combine(path, "labels");
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        var sourcePath = Path.Combine(path, "cartonIds.json");
        var labelPath = Path.Combine(path, filename);

        if(!File.Exists(labelPath))
        {
            logger.LogError($"LabelPrintingService - Label file not found, create one ...");
            using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);

            // Create an output filename
            string targetFile = labelPath;

            // Copy the file to the AppDataDirectory
            using FileStream outputStream = File.Create(targetFile);
            await inputStream.CopyToAsync(outputStream);
        }
        
        logger.LogInformation($"LabelPrintingService - generating json file:{sourcePath}");
        var json = JsonSerializer.Serialize(items);
        await File.WriteAllTextAsync(sourcePath, json);

        BTAction action = new()
        {
            PrintBTWAction = new PrintBTWAction
            {
                DocumentFile = labelPath,
                Printer = null,
                SaveAfterPrint = false
            }
        };

        var uri = "actions?KeepStatus=60m&Wait=15s&MessageCount=200&MessageSeverity=Info";
        logger.LogInformation($"LabelPrintingService - sending printing request to {httpClient.BaseAddress}{uri}" +
            $"{Environment.NewLine}==>{JsonSerializer.Serialize(action)}");

        IEnumerable<string> messages = [];
        try
        {
            var response = await httpClient.PostAsJsonAsync<BTAction>(uri, action);


            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<BTPrintResponse>();
                messages = result?.Messages ?? [];

            }
            else
            {
                logger.LogError($"LabelPrintingService - Print failed: {response.StatusCode}");
                messages = [$"Print failed: {response.StatusCode}"];
            };
        }
        catch (Exception ex)
        {
            logger.LogError($"LabelPrintingService - Error while sending printing request to {httpClient.BaseAddress}{uri}" +
                               $"{Environment.NewLine}==>{JsonSerializer.Serialize(action)}" +
                                              $"{Environment.NewLine}==>{ex.Message}{Environment.NewLine}{ex}");
        }
        return [];
    }
}
