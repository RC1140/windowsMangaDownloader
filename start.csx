#load "functionHelpers.csx"

using System.Net;
using System.IO;

using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

string currentFolderNumber = "4";
string folder = ".\\"+currentFolderNumber+"\\";
Directory.CreateDirectory(folder);

WebClient wc = new WebClient();
string baseLocation = "http://www.mangareader.net";
string mangaName = "/shingeki-no-kyojin/2";
var pageData = wc.DownloadString(baseLocation+mangaName);
HtmlDocument html = new HtmlDocument();
html.LoadHtml(pageData);
HtmlNode document = html.DocumentNode;
Console.WriteLine("[+]Starting Chapter Download : ");
//var chapterListings = document.QuerySelectorAll("#listing a").Select(n=> n.Attributes[0].Value).ToList();
var imageListings = document.QuerySelectorAll("#pageMenu option").Select(n=> n.Attributes[0].Value).ToList();
foreach(var imagePage in imageListings)
{
    var currentPageData = wc.DownloadString(baseLocation + imagePage);
    HtmlDocument html = new HtmlDocument();
    html.LoadHtml(currentPageData);
    HtmlNode document = html.DocumentNode;
    DownloadPageImage(document,wc,folder);
}
CreateComicFile(currentFolderNumber);
