using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace CaseHelp_Core.Tools
{
    public class UploadHelper
    {
        /// <summary>
        /// 网络访问类
        /// </summary>
        private RequestHelper request = null;

        /// <summary>
        /// 上传路径
        /// </summary>
        public string Dir { get; set; }

        /// <summary>
        /// 上传地址
        /// </summary>
        public string PostUrl { get; set; }

        /// <summary>
        /// 引用
        /// </summary>
        public string Referer { get; set; }

        /// <summary>
        /// 水印文字
        /// </summary>
        public string WaterMark { get; set; }

        /// <summary>
        /// 水印文字大小
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// 上传路径
        /// </summary>
        private static string imgUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["ImageUrl"].ToString();
        /// <summary>
        /// 水印文字
        /// </summary>
        private static string HostMechine = System.Web.Configuration.WebConfigurationManager.AppSettings["HostMechine"].ToString();


        //按类型设置图片大小
        private int big_width = 600;
        private int big_height = 0;
        private int small_width = 150;
        private int small_height = 100;
        private string fileName;

        public UploadHelper()
        {
            this.request = new RequestHelper();
            this.FontSize = "10";
        }

        /// <summary>
        /// 设置上传后原始图片尺寸及缩略图尺寸
        /// </summary>
        /// <param name="big_width"></param>
        /// <param name="big_height"></param>
        /// <param name="small_width"></param>
        /// <param name="small_height"></param>
        public void SetImageSize(int big_width, int big_height, int small_width, int small_height)
        {
            this.big_width = big_width;
            this.big_height = big_height;
            this.small_width = small_width;
            this.small_height = small_height;
        }

        public string UploadImage(HttpPostedFileBase pic_upload)
        {
            string result = string.Empty;

            if (pic_upload != null)
            {
                try
                {
                    this.fileName = Guid.NewGuid().ToString() + Path.GetExtension(pic_upload.FileName);


                    // 将文件流转换成Base64字符串
                    pic_upload.InputStream.Seek(0, SeekOrigin.Begin);
                    Stream stream = pic_upload.InputStream;
                    BinaryReader br = new BinaryReader(stream);
                    byte[] fileByte = br.ReadBytes((int)stream.Length);
                    string baseFileString = Convert.ToBase64String(fileByte);

                    string postData = string.Format("data={0}&fileName={1}&path={2}&big_width={3}&big_height={4}&small_width={5}&small_height={6}&watermark={7}&fontsize={8}",
                        baseFileString.Replace("+", "%2B"), fileName, Dir, big_width, big_height, small_width, small_height, this.WaterMark, this.FontSize);

                    result = this.request.ResponseToString(this.request.doPost(PostUrl, postData, this.Referer));
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    GC.Collect();
                }
            }

            return result;
        }

        public string UploadImage(byte[] fileByte, string FileName)
        {
            string result = string.Empty;
            try
            {
                this.fileName = Guid.NewGuid().ToString() + Path.GetExtension(FileName);


                // 将文件流转换成Base64字符串
                //pic_upload.InputStream.Seek(0, SeekOrigin.Begin);
                //Stream stream = pic_upload.InputStream;
                //BinaryReader br = new BinaryReader(stream);
                //byte[] fileByte = br.ReadBytes((int)stream.Length);
                string baseFileString = Convert.ToBase64String(fileByte);

                string postData = string.Format("data={0}&fileName={1}&path={2}&big_width={3}&big_height={4}&small_width={5}&small_height={6}&watermark={7}&fontsize={8}",
                    baseFileString.Replace("+", "%2B"), fileName, Dir, big_width, big_height, small_width, small_height, this.WaterMark, this.FontSize);

                result = this.request.ResponseToString(this.request.doPost(PostUrl, postData, this.Referer));
            }
            catch
            {
                result = "";
            }
            finally
            {
                GC.Collect();
            }

            return result;
        }


        public static string FileImgSave(HttpPostedFileBase FileUpload, string Mark)
        {
            UploadHelper upload = new UploadHelper();
            upload.PostUrl = imgUrl;
            upload.Dir = Mark;// 标示 调用方法时传进来
            upload.WaterMark = HostMechine;//水印
            upload.SetImageSize(0, 0, 0, 0);
            string url = upload.UploadImage(FileUpload);
            string[] result = url.Split('|');
            if (result[0] == "0")
            {
                return "上传超时";
            }
            else
            {
                return result[1].ToString();  //result[1]为返回图片的URL地址
            }

        }

        public static string FileImgSave(HttpPostedFileBase FileUpload, string Mark, int sy = 0)
        {
            UploadHelper upload = new UploadHelper();
            upload.PostUrl = imgUrl;
            upload.Dir = Mark;// 标示 调用方法时传进来
            upload.WaterMark = sy == 0 ?  HostMechine : "";//水印
            upload.SetImageSize(0, 0, 0, 0);
            string url = upload.UploadImage(FileUpload);
            string[] result = url.Split('|');
            if (result[0] == "0")
            {
                return "上传超时";
            }
            else
            {
                return result[1].ToString();  //result[1]为返回图片的URL地址
            }

        }
    }
}