namespace ImageOrganiser;

class Program {
    static void Main() {
        var directoryManager = new DirectoryManager();
        string targetDirectory = @"C:\Users\Iain\source\repos\TEMP_IMG_FOLDER\";
        string[] files = FileManager.GetFilesFromDirectory(targetDirectory);

        List<ImageFile> imageFiles = FileManager.CreateListOfImageFiles(files);

        directoryManager.SetYearDirectoriesRequired(imageFiles);
        directoryManager.SetMonthDirectoriesRequired(imageFiles);

        directoryManager.CreateRequiredDirectories(targetDirectory);

        FileManager.MoveFilesToFolders(imageFiles);
    }
}