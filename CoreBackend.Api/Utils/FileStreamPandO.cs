using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBackend.Api.Utils
{
    public class FileStreamPandO
    {
        /// <summary>
        /// 时间戳 取时间生成随机数
        /// </summary>
        public class UnixStamp
        {


            // 时间戳转为C#格式时间
            public DateTime StampToDateTime(string timeStamp)
            {
                DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);

                return dateTimeStart.Add(toNow);
            }

            // DateTime时间格式转换为Unix时间戳格式
            public int DateTimeToStamp(System.DateTime time)
            {
                System.DateTime startTime= System.TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local);
                //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));//弃用
                return (int)(time - startTime).TotalSeconds;
            }
        }
        public class FilesPrint
        {
            /// <summary>
            /// 成功写入返回路径
            /// </summary>
            /// <param name="url">绝对路径</param>
            /// <param name="src">相对路径</param>
            /// <param name="htmlcontent">内容</param>
            /// <returns>返回绝对路径</returns>
            public String PrintFileCreate(String url, Byte[] htmlcontent)
            {
                try
                {
                    UnixStamp ustamp = new UnixStamp();
                    string filestr = @"\" + System.DateTime.Now.Year.ToString() + @"\" + System.DateTime.Now.Month.ToString() + @"\" + System.DateTime.Now.Day.ToString();//文件夹
                    string fileurl = url + filestr;//绝对路径
                //    string filesrc = src + filestr;//相对路径
                    if (!(Directory.Exists(fileurl)))
                    {
                        Directory.CreateDirectory(fileurl);



                    }
                    string filedir = @"\" + ustamp.DateTimeToStamp(System.DateTime.Now) + ".bin";
                    string fullurl = fileurl + filedir;
                 //   string fullsrc = filesrc + filedir;
                    if (!(File.Exists(fullurl)))
                    {
                        File.WriteAllBytesAsync(fullurl, htmlcontent);
                        fullurl = fullurl.Replace(@"\", "-");
                        return fullurl;

                    }
                    return null;
                }
                catch (Exception)
                {
                    return null;
                    throw;

                }


            }
            /// <summary>
            /// 读取文件
            /// </summary>
            /// <param name="fileurl">文件完整路径</param>
            /// <returns>二进制</returns>
            public Byte[] ReadFile(string fileurl)
            {
                try
                {
                    byte[] bBuffer;

                    FileStream fs = new FileStream(fileurl, FileMode.Open);
                    BinaryReader binReader = new BinaryReader(fs);

                    bBuffer = new byte[fs.Length];
                    binReader.Read(bBuffer, 0, (int)fs.Length);

                    binReader.Close();
                    fs.Close();
                    return bBuffer;
                }
                catch (Exception)
                {
                    return null;
                 
                }
            }
            /// <summary>
             /// 成功写入返回1
             /// </summary>
             /// <param name="fileurl">完整路径</param>
             /// <param name="htmlcontent">内容</param>
             /// <returns></returns>
            public int PrintFileUpdate(String fileurl, Byte[] htmlcontent)
            {
                try
                {

                    if (File.Exists(fileurl))
                    {
                        File.WriteAllBytesAsync(fileurl, htmlcontent);
                        return 1;

                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return 0;
            }
            /// <summary>
            /// 路径转换,将数据库字符解析为绝对路径
            /// </summary>
            /// <param name="SQLURL">数据库读取路径</param>
            /// <returns></returns>
            public string readURL(String SQLURL)
            {
                return SQLURL.Replace("-", @"\");
            }
            /// <summary>
            /// 路径转换,获取相对网站路径
            /// </summary>
            /// <param name="SQLURL"></param>
            /// <returns></returns>
            public string ReadSRC(String SQLURL)
            {
                return SQLURL.Replace("-", @"/");
            }
            /// <summary>
            /// 路径转换 ，将绝对路径转换为数据库存储路径
            /// </summary>
            /// <param name="URL">文件存储路径</param>
            /// <returns></returns>
            public string SaveSRC(String URL)
            {
                return URL.Replace("\\", "-");
            }
        }

    }
}
