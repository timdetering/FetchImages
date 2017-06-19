using System;
using System.Collections.Generic;
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
            var imageList = new List<string>();

            //Append http:// if necessary
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                url = "http://" + url;
            }

            string responseUrl = string.Empty;
            string htmlData = ASCIIEncoding.ASCII.GetString(DownloadData(url, out responseUrl));

            if (0 != responseUrl.Length)
            {
                url = responseUrl;
            }

            if (0 != htmlData.Length)
            {
                string imageHtmlCode = "<img";
                string imageSrcCode = @"src=""";

                int index = htmlData.IndexOf(imageHtmlCode);
                while (index != -1)
                {
                    //Remove previous data
                    htmlData = htmlData.Substring(index);

                    //Find the location of the two quotes that mark the image's location
                    int brackedEnd = htmlData.IndexOf('>'); //make sure data will be inside img tag
                    int start = htmlData.IndexOf(imageSrcCode) + imageSrcCode.Length;
                    int end = htmlData.IndexOf('"', start + 1);

                    //Extract the line
                    if (end > start && start < brackedEnd)
                    {
                        string loc = htmlData.Substring(start, end - start);

                        //Store line
                        imageList.Add(loc);
                    }

                    //Move index to next image location
                    if (imageHtmlCode.Length < htmlData.Length)
                    {
                        index = htmlData.IndexOf(imageHtmlCode, imageHtmlCode.Length);
                    }
                    else
                    {
                        index = -1;
                    }
                }

                //Format the image URLs
                for (var i = 0; i < imageList.Count; i++)
                {
                    string img = imageList[i];

                    string baseUrl = GetBaseUrl(url);

                    if ((!img.StartsWith("http://") && !img.StartsWith("https://"))
                        && baseUrl != string.Empty)
                    {
                        img = baseUrl + "/" + img.TrimStart('/');
                    }

                    imageList[i] = img;
                }
            }

            return imageList;
        }

        //http://www.vcskicks.com/download_file_http.html
        private byte[] DownloadData(string url)
        {
            string empty = string.Empty;
            return DownloadData(url, out empty);
        }

        private byte[] DownloadData(string url, out string responseUrl)
        {
            byte[] downloadedData = new byte[0];
            try
            {
                //Get a data stream from the url
                var req = WebRequest.Create(url);
                var response = req.GetResponse();
                var stream = response.GetResponseStream();

                responseUrl = response.ResponseUri.ToString();

                //Download in chuncks
                byte[] buffer = new byte[1024];

                //Get Total Size
                int dataLength = (int)response.ContentLength;

                //Download to memory
                //Note: adjust the streams here to download directly to the hard drive
                MemoryStream memStream = new MemoryStream();
                while (true)
                {
                    //Try to read the data
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        break;
                    }
                    else
                    {
                        //Write the downloaded data
                        memStream.Write(buffer, 0, bytesRead);
                    }
                }

                //Convert the downloaded stream to a byte array
                downloadedData = memStream.ToArray();

                //Clean up
                stream.Close();
                memStream.Close();
            }
            catch (Exception)
            {
                responseUrl = string.Empty;
                return new byte[0];
            }

            return downloadedData;
        }

        //http://www.vcskicks.com/image-from-url.php
        private Image ImageFromUrl(string url)
        {
            byte[] imageData = DownloadData(url);
            Image img = null;

            try
            {
                MemoryStream stream = new MemoryStream(imageData);
                img = Image.FromStream(stream);
                stream.Close();
            }
            catch (Exception)
            {
            }

            return img;
        }

        private string GetBaseUrl(string url)
        {
            int inx = url.IndexOf("://") + "://".Length;
            int end = url.IndexOf('/', inx);

            string baseUrl = string.Empty;
            if (end != -1)
            {
                return url.Substring(0, end);
            }
            else
            {
                return string.Empty;
            }
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
    }
}