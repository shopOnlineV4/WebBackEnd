
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAdmin.Models.ModelView
{
    public class ImageMv
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public int Status { get; set; }
        public Guid ProductId { get; set; }
        public string FileInput { get; set; }
        public string ImageLocation { get; set; }
        public ProductMv Product { get; set; }
    }


    public class ImageInput
    {
        public Guid ProductId { get; set; }
        [Required(ErrorMessage = "Please Choosen File !")]
        public IFormFile FileInput { get; set; }
    }
}