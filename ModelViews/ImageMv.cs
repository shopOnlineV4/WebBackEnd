
using System;

namespace ModelViews
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
        public string ImageLocation { get; set; }


        public ProductForList Product { get; set; }
        public UserInfor UserCreate { get; set; }
        public UserInfor UserModified { get; set; }

    }
    public class ImageInputMv
    {
        public Guid Id { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime DateCreate { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public int Status { get; set; }
        public Guid ProductId { get; set; }
        public string FileInput { get; set; }

       

    }
}