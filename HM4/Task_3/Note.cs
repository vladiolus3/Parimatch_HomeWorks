using System;
using System.Text.Json.Serialization;

namespace Task_3
{
    [Serializable]
    public class Note : INote
    {
        [JsonPropertyName("id")]
        public int Id { set; get; }
        [JsonPropertyName("title")]
        public string Title { set; get; }
        [JsonPropertyName("text")]
        public string Text { set; get; }
        [JsonPropertyName("date")]
        public DateTime CreatedOn { set; get; }
        public Note() { }
        public Note(int id, string title, string text, DateTime createdOn)
        {
            if (id < 0) throw new ArgumentOutOfRangeException("ID cannot be less than zero!");
            Id = id;
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentNullException("The title is cannot be empty!");
            Title = title;
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("The text is cannot be empty!");
            Text = text;
            CreatedOn = createdOn;
        }
        public string ToString(bool withText)
        {
            string output;
            if (withText == true)
            {
                output = $"Id:\t{Id}\n" +
                   $"Title:\t{Title}\n" +
                   $"Text:\t{Text}\n" +
                   $"Created On:\t{CreatedOn}\n";
            }
            else
            {
                output = $"Id:\t{Id}\n" +
                   $"Title:\t{Title}\n" +
                   $"Created On:\t{CreatedOn}\n";
            }
            return output;
        }
    }
}
