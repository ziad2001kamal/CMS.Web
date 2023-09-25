using CMS.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data
{
    public static class DbSeeder
    {
       
        public static IHost SeedDb(this IHost webHost)
        {
           using var scope = webHost.Services.CreateScope();
            try
            {
                var context=scope.ServiceProvider.GetService<CMSDbContext>();
                var userManager=scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                context.SeedCategory().Wait();
                userManager.SeedUser().Wait();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return webHost;
        }

        public static async Task SeedCategory(this CMSDbContext _db)
        {
            if (await _db.Categorys.AnyAsync())
            {
                return;           
            }
            var categoires=new List<Category>();
            var category=new Category();
            category.Name = "A1";  
            category.CreatedAt = DateTime.Now;
            var category2 = new Category();
            category2.Name = "A2";
            category2.CreatedAt = DateTime.Now;
            categoires.Add(category);
            categoires.Add(category2);
                await _db.Categorys.AddRangeAsync(categoires);
            await _db.SaveChangesAsync();   

        }
        public static async Task SeedUser(this UserManager<User> _userManager)
        {
            if(await _userManager.Users.AnyAsync()) { return; }
            var user=new User();
            user.FullName = "System Developer";
            user.UserName = "dev";
            user.Email = "dev@gmail.com";
            user.CreatedAt = DateTime.Now;
           await _userManager.CreateAsync(user,"Admin1111$");

        }
    }
}
