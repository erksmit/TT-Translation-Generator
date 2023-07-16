using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TT_Translation_Generator;

if (args.Length == 0)
{
    Console.WriteLine("Please pass the path to the folder with your plugin as an argument.");
    return;
}
else if (args.Length == 1)
{
    Console.WriteLine("Please pass the id of the draft you'd like to generate as an argument.");
    return;
}

string workingfolder = args[0];
string translationId = args[1];

if (!Directory.Exists(workingfolder))
{
    Console.WriteLine("Invalid folder, please pass a valid folder path");
    return;
}

var convertSettings = new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver(),
    Formatting = Formatting.Indented,
};

var result = new TranslationDraft(translationId);
foreach (var file in Directory.GetFiles(workingfolder, "*.json", SearchOption.AllDirectories))
{
    if (file.Split(Path.DirectorySeparatorChar).Any(s => s.StartsWith(".")))
    {
        Console.WriteLine("Skipping file in ignored folder " + file);
        continue;
    }

    string content = File.ReadAllText(file);
    List<Draft>? drafts;
    try
    {
        drafts = JsonConvert.DeserializeObject<List<Draft>>(content, convertSettings);
    }
    catch
    {
        Console.WriteLine("Unrecognised json in " + file);
        continue;
    }
    if (drafts == null)
        continue;
    if (drafts.Count == 0)
        continue;

    foreach(var draft in drafts)
    {
        var cleanedId = draft.Id.TrimStart('$');

        if (draft.Title != null)
        {
            result.Translation.Add("draft_" + cleanedId + "_title", draft.Title);
        }
        
        if (draft.Text != null)
        {
            result.Translation.Add("draft_" + cleanedId + "_text", draft.Text);
        }

        Console.WriteLine("Added translations for " + file);
    }
}


string resultJson = JsonConvert.SerializeObject(result, convertSettings);
File.WriteAllText(workingfolder + Path.DirectorySeparatorChar + "translation.json", resultJson);

Console.WriteLine("Result generated as translation.json in the plugin's folder");
Console.WriteLine("Press any key to quit...");
Console.ReadKey();