namespace PatchSeller.Web.DTOs
{
    public class ChatNodeResponse
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";
        public string? Response { get; set; }
        public List<ChatNodeResponse>? Children { get; set; }
    }

    public class ChatBotResponse
    {
        public string Text { get; set; } = "";
        public bool IsBot { get; set; }
        public bool IsTyping { get; set; }
        public List<ChatNodeResponse>? Options { get; set; }
    }
}
