#load "functionHelpers.csx"

int chapterCount = 45;
for(int i =1;chapterCount != 0; chapterCount -= 1)
{
	DownloadChapter(i);
	i++;
}
