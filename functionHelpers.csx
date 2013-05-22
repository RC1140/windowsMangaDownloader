#r "System.IO.Compression.FileSystem.dll"
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
	Console.WriteLine("[-]Downloading File : " + node);
    }
}

public void CreateComicFile(string currentFolderNumber)
{

	Console.WriteLine("Creating Zip File");
	ZipFile.CreateFromDirectory(folder, ".\\"+currentFolderNumber+.zip");
	Directory.Delete(folder);
	File.Move(".\\"+currentFolderNumber+".zip",".\\"+currentFolderNumber+".cbz")
	Console.WriteLine("Done Creating");

}
