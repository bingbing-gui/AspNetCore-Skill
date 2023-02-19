namespace PartialViews.ViewModels
{
    public class Article
    {
        public string AuthorName { get; set; }

        public DateTime PublicationDate { get; set; }

        public string Title { get; set; }

        public List<ArticleSection> Sections { get; } = new List<ArticleSection>(); 
        
    }
}
