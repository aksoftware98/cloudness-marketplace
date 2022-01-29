# Deploye the infrastructuer by calling the following commands

# Create the resource group 
New-AzResourceGroup -Name "cloudnessmarketplace-rg" -Location "East US"

# Create the storage account 
New-AzStorageAccount -ResourceGroup "cloudnessmarketplace-rg" `
                     -AccountName "cloudnessmarketplace-storage" `
                     -Sku "Standard_LRS" `
                     -Location "East US"

#Create the Cosmos DB account 
# TO BE FILLED 

# Register the Microsoft.Web resource provider 
Register-AzResourceProvider -ProviderNamespace "Microsoft.Web"

# Create the Function App 
New-AzFunctionApp -ResourceGroup "cloudnessmarketplace-rg" `
                  -Location "East US" `
                  -StorageAccount "cloudnessmpstorage" `
                  -RunTime "DotNet" `
                  -FunctionsVersion "4" `
                  -Name "cloudnessmarketplace-functions"

# Create the Application Insights Account 
New-AzApplicationInsights -Location "East US" `
                          -ResourceGroup "cloudnessmarketplace-rg" `
                          -Kind "web" `
                          -Name "cloudnessmp-insights"

# Update the Azure Function to use the created Application Insights 
Update-AzFunctionApp -ApplicationInsightsName "cloudnessmp-insights" `
                     -Name "cloudnessmarketplace-functions" `
                     -ResourceGroupName "cloudnessmarketplace-rg"
