namespace Challenge_Locaweb.Models
{
    public class MongoDBSettingsModel
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; } 
        public string MessagesCollection { get; set; } = null;
    }
}
