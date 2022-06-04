using System.Text.RegularExpressions;

namespace ImageOrganiser;

class ImageFile {
    private string? _filePath;
    private string? _fileName;
    private string? _targetDirectory;
    private DateTime? _dateTaken;

    public string? FilePath => _filePath;
    public string? FileName => _fileName;
    public string? TargetDirectory => _targetDirectory;
    public DateTime? DateTaken => _dateTaken;

    public ImageFile(string filePath, DateTime? dateTaken) {
        _filePath = filePath;
        _dateTaken = dateTaken;
        _fileName = regexPattern.Match(_filePath).ToString();
        _targetDirectory = filePath.Replace(_fileName, "");
    }

    private static Regex regexPattern = new(@"[^\\]*\.(\w+)$");
}