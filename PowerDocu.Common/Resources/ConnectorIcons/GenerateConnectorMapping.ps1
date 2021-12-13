# Run this file to generate a current list of connector icon URLs and have them downloaded


$myURI = "https://powerautomate.microsoft.com/en-us/api/connectors/all/"

$connectors = @()

#[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$req = Invoke-Webrequest -URI $myURI
$jsonObj = ConvertFrom-Json $([String]::new($req.Content))


write-host "$($jsonObj.value.Count) potential connectors found. Starting processing"
$jsonObj.value| %{
    $name = $_.properties.displayName
    $uniquename = $_.name -replace('shared_','')
    $imageurl = $_.properties.iconUri
    $connectors += New-Object PSObject -Property @{
                                                Name = $name
                                                Uniquename = $uniquename
                                                Url = $imageurl
                                                }
	try{ Invoke-WebRequest $imageurl -OutFile "$($uniquename).png" } catch {}
}
$uniqueconnectors =  $connectors | Sort-Object -Property Uniquename -Unique
write-host "A total of $($uniqueconnectors.Count) unique connectors found"
write-host "Exporting json"
$uniqueconnectors | ConvertTo-Json | out-file connectors.json

write-host "All done"