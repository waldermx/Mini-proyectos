using Spectre.Console;

#region Handling cross-platform environments and filesystems


// SectionTable("Handling cross-platform environments and filesystems");

// Table table = new();

// table.AddColumn("[blue]MEMBER[/]");
// table.AddColumn("[blue]VALUE[/]");

// table.AddRow("Path.PathSeparator", PathSeparator.ToString());
// table.AddRow("Path.DirectorySeparatorChar", DirectorySeparatorChar.ToString());

// table.AddRow("Directory.DirectoryGetCurrentDirectory",GetCurrentDirectory());
// table.AddRow("Environment.CurrentDirectory", CurrentDirectory);
// table.AddRow("Environment.SystemDirectory", SystemDirectory);

// table.AddRow("Path.GetTempPath()", GetTempPath());
// table.AddRow("");
// table.AddRow("GetFolderPath(SpecialFolder", "");
// table.AddRow(" .System)", GetFolderPath(SpecialFolder.System));
// table.AddRow(" .ApplicationData)",
//  GetFolderPath(SpecialFolder.ApplicationData));
// table.AddRow(" .MyDocuments)",
//  GetFolderPath(SpecialFolder.MyDocuments));
// table.AddRow(" .Personal)",
//  GetFolderPath(SpecialFolder.Personal));
// AnsiConsole.Write(table);

#endregion

#region Managing Drives

// Table table = new();

// table.AddColumn("[blue]NAME[/]");
// table.AddColumn("[blue]TYPE[/]");
// table.AddColumn("[blue]FORMAT[/]");


// table.AddColumn(new TableColumn("[blue]SIZE (BYTES)[/]").RightAligned());
// table.AddColumn(new TableColumn("[blue]FREE SPACE[/]").RightAligned());

// foreach (DriveInfo drive in DriveInfo.GetDrives())
// {
//     if (drive.IsReady)
//     {
//         table.AddRow(drive.Name, drive.DriveType.ToString(),
//             drive.DriveFormat, drive.TotalSize.ToString("N0"),
//             drive.AvailableFreeSpace.ToString("N0"));
//     }
//     else
//     {
//         table.AddRow(drive.Name, drive.DriveType.ToString(),
//         string.Empty, string.Empty, string.Empty);
//     }
// }

// AnsiConsole.Write(table);

#endregion


#region 

SectionTitle("Managing directories");
string newFolder = Combine(
 GetFolderPath(SpecialFolder.Personal), "NewFolder");
WriteLine($"Working with: {newFolder}");
// We must explicitly say which Exists method to use
// because we statically imported both Path and Directory.
WriteLine($"Does it exist? {Path.Exists(newFolder)}");
WriteLine("Creating it...");
CreateDirectory(newFolder);
// Let's use the Directory.Exists method this time.
WriteLine($"Does it exist? {Directory.Exists(newFolder)}");
WriteLine("Confirm the directory exists, and then press any key.");
ReadKey(intercept: true);
WriteLine("Deleting it...");
Delete(newFolder, recursive: true);
WriteLine($"Does it exist? {Path.Exists(newFolder)}");

#endregion