using System.Diagnostics;

namespace PatchSeller.API.Utilities
{
    public class SecurityService
    {
        public async Task<bool> ScanWithDefenderAsync(string filePath)
        {
            var programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            var defenderPath = Path.Combine(programFiles, "Windows Defender", "MpCmdRun.exe");

            var psi = new ProcessStartInfo
            {
                FileName = defenderPath,
                Arguments = $"-Scan -ScanType 3 -File \"{filePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            var output = process.StandardOutput.ReadToEnd();
            await process.WaitForExitAsync();

            return process.ExitCode == 0;
        }

    }
}
