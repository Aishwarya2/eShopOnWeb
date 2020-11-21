using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.ViewModels.File;
using Microsoft.Win32;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        RegistryKey key;
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Upload(FileViewModel fileViewModel)
        {
            if (!Request.Headers.ContainsKey("auth-key") || Request.Headers["auth-key"].ToString() != ApplicationCore.Constants.AuthorizationConstants.AUTH_KEY)
            {
                return Unauthorized();
            }

            if(fileViewModel == null || string.IsNullOrEmpty(fileViewModel.DataBase64)) return BadRequest();

            var fileData = Convert.FromBase64String(fileViewModel.DataBase64);
            if (fileData.Length <= 0) return BadRequest();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/products", fileViewModel.FileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            System.IO.File.WriteAllBytes(fullPath, fileData);

            return Ok();
        }
        
    }
    
            public void testmethod(String s)
        {

            key = Registry.CurrentUser.CreateSubKey(s);
            key.CreateSubKey(s);

        }
}
