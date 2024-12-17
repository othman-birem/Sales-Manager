
namespace Sales_Manager.Models
{
    public class FavoriteShortcut
    {
        public FavoriteShortcut(int pageIndex, string name = "fav")
        {
            PageIndex = pageIndex;
            Name = name;
        }
        public int PageIndex { get; set; }
        public string Name { get; set; }
        public override bool Equals(object? obj)
        {
            if (!(obj is FavoriteShortcut))
                return base.Equals(obj);
            return ((FavoriteShortcut)obj).PageIndex == this.PageIndex;
        }
    }
}
