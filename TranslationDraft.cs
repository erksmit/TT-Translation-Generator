using System.Runtime.Serialization;

namespace TT_Translation_Generator;

[DataContract]
public class TranslationDraft
{
    [DataMember]
    public string Id { get; set; }

    [DataMember]
    public string Type => "translation";

    [DataMember(Name = "*")]
    public Dictionary<string, string> Translation { get; set; }

    public TranslationDraft(string id, Dictionary<string, string>? translation = null)
    {
        Id = id;
        Translation = translation ??= new Dictionary<string, string>();
    }
}
