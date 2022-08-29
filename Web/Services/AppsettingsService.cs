using Newtonsoft.Json.Linq;

namespace Vulns.Services;

public class AppsettingsService
{
    private readonly ILogger<AppsettingsService> _logger;
    private readonly IWebHostEnvironment _env;

    // Change configurations in appsettings.json.
    public AppsettingsService(ILogger<AppsettingsService> logger, IWebHostEnvironment env) => (_logger, _env) = (logger, env);

    public bool Update(string section, string key, Type valueType, object value)
    {
        // Read appsettings[.ENVIRONMENT].json
        string appSettingsEnvironmentPostfix = $".{_env.EnvironmentName}";
        if (appSettingsEnvironmentPostfix == ".Production") appSettingsEnvironmentPostfix = "";
        string appSettingsFilePath = $"{_env.ContentRootPath}appsettings{appSettingsEnvironmentPostfix}.json";
        string jsonString = File.ReadAllText(appSettingsFilePath);

        _logger.LogTrace($"Appsettings File Path: {appSettingsFilePath}");
        _logger.LogTrace($"Section: {section}");
        _logger.LogTrace($"key: {key}");
        _logger.LogTrace($"value: {value}");
        _logger.LogTrace($"value type: {valueType.Name}");

        // Convert the JSON string to a JObject:
        JObject? jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString) as JObject;
        if (jObject == null)
        {
            _logger.LogTrace($"Could not deserialize appsettings into a json object");
            return false;
        }

        // Select a nested property using a single string:
        JToken? jToken = jObject.SelectToken($"{section}.{key}");
        if (jToken == null)
        {
            _logger.LogTrace($"Could not find {section}.{key} in appsettings");
            return false;
        }

        // Update the value of the property: 
        if (valueType == typeof(int)) jToken.Replace((int)value);
        else if (valueType == typeof(string)) jToken.Replace((string)value);
        else if (valueType == typeof(bool)) jToken.Replace((bool)value);
        else {
            _logger.LogTrace($"Type {valueType.Name} is not supported in AppsettingsService");
            return false;
        }

        // Convert the JObject back to a string:
        string updatedJsonString = jObject.ToString();
        File.WriteAllText(appSettingsFilePath, updatedJsonString);
        _logger.LogInformation($"Updated Appsettings key {section}.{key} to {value}!");
        return true;
    }
}