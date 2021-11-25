# Run this file to generate a current list of connector icon URLs and have them downloaded


$myURI = "https://flow.microsoft.com/en-us/connectors/"

$searchClass = "connector-item"

# items have the following structure:
#
#<a title="Harness PDFx" href="/en-us/connectors/shared_harnesspdfx/harness-pdfx/" data-event-name="ApiBrowsed" data-automation-id="connector-link" data-event-apidisplayname="Harness PDFx" data-event-apiname="shared_harnesspdfx">
#<img aria-hidden="true" onerror="this.onerror = null; this.src = 'https://psuxasia.azureedge.net/Content/retail/assets/connection.668c984c82d6c33195b0c32a5f48ea34.2.svg'" alt="Harness PDFx" src="https://connectoricons-prod.azureedge.net/releases/v1.0.1507/1.0.1507.2528/harnesspdfx/icon.png">
#<p class="api-name">Harness PDFx</p>
#<div class="premium-label-container" ng-non-bindable="">
#<span class="premium-label">
#PREMIUM
#</span>
#</div>
#</a>

$connectors = @()

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
$req = Invoke-Webrequest -URI $myURI
$parsedconnectors = $req.ParsedHtml.getElementsByClassName($searchClass)
write-host "$($parsedconnectors.length) potential connectors found. Starting processing"
$parsedconnectors| %{
    #lots of manual parsing here. Does its job at the moment, could be improved if needed
    $nameextract = $_.innerHTML.substring($_.innerHTML.IndexOf('title="')+7)
    $name = $nameextract.substring(0,$nameextract.IndexOf('"'))
    $uniquenameextract = $_.innerHTML.substring($_.innerHTML.IndexOf('href="/en-us/connectors/shared_')+31)
    $uniquename = $uniquenameextract.substring(0,$uniquenameextract.IndexOf('/'))
    $imageurlextract = $_.innerHTML.substring($_.innerHTML.IndexOf('https://connectoricons-prod.azureedge.net/')+42)
    $imageurl = $imageurlextract.substring(0,$imageurlextract.IndexOf('"'))
    $connectors += New-Object PSObject -Property @{
                                                Name = $name
                                                Uniquename = $uniquename
                                                Url = "https://connectoricons-prod.azureedge.net/$($imageurl)"
                                                }
	try{ Invoke-WebRequest "https://connectoricons-prod.azureedge.net/$($imageurl)" -OutFile "$($uniquename).png" } catch {}
}
$uniqueconnectors =  $connectors | Sort-Object -Property Uniquename -Unique
write-host "A total of $($uniqueconnectors.Count) unique connectors found"
write-host "Exporting json"
$uniqueconnectors | ConvertTo-Json | out-file connectors.json

write-host "All done"