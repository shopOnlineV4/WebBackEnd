using System;
using System.Collections.Generic;
using System.Text;
using Domain.EF;
using Domain.Models.Entities;

namespace UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private WebOnlineDbContext _dbContext;
        private BaseRepository<AppRole> _AppRole;
        private BaseRepository<AppUser> _AppUser;
        private BaseRepository<Cart> _Cart;
        private BaseRepository<Category> _Category;
        private BaseRepository<Image> _Image;
        private BaseRepository<Product> _Product;
        private BaseRepository<TypeProduct> _TypeProduct;
        private BaseRepository<Order> _Order;
        private BaseRepository<OrderDetail> _OrderDetail;
        private BaseRepository<ColorCode> _ColorCode;
        private BaseRepository<Size> _Size;
        public UnitOfWork(WebOnlineDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual IRepository<AppRole> AppRoles => _AppRole ??= new BaseRepository<AppRole>(_dbContext);
        public virtual IRepository<AppUser> AppUsers => _AppUser ??= new BaseRepository<AppUser>(_dbContext);
        public virtual IRepository<Cart> Carts => _Cart ??= new BaseRepository<Cart>(_dbContext);
        public virtual IRepository<Category> Categories => _Category ??= new BaseRepository<Category>(_dbContext);
        public virtual IRepository<Image> Images => _Image ??= new BaseRepository<Image>(_dbContext);
        public virtual IRepository<Order> Orders => _Order ??= new BaseRepository<Order>(_dbContext);
        public virtual IRepository<OrderDetail> OrderDetails => _OrderDetail ??= new BaseRepository<OrderDetail>(_dbContext);
        public virtual IRepository<Product> Products => _Product ??= new BaseRepository<Product>(_dbContext);
        public virtual IRepository<TypeProduct> TypeProducts => _TypeProduct ??= new BaseRepository<TypeProduct>(_dbContext);
        public virtual IRepository<ColorCode> ColorCodes => _ColorCode ??= new BaseRepository<ColorCode>(_dbContext);
        public virtual IRepository<Size> Sizes => _Size ??= new BaseRepository<Size>(_dbContext);
        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}
