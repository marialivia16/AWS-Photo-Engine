using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;

namespace PhotoEngine
{
    public partial class _Default : Page
    {
        private const int C = 8;
        private const double minPercent = 1.0;
        static string bucketName = "elasticbeanstalk-us-west-2-675072056297";
        static string filePath = "Pictures/";
        static string key = "Pictures/abc.jpg";
        static IAmazonS3 client;
        static PutObjectRequest putObjectRequest;
        static string ACCESS_KEY = "AKIAJQY44DRZLDVFQSCQ";
        static string SECRET_ACCESS_KEY = "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";

        protected void Page_Load(object sender, EventArgs e)
        {
            //client = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);
            client = new AmazonS3Client(ACCESS_KEY, SECRET_ACCESS_KEY,Amazon.RegionEndpoint.USWest2);
            putObjectRequest = new PutObjectRequest();
            putObjectRequest.BucketName = bucketName;
            putObjectRequest.Key = key;
            putObjectRequest.ContentType = "image/jpeg";
        }

        protected void btnPreview_OnClick(object sender, EventArgs e)
        {
            Session["Image"] = fileUploader.PostedFile.InputStream;
            var fs = fileUploader.PostedFile.InputStream;

            putObjectRequest.InputStream = fileUploader.PostedFile.InputStream;
            putObjectRequest.Key = "Pictures/" + fileUploader.PostedFile.FileName;


            var br = new BinaryReader(fs);
            var bytes = br.ReadBytes((Int32)fs.Length);
            var base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            imgPreview.ImageUrl = "data:image/png;base64," + base64String;
            panelPreview.Visible = true;
            Session["bitmap"] = new Bitmap(fs);

            //trying to put an object in S3
            WritingAnObject();

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Bitmap bMap = Session["bitmap"] as Bitmap;
            var totalPixels = bMap.Width * bMap.Height;
            var hash = new Hashtable();
            BitmapData bData = bMap.LockBits(new Rectangle(0, 0, bMap.Width, bMap.Height), ImageLockMode.ReadWrite,
                bMap.PixelFormat);
            byte bitsPerPixel = GetBitsPerPixel(bData.PixelFormat);
            int size = bData.Stride * bData.Height; // The size of the image in bytes
            byte[] data = new byte[size]; //Allocate buffer for image
            System.Runtime.InteropServices.Marshal.Copy(bData.Scan0, data, 0, size);
            double percentOfPixel = 100 / Double.Parse(totalPixels.ToString());

            for (int i = 0; i < size - bitsPerPixel / 8; i += bitsPerPixel / 8)
            {
                var code = locateZone(data[i]) + "" + locateZone(data[i + 1]) + "" + locateZone(data[i + 2]);
                if (hash.ContainsKey(code))
                {
                    var valueToUpdate = (double)hash[code];
                    hash[code] = valueToUpdate + percentOfPixel;
                }
                else
                    hash.Add(code, percentOfPixel);
            }

            System.Runtime.InteropServices.Marshal.Copy(data, 0, bData.Scan0, data.Length);
            bMap.UnlockBits(bData);

            var keys = (from DictionaryEntry entry in hash select entry.Key.ToString()).ToList();
            foreach (var key in keys)
            {
                if (minPercent > (double)hash[key])
                    hash.Remove(key);
            }

            foreach (DictionaryEntry entry in hash)
            {
                Debug.WriteLine("{0}, {1}%", entry.Key, entry.Value);
            }
            
            int id = ImageHash.H.Count;
            ImageHash.H.Add(id, hash);
            panelPreview.Visible = false;
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            panelPreview.Visible = false;
        }

        private byte GetBitsPerPixel(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    return 24;
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 32;
                default:
                    throw new ArgumentException("Only 24 and 32 bit images are supported");

            }
        }

        private object locateZone(byte p)
        {
            var zone = 1;
            var step = 256 / C;
            var limit = step;
            while (p > limit)
            {
                zone++;
                limit += step;
            }
            return zone;
        }


        static void WritingAnObject()
        {
            try
            {
                PutObjectResponse response = client.PutObject(putObjectRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }

    }
}