using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageOrganiser;

sealed class FileManager {

    public static string[] GetFilesFromDirectory(string targetDirectory) {
        HashSet<string> extensions = new() { ".bmp", ".gif", ".jpg", ".jpeg", ".png", ".svg", ".tiff" };

        string[] files = Directory.EnumerateFiles(targetDirectory)
            .Where(f => extensions.Contains(Path.GetExtension(f).ToLower()))
            .ToArray();

        return files;
    }

    public static DateTime? GetDateTakenFromImage(string path) {
        try {
            Regex _regex = new(":");
            const int DateTakenProperty = 36867;

            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using Image? myImage = Image.FromStream(fileStream, false, false);

            PropertyItem propItem = myImage.GetPropertyItem(DateTakenProperty);
            string dateTaken = _regex.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);

            return DateTime.Parse(dateTaken);
        }
        catch (Exception) {
            return null;
        }
    }

    public static List<ImageFile> CreateListOfImageFiles(string[] files) {
        var imageFiles = new List<ImageFile>();

        foreach (var file in files) {
            DateTime? dateTaken = GetDateTakenFromImage(file);
            ImageFile newImage = CreateImageFile(file, dateTaken);

            imageFiles.Add(newImage);
        }

        return imageFiles;
    }

    public static void MoveFilesToFolders(List<ImageFile> files) {
        foreach (var file in files) {
            if (file.DateTaken is not null) {
                string? source = file.FilePath;
                string destination = 
                    @$"{file.TargetDirectory}{file.DateTaken.Value.Year}\{(Months)file.DateTaken.Value.Month - 1}\{file.FileName}";

                File.Move(source, destination);
            }
        }
    }

    #region Private Methods
    private static ImageFile CreateImageFile(string filePath, DateTime? dateTaken) {
        var newImage = new ImageFile(filePath, dateTaken);

        return newImage;
    }
    #endregion
}