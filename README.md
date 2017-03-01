# C# Extract Images From URL #
<https://github.com/timdetering/FetchImages>

## Extract Image ##

We can use C# to extract images from a webpage with relative ease. Fetching images is simple since the .NET Framework comes with so many built-in libraries to connect .NET applications to the web.

The program will extract one image at a time using the same [C# image download method](http://www.vcskicks.com/image-from-url.php) explained before.

## Image URL ##

Since images on webpages are mostly referenced directly from other URLs (we're going to ignore CSS images in this article), by extracting the image locations from the HTML code of a webpage then we can individually download each image.

There are two parts to parsing the image URLs in HTML code. The first one is to find the location. The HTML code for an image is:

    <img [attributes] src="url" />

Which means the C# fetching application should focus on finding strings that match the beginning of the image tag, i.e. `<img`, and from there find the `src` attribute. Note that the application should not look for `<img src=` because we can't guarantee `src` will be the first attribute, although it commonly is.

Once we have the image URL we have a second thing to consider. Many times image URLs are *relative*. That means they do not spell out an entire web address, instead the use only a portion relative to the main webpage's address.

To convert the relative image URL to an *absolute* address (so we can download it), we need the base name of the main webpage and append the image relative URL.

## Downloading Images from URL ##

Once you have an absolute URL for an image you can do one of two things. You can either download the image to a file, which is explained in the [downloading web data in C#](http://www.vcskicks.com/download_file_http.php) article. Or you can download the image into memory (which is more flexible) which is explained in the [web image to memory](http://www.vcskicks.com/image-from-url.php) article.

## Issues and Bugs ##

Notice that our approach to extract images from a URL in C# has a few flaws.

1. The first flaw is that the source code only works on images that have a URL listed on the HTML code. This means flash images and CSS images go undetected. To fetch those images, further code is needed to track down the original image files' locations.

1. The second flaw is how we handled relative URLs. We turned relative URLs into absolute ones by using the basename from our original web request. That means if our original url was `www.gmail.com` for example, images might not be at `www.gmail.com/[relative url]` because gmail jumps around URLs.

However for now this is a basic C# example that works for general cases.



<http://www.vcskicks.com/extract-images.php>