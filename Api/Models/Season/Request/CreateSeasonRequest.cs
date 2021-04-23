using System;
using System.ComponentModel.DataAnnotations;

namespace Mongoose.Api.Models.Season.Request
{
    public class CreateSeasonRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Number { get; set; }
        [Required]
        public int SeriesId { get; set; }
    }
}