using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FetchImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<string> FetchImages(string Url)
        {
            List<string> imageList = new List<string>();

            //Append http:// if necessary
            if (!Url.StartsWith("http://") && !Url.StartsWith("https://"))
            {
                Url = "http://" + Url;
            }

            string responseUrl = string.Empty;
            string htmlData = ASCIIEncoding.ASCII.GetString(DownloadData(Url, out responseUrl));

            if (responseUrl != string.Empty)
            {
                Url = responseUrl;
            }

            if (htmlData != string.Empty)
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
                for (int i = 0; i < imageList.Count; i++)
                {
                    string img = imageList[i];

                    string baseUrl = GetBaseURL(Url);

                    if ((!img.StartsWith("http://") && !img.StartsWith("https://"))
                        && baseUrl != string.Empty)
                        img = baseUrl + "/" + img.TrimStart('/');

                    imageList[i] = img;
                }
            }

            return imageList;
        }

        //http://www.vcskicks.com/download_file_http.html
        private byte[] DownloadData(string Url)
        {
            string empty = string.Empty;
            return DownloadData(Url, out empty);
        }

        private byte[] DownloadData(string Url, out string responseUrl)
        {
            byte[] downloadedData = new byte[0];
            try
            {
                //Get a data stream from the url
                WebRequest req = WebRequest.Create(Url);
                WebResponse response = req.GetResponse();
                Stream stream = response.GetResponseStream();

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
        private Image ImageFromURL(string Url)
        {
            byte[] imageData = DownloadData(Url);
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

        private string GetBaseURL(string Url)
        {
            int inx = Url.IndexOf("://") + "://".Length;
            int end = Url.IndexOf('/', inx);

            string baseUrl = string.Empty;
            if (end != -1)
            {
                return Url.Substring(0, end);
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

            foreach (string image in FetchImages(txtURL.Text))
            {
                listImages.Items.Add(image);
            }

            this.Cursor = Cursors.Default;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (listImages.SelectedIndex != -1)
            {
                picImage.Image = ImageFromURL(listImages.SelectedItem.ToString());
            }
        }
    }
}