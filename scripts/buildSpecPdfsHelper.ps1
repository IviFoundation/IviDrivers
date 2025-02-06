# This script is a bit of a hack to generate MD files suitable for creating PDFs with the 
# Visual Studio Code or other manual tools.


$Conversions = @(
    [System.Tuple]::Create('IviDriverCore/1.0/Spec/IviDriverCore','\(\./', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/")
    [System.Tuple]::Create('IviDriverNet/1.0/Spec/IviDriverNet','\(\.\./\.\./\.\.\/IviDriverCore', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore")
)

$fileHeaderForPdf = @" 
"@

$fileHeaderForPdf2 = @"
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
  - \usepackage[letterpaper,margin=0.7in]{geometry}
  - \fontsize{14pt}{18pt}\selectfont
  - \usepackage{hyperref}
  - \usepackage{xcolor}
  - \hypersetup{colorlinks=true, linkcolor=blue, urlcolor=blue, citecolor=blue}
  - \renewcommand{\normalsize}{\fontsize{14}{18}\selectfont}
  - \usepackage{fancyhdr}
  - \pagestyle{fancy}
  - \fancyhf{}
  - \fancyhead[C]{IVI Foundation}
  - \fancyfoot[R]{Page \thepage}
  - \renewcommand{\headrulewidth}{0.4pt}
  - \renewcommand{\footrulewidth}{0.4pt}

  
---
"@

$tempFile = 'tempPdfGen.md'

# delete temporary file if left around
if (Test-Path $tempFile) {
    Remove-Item $tempFile
}

# This command gets all files in the specified directory and processes them one by one
foreach($file in $Conversions) {
    Write-Output "Processing: $($file.Item1)"
   
    Add-Content -Path $tempFile -Value $fileHeaderForPdf
    $Content = Get-Content -Path "$($file.Item1).md" 
    #$Content = $Content -Replace '\(\./', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/"
    $Content = $Content -Replace $($file.Item2),$($file.Item3)
    Add-Content $Content -Path $tempFile

    $outFile = $($file.Item1)+"GenSource.md"
    if (Test-Path $outFile) {
        Remove-Item $outFile
    }

    Copy-Item $tempFile $outfile
    #docker run --rm --volume "${pwd}:/data"  pandoc/extra $tempFile -o "$($file.Item1).pdf"
    Remove-Item $tempFile
}

