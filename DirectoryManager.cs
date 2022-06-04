namespace ImageOrganiser;

class DirectoryManager {
    HashSet<int> _years;
    HashSet<string> _months; // YYYYMM

    public DirectoryManager() {
        _years = new();
        _months = new();
    }

    public void SetYearDirectoriesRequired(List<ImageFile> files) {
        foreach (var file in files) {
            if (file.DateTaken is not null) {
                int year = file.DateTaken.Value.Year;

                _years.Add(year);
            }
        }
    }

    public void SetMonthDirectoriesRequired(List<ImageFile> files) {
        foreach (var file in files) {
            if (file.DateTaken is not null) {
                string month = $"{file.DateTaken.Value.Year}{file.DateTaken.Value.Month}";

                _months.Add(month);
            }
        }
    }

    public void CreateRequiredDirectories(string targetDirectory) {
        CreateYearSubDirectories(targetDirectory);

        foreach (var year in _years) {
            Directory.SetCurrentDirectory(targetDirectory + year.ToString());
            CreateMonthSubDirectories(year.ToString());
        }
    }

    private void CreateYearSubDirectories(string targetDirectory) {
        string[] existingDirectories = Directory.GetDirectories(targetDirectory);

        if (existingDirectories.Length == 0) {
            foreach (var year in _years) {
                CreateSubDirectory(targetDirectory, year.ToString());
            }

            return;
        }

        foreach (var year in _years) {
            for (int i = 0; i < existingDirectories.Length; i++) {
                if (existingDirectories[i].Contains(year.ToString())) {
                    continue;
                }
                else {
                    CreateSubDirectory(targetDirectory, year.ToString());
                }
            }
        }
    }

    private void CreateMonthSubDirectories(string year) {
        var months = from m in _months
                     where m.Contains(year)
                     select int.Parse(m.Replace(year, ""));

        foreach (var month in months) {
            Directory.CreateDirectory($"{(Months)month - 1}");
            }
    }

    private void CreateSubDirectory(string targetDirectory, string directoryName) {
        Directory.CreateDirectory(targetDirectory + directoryName);
    }
}