param projectName string = 'sswtimesheetgpt'
param location string = resourceGroup().location
param tags object
param appInsightConnectionString string
param keyVaultName string

@allowed([
  'B1'
  'B2'
  'B3'
  'S1'
  'S2'
  'S3'
  'P1'
  'P2'
  'P3'
  'P1V2'
  'P2V2'
  'P3V2'
  'P1V3'
  'P2V3'
  'P3V3'
])
param skuName string = 'P1V2'

@minValue(1)
param skuCapacity int = 1

var entropy = substring(guid(subscription().subscriptionId, resourceGroup().id), 0, 4)

resource plan 'Microsoft.Web/serverfarms@2022-03-01' = {
  name: 'plan-${projectName}'
  location: location
  tags: tags
  kind: 'linux'
  sku: {
    name: skuName
    capacity: skuCapacity
  }
  properties: {
    reserved: true //required as this is a linux plan
  }
}

var appSettings = [
  {
    name: 'WEBSITES_ENABLE_APP_SERVICE_STORAGE'
    value: 'false'
  }
  //TODO: Check all the current appSettings and align
  //TODO: get secrects from github
  
]

resource appService 'Microsoft.Web/sites@2022-03-01' = {
  name: 'app-${projectName}-${entropy}'
  location: location
  kind: 'app,linux,container'
  identity: {
    type: 'SystemAssigned'
  }
  tags: union(tags, {
    'hidden-related:${plan.id}': 'empty'
  })
  properties: {
    serverFarmId: plan.id
    httpsOnly: true
    siteConfig: {
      appSettings: appSettings
      acrUseManagedIdentityCreds: true
      alwaysOn: true
      http20Enabled: true
      minTlsVersion: '1.2'
    }
    clientAffinityEnabled: false
  }
}

output appServiceHostName string = appService.properties.defaultHostName
output AppPrincipalId string = appService.identity.principalId
