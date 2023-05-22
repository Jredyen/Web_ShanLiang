using Microsoft.EntityFrameworkCore;
using prjShanLiang.Models;

namespace prjShanLiang.ViewModels
{
    public class CShowRestaurantViewModel
    {
        private IEnumerable<Store> _store { get; set; }
        private IEnumerable<Member> _member { get; set; }
        public CShowRestaurantViewModel()
        {
            ShanLiang21Context db = new ShanLiang21Context(); 
            //_store = db.Stores.ToList();
            _member = db.Members.ToList();
        }

        public IEnumerable<Store> store
        {
            get { return _store; }
            set { _store = value; }
        }
        public IEnumerable<Member> member
        {
            get { return _member; }
            set { _member = value; }
        }
        public string? storeDecorationImagePath { get; set; }
        public int? memberFavorateCount { get; set; }
        public IEnumerable<string>? storeMealImages { get; set; } 


    }
}
