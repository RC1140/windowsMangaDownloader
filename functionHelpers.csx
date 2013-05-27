#r "System.IO.Compression.FileSystem.dll"
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

using System.Net;
using System.IO;
using System.IO.Compression;

public void DownloadPageImage(HtmlNode document,WebClient wc,string folder)
{
    var documents = document.QuerySelectorAll("#img").Select(n=> n.Attributes[3].Value).ToList();
    foreach(var node in documents)
    {
	var fileParts = node.Split('/');
	var fileName = fileParts[fileParts.Length - 1];
	wc.DownloadFile(node,folder+fileName);
    }
}

public void CreateComicFile(string currentFolderNumber)
{

	Console.WriteLine("Creating Zip File");
	ZipFile.CreateFromDirectory(".\\"+currentFolderNumber, ".\\"+currentFolderNumber+".zip");
	File.Move(".\\"+currentFolderNumber+".zip",".\\"+currentFolderNumber+".cbz");
	Directory.Delete(".\\"+currentFolderNumber,true);
	Console.WriteLine("Done Creating");

}

public void DownloadChapter(int chapterNumber)
{
	string currentFolderNumber = chapterNumber.ToString();
	string folder = ".\\"+currentFolderNumber+"\\";
	Directory.CreateDirectory(folder);

	WebClient wc = new WebClient();
	string baseLocation = "http://www.mangareader.net";
	string mangaName = "/shingeki-no-kyojin/"+currentFolderNumber;
	var pageData = wc.DownloadString(baseLocation+mangaName);
	HtmlDocument html = new HtmlDocument();
	html.LoadHtml(pageData);
	HtmlNode document = html.DocumentNode;
	//var chapterListings = document.QuerySelectorAll("#listing a").Select(n=> n.Attributes[0].Value).ToList();
	var imageListings = document.QuerySelectorAll("#pageMenu option").Select(n=> n.Attributes[0].Value).ToList();
	int counter = 0;
	foreach(var imagePage in imageListings)
	{
	    var currentPageData = wc.DownloadString(baseLocation + imagePage);
	    HtmlDocument htmlNode = new HtmlDocument();
	    htmlNode.LoadHtml(currentPageData);
	    DownloadPageImage(htmlNode.DocumentNode,wc,folder);
	    counter++;
	    Console.Clear();
	    Console.WriteLine("Downloading Chapter : "+currentFolderNumber);
	    Console.WriteLine("Number of pages found : "+imageListings.Count);
	    Console.WriteLine("Pages Downloaded : [" +counter.ToString()  + "]");
	}
	CreateComicFile(currentFolderNumber);
}
