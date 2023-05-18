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
        public string? CityName
        {
            get { return _blog.CityName; }
            set { _blog.CityName = value; }
        }

        public string? DistrictName 
        {
            get { return _blog.DistrictName; }
            set { _blog.DistrictName = value; } 
        }

        public string? RestaurantName 
        {
            get { return _blog.RestaurantName; }
            set { _blog.RestaurantName = value; } 
        }
        public IFormFile photo { get; set; }
    }
}
