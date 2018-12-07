using Microsoft.AspNetCore.Http;
using System;

namespace CoreBackend.Api.Dtos
{
   
    /// <summary>
    /// 文件上传
    /// </summary>
    public class FileUpDownLoadDto
    {
        public int FID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 
        /// </summary>

        public string FileUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileClass FileClass { get; set; }
        /// <summary>
        /// 文件转换为字节类传送
        /// </summary>
        public IFormFile file { get; set; }
        /// <summary>
        /// 样例 img1.jpg
        /// </summary>
    
    }

    /// <summary>
    /// 字节文件传输
    /// </summary>
    public class FileUpDownLoadToByteDto
    {
        public int FID { set; get; }
       /// <summary>
       /// 
       /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 
        /// </summary>
     
        public string FileUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileClass FileClass { get; set; }
        /// <summary>
        /// 文件转换为字节类传送
        /// </summary>
        public Byte[] Bytes { get; set; }
        /// <summary>
        /// 样例 img1.jpg
        /// </summary>
        public string FileName { set; get; }
    }
    /// <summary>
    /// 字节文件文件获取
    /// </summary>
    public class FileGetToByteDto
    {

     

        
        public int FID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public FileClass FileClass { get; set; }
        /// <summary>
        /// 文件转换为字节类传送
        /// </summary>
        public  Byte[] bytes  { get; set; }
        /// <summary>
        /// 样例 img1.jpg
        /// </summary>
        public string FileName { set; get; }
    }
    /// <summary>
    /// 文件获取不带file文件体
    /// </summary>
    public class FileUpDownLoadGetDto
    {
        public int FID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 
        /// </summary>

        public string FileUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileClass FileClass { get; set; }
   
        /// <summary>
        /// 样例 img1.jpg
        /// </summary>

    }
    /// <summary>
    /// 文件获取带file文件体
    /// </summary>
    public class FileUpDownLoadAndFileGetDto
    {
        public int FID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int SeedID { set; get; }
        /// <summary>
        /// 
        /// </summary>

        public string FileUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FileClass FileClass { get; set; }
        /// <summary>
        /// 文件转换为字节类传送
        /// </summary>
        public IFormFile file { get; set; }
        /// <summary>
        /// 样例 img1.jpg
        /// </summary>

    }
}
