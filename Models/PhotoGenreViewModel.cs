using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace YourSpace.Models
{
    public class PhotoGenreViewModel
    {
        public List<Photos> Photos { get; set; }
        public SelectList Genres { get; set; }
        public string PhotoGenre { get; set; }
        public string SearchString { get; set; }
    }
}
