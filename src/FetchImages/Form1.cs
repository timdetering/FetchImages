using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace FetchImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<string> FetchImages(string url)
        {
            Debug.Assert(false == String.IsNullOrEmpty(url));

            var imageList = new List<string>();

            //
            //  Append 'http://' if necessary.
            //
            if (false == StartsWithHttp(url))
            {
                url = "http://" + url;
            }

            string responseUrl;
            string htmlData = Encoding.ASCII.GetString(DownloadData(url, out responseUrl));

            if (0 != responseUrl.Length)
            {
                url = responseUrl;
            }

            if (0 != htmlData.Length)
            {
                int index = htmlData.IndexOf(ImageHtmlCode, StringComparison.OrdinalIgnoreCase);
                while (index != -1)
                {
                    //Remove previous data
                    htmlData = htmlData.Substring(index);

                    //Find the location of the two quotes that mark the image's location
                    int brackedEnd = htmlData.IndexOf('>'); //make sure data will be inside img tag
                    int start = htmlData.IndexOf(ImageSrcCode, StringComparison.OrdinalIgnoreCase) +
                                ImageSrcCode.Length;
                    int end = htmlData.IndexOf('"', start + 1);

                    //Extract the line
                    if (end > start && start < brackedEnd)
                    {
                        string loc = htmlData.Substring(start, end - start);

                        //Store line
                        imageList.Add(loc);
                    }

                    //Move index to next image location
                    if (ImageHtmlCode.Length < htmlData.Length)
                    {
                        index = htmlData.IndexOf(ImageHtmlCode, ImageHtmlCode.Length,
                                                 StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        index = -1;
                    }
                }

                //
                //  Format the image URLs.
                //
                string baseUrl = GetBaseUrl(url);
                for (var i = 0; i < imageList.Count; i++)
                {
                    string imageSrc = imageList[i];
                    if (false == String.IsNullOrEmpty(baseUrl) && false == StartsWithHttp(imageSrc))
                    {
                        imageSrc = baseUrl + "/" + imageSrc.TrimStart('/');
                    }

                    imageList[i] = imageSrc;
                }
            }

            return imageList;
        }

        //
        //  http://www.vcskicks.com/download_file_http.html
        //
        private static byte[] DownloadData(string url)
        {
            string responseUrl;
            return DownloadData(url, out responseUrl);
        }

        private static byte[] DownloadData(string url, out string responseUrl)
        {
            byte[] downloadedData;

            //
            //  Get a data stream from the URL.
            //
            var req = WebRequest.Create(url);
            using (var response = req.GetResponse())
            {
                responseUrl = response.ResponseUri.ToString();
                using (var stream = response.GetResponseStream())
                {
                    Debug.Assert(null != stream);

                    //
                    //  Download in chuncks.
                    //
                    byte[] buffer = new byte[1024];

                    //
                    //  Download to memory
                    //  NOTE: Adjust the streams here to download directly to the hard drive.
                    //
                    using (var memStream = new MemoryStream())
                    {
                        while (true)
                        {
                            //
                            //  Try to read the data.
                            //
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0)
                            {
                                break;
                            }

                            //
                            //  Write the downloaded data.
                            //
                            memStream.Write(buffer, 0, bytesRead);
                        }

                        //
                        //  Convert the downloaded stream to a byte array.
                        //
                        downloadedData = memStream.ToArray();
                    }
                }
            }

            return downloadedData;
        }

        //
        //  http://www.vcskicks.com/image-from-url.php
        //
        private Image ImageFromUrl(string url)
        {
            byte[] imageData = DownloadData(url);
            Image img;

            //
            //  TODO: Catch specific exceptions if creating the image fails.
            //
            using (var stream = new MemoryStream(imageData))
            {
                img = Image.FromStream(stream);
            }

            return img;
        }

        private string GetBaseUrl(string url)
        {
            const string schemeSeparator = "://";
            int inx = url.IndexOf(schemeSeparator, StringComparison.Ordinal) + schemeSeparator.Length;
            int end = url.IndexOf('/', inx);

            string baseUrl;
            if (end != -1)
            {
                baseUrl= url.Substring(0, end);
            }
            else
            {
                baseUrl= String.Empty;
            }
            return baseUrl;
        }

        private void btnGetImages_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            listImages.Items.Clear();

            foreach (var image in FetchImages(txtURL.Text))
            {
                listImages.Items.Add(image);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (listImages.SelectedIndex != -1)
            {
                picImage.Image = ImageFromUrl(listImages.SelectedItem.ToString());
            }
        }

        private static bool StartsWithHttp(string url)
        {
            return url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                   url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);
        }

        private const string ImageHtmlCode = "<img";
        private const string ImageSrcCode = @"src=""";
    }
}