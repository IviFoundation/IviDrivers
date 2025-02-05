# This script builds PDFs for spec MD files

# Pre-requisites to run the script
# --------------------------------
# Rather than trying a complete pandoc installation, with the associated required
# TeX installation, this script using the docker image provided by pandoc with 
# reasonably extensive TeX support for pdf generation.
#
# Therefore, the pre-requisites are:
#    1). Docker
#    2) THe Docker pandoc/extra image 
#
# The pandoc/extra docker image is available from from docker hub.
# To acquire, it need something like:
# >docker pull pandoc/extra

# Running the script
# ------------------
# The script has filename paths coded in the Conversions array, therefore it must
# be run from the root of the repository.
#    The script creates PDF files for the files identified in the Conversions array
# The conversions array also specifies generally replacements that may be made in the
# file.  The intended use is to convert URLS from MD relative links (as they are 
# preferred in markdown) to absolute links to github.  The latter being necessary 
# for the generated pdfs to be portable.

# Warnings and Errors
# -------------------
# When this script runs, there are several warnings about hyperlinks not found.
# this appear to me (JM 2/2025) to all be related to inconsistencies in how links
# to sections headers are handled between GFM and pandoc.  Therefore, I have left
# then since the GFM is the authoritative version.


# Script customization and usage
# ------------------------------
# The primary control of the script us through Conversions array below.  It works as follows:
#     * The array is a list of tuples, each specifies conversions to perform
#     * The first item in the tuple is the file name
#     * The second item is the target to replace
#     * The third item is the actual replacement value
#
# It is quite reasonable that a single file may have multiple links of different
# shapes that need to be modified.  That would require a simple extension to this
# script.
#


$Conversions = @(
    [System.Tuple]::Create('IviDriverCore/1.0/Spec/IviDriverCore','\(\./', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/")
    [System.Tuple]::Create('IviDriverNet/1.0/Spec/IviDriverNet','\(\.\./\.\./\.\.\/IviDriverCore', "(https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore")
)

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

    $outFile = "$_.pdf"
    if (Test-Path $outFile) {
        Remove-Item $outFile
    }

    docker run --rm --volume "${pwd}:/data"  pandoc/extra $tempFile -o "$($file.Item1).pdf"
    Remove-Item $tempFile
}

