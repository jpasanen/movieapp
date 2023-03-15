@description('Object with general settings for all of the environments. (Eg. Location)')
param global object
@description('Data Lake name')
param dataLakeName string
@description('Data Lake bronze container name')
param bronzeContainer string
@description('Data Lake source container name')
param sourceContainer string

param buildId string = utcNow()

// Data Lake
resource dataLake 'Microsoft.Storage/storageAccounts@2020-08-01-preview' = {
  name: dataLakeName
  location: global.location
  kind: 'StorageV2'
  sku: {
    name: 'Standard_RAGRS'
  }
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    accessTier: 'Hot'
    supportsHttpsTrafficOnly: true
    allowBlobPublicAccess: false
    isHnsEnabled: true
    networkAcls: {
      bypass: 'AzureServices'
      defaultAction: 'Allow'
    }
  }
}

// Bronze container
resource bronze 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: '${dataLake.name}/default/${bronzeContainer}'
  properties: {
    publicAccess: 'None'
  }
}

// Source container
resource source 'Microsoft.Storage/storageAccounts/blobServices/containers@2021-04-01' = {
  name: '${dataLake.name}/default/${sourceContainer}'
  properties: {
    publicAccess: 'None'
  }
}

// Assign storage blob data contributor rights to developers
module roleAssignment 'roleAssignment.bicep' = {
  name: 'dataLake-roleAssignment-${buildId}'
  params: {
    dataLakeName: dataLakeName
    identityid: global.developerGroupId
  }
}

output dataLakeName string = dataLake.name
output dataLakeResourceId string = dataLake.id
