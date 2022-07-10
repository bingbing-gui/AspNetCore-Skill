namespace AspNetCore.UserSecrets.Models
{
    public class MovieSettings
    {
        public string ConnectionString { get; set; }

        public string ServiceApiKey { get; set; }

        public override string ToString()
        {
            return $"ConnectionString:{ConnectionString}{Environment.NewLine}ServiceApiKey={ServiceApiKey}";
        }
    }
}
