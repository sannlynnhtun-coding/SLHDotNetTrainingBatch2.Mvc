namespace SLHDotNetTrainingBatch2.Mvc.Models
{
    public class BlogCreateRequestModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }

    public class BlogEditRequestModel
    {
        public string BlogId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}
