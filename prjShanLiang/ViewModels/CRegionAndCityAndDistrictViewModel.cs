namespace prjShanLiang.ViewModels
{
    public class CRegion
    {
        public string regionName { get; set; }
        public List<CCity> Cities { get; set; }
    }

    public class CCity
    {
        public string cityName { get; set; }
        public List<CDistrict> Districts { get; set; }
    }

    public class CDistrict
    {
        public string districtName { get; set; }
        public int districtID { get; set; }
    }
}
