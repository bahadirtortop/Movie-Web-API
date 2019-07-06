using System;

namespace Movie.Service.Movie.Model
{
    public class MovieListDtoModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Year { get; set; }
        public string Released { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Poster { get; set; }
        public string ImdbRating { get; set; }
    }
}
