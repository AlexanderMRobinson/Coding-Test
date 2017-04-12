param(
	[int]$records = 100
)

function Compare-Output()
{
	param(
		[Parameter(Mandatory=$true)]$csvgen,
		[Parameter(Mandatory=$true)]$slnout
	)
	$csvgen = $csvgen.Trim().Split([Environment]::NewLine)
	$slnout = $slnout.Trim().Split([Environment]::NewLine)
	
	foreach($p in $slnout)
	{
		if(($csvgen -contains $p) -eq $false)
		{
			$false
		}
	}
	$true
}

$csvfile = $env:TEMP + "\temp.csv"
$csvgen = "CSVGenerator\bin\Debug\CSVGenerator.exe"
$solution = "..\Source\CodingTest\bin\Debug\CodingTest.exe"

$path = "'$csvgen' $records $csvfile"
$csvout = Invoke-Expression "& $path"
$path = "'$solution' $csvfile"
$slnout = Invoke-Expression "& $path"
if(($csvout -eq $null) -and ($slnout -eq $null))
{
	Write-Host "Sort of success" 
	break
}
if((Compare-Output $csvout $slnout) -eq $true)
{
	Write-Host "Success"
}