# This script builds PDFs for spec MD files

# This require the pandoc/extra docker image from docker hub.
# for instance, need to perform:
# >docker pull pandoc/extra

$filesToConvert = @('IviDriverCore/1.0/Spec/IviDriverCore')

$fileHeaderForPdf = @"
---
header-includes:
  - \usepackage{enumitem}
  - \setlistdepth{20}
  - \renewlist{itemize}{itemize}{20}
  - \renewlist{enumerate}{enumerate}{20}
  - \setlist[itemize]{label=$\cdot$}
  - \setlist[itemize,1]{label=\textbullet}
  - \setlist[itemize,2]{label=--}
  - \setlist[itemize,3]{label=*}
---
"@

$tempFile = 'tempPdfGen.md'

# delete temporary file if left around
if (Test-Path $tempFile) {
    Remove-Item $tempFile
}

# This command gets all files in the specified directory and processes them one by one
$filesToConvert |  ForEach-Object {
    Write-Output "Processing: $_"
   
    Add-Content -Path $tempFile -Value $fileHeaderForPdf
    $Content = Get-Content -Path "$_.md" 
    $Content = $Content -Replace '\(\./', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/"
    Add-Content $Content -Path $tempFile

    $outFile = "$_.pdf"
    if (Test-Path $outFile) {
        Remove-Item $outFile
    }

    docker run --rm --volume "${pwd}:/data"  pandoc/extra $tempFile -o "$_.pdf"
    Remove-Item $tempFile
}

