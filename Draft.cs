using System.Runtime.Serialization;

namespace TT_Translation_Generator;

[DataContract]
public class Draft
{
    [DataMember]
    public string Id { get; }

    [DataMember]
    public string Title { get; }

    [DataMember]
    public string Text { get; }

    public Draft(string id, string title, string text)
    {
        Id = id;
        Title = title;
        Text = text;
    }
}