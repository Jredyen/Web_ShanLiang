namespace prjShanLiang.Models
{
    public class CBlogwrap
    {
        private Blog _blog;
        public Blog blog
        {
            get { return _blog; }
            set { _blog = value; }
        }
        public CBlogwrap()
        {
            _blog = new Blog();
        }
        public int BlogId {
            get { return _blog.BlogId; }
            set { _blog.BlogId = value; }
        }

        public string? BlogHeader {
            get { return _blog.BlogHeader; }
            set { _blog.BlogHeader = value; } 
        }

        public string? BlogContent {
            get { return _blog.BlogContent; }
            set { _blog.BlogContent = value; } 
        }

        public string? BlogPic
        {
            get { return _blog.BlogPic; }
            set { _blog.BlogPic = value; }
        }
        public IFormFile photo { get; set; }
    }
}
