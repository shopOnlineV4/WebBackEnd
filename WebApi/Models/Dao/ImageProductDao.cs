using AutoMapper;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork;
using WebApi.Common;
using WebApi.Models.FactoryModule;
using WebApi.Models.ModelView;

namespace WebApi.Models.Dao
{
    public class ImageProductDao : IFactory<ImageMv>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       
        private readonly IWebHostEnvironment _hostEnvironment;

        [Obsolete]
        public ImageProductDao(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostEnvironment = environment;

        }
        [Obsolete]
        public virtual ImageMv CreateNew(ImageMv data)
        {
            string fileName = DateTime.Now.Ticks + ".jpg";
            string pathBase = _hostEnvironment.WebRootPath + "\\productImages\\";
            try
            {
                if (!Directory.Exists(pathBase + "\\productImages\\"))
                    Directory.CreateDirectory(pathBase + "\\productImages\\");
                byte[] contents = Convert.FromBase64String(data.FileInput);
                MemoryStream ms = new MemoryStream(contents, 0, contents.Length);
                File.WriteAllBytes(pathBase + fileName, contents);
                //save data
                var image = new Image();
                image.FileName = fileName;
                image.ProductId = data.ProductId;
                image.CreateBy = data.CreateBy;
                var res = _unitOfWork.Images.CreateNewAddReturnObject(image);
                return _unitOfWork.Commit() ? _mapper.Map<ImageMv>(res) : null;
            }
            catch (Exception e)
            {
                //delete file when fail
                if (File.Exists(Path.Combine(pathBase, fileName)))
                    File.Delete(Path.Combine(pathBase, fileName));
                Console.WriteLine(e.Message);
                throw;
            }
        }

        [Obsolete]
        public virtual bool Delete(object id)
        {
            string pathBase = _hostEnvironment.WebRootPath + "\\productImages\\";
            _unitOfWork.Images.Delete(id);
            var image = _unitOfWork.Images.GetById(id).Result;
            //delete file when fail
            if (File.Exists(Path.Combine(pathBase, image.FileName)))
                File.Delete(Path.Combine(pathBase, image.FileName));
            return _unitOfWork.Commit();
        }

        public virtual bool Disable(object id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<ImageMv>> GetAll()
        {
            var images = await _unitOfWork.Images.GetAll();

            return images.Select(x => new ImageMv
            {
                CreateBy = x.CreateBy,
                DateCreate = x.DateCreate,
                DateModified = x.DateModified,
                FileInput = "",
                FileName = x.FileName,
                ImageLocation = ConstString.BaseLocationImageLink + x.FileName,
                Id = x.Id,
                ProductId = x.ProductId,
                ModifiedBy = x.ModifiedBy,
                Status = x.Status
            }).ToList();
        }

        public Task<List<ImageMv>> GetAll(int index = 1, int take = 10)
        {
            throw new NotImplementedException();
        }

        public virtual ImageMv GetById(object id)
        {
            throw new NotImplementedException();
        }

        public virtual bool Update(object id, ImageMv data)
        {
            throw new NotImplementedException();
        }
    }
}
